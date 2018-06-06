using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using sql_project_cryptocurrency.Models;
using Newtonsoft.Json;
using static System.Int32;

namespace sql_project_cryptocurrency.WebService
{
    public class CryptoWebService
    {

        public async Task<string> InvokeRequestResponseService(string id_crypto) 
        {
            //Debug.WriteLine("Start");
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"id", "price_float"},
                                Values = new string[,] {  { id_crypto, "0" }, }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                //Debug.WriteLine("Stworzenie");
                const string apiKey = "fKVDDWXtkgnzT1l/NmEEDJ/oYKz4ldq9Ju62rV3lnwDQ0maOUI+cER27qlvHv1iJjcP5hBp1mruXk9etOKfiVg=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/8efabc7722504e9fb1bcba4033ebc454/services/b7c0ebc2756c49ee9c54f1ee2542a42b/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);
                //Debug.WriteLine("Połączono");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var resultOutcome = JsonConvert.DeserializeObject<ResultOutcome>(result);
                    //Debug.WriteLine("Result: {0}", result);
                    var resultat = resultOutcome.Results.Output1.Value.Values;
                    string predicted = resultat[0, 2];
                    return predicted;
                }
                else
                {
                    Debug.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Debug.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Debug.WriteLine(responseContent);
                    return "";
                }
            }
        }
    }
}