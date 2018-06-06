using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sql_project_cryptocurrency.Models
{
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }
}