# -*- coding: utf-8 -*-
import scrapy
from time import gmtime, strftime

class CryptoSpider(scrapy.Spider):
  name = 'bitinfocharts' 
  allowed_domains = ['www.bitinfocharts.com/cryptocurrency-prices/all.html']
  start_urls = ['http://www.bitinfocharts.com/cryptocurrency-prices/all.html']
  def parse(self, response):
    currentTime = strftime("%Y-%m-%d %H:%M:%S", gmtime())
    rows = response.xpath('//div[@id="markets_container"]/div/table/tr[@class="ptr"]')
    for row in rows:
        name = row.xpath('td[2]').xpath('span/a/text()').extract_first();
        if name is not None:
            yield{	
			'name': name,
            'id': row.xpath('td[2]').xpath('@data-val').extract_first(),
			'change_7d' : row.xpath('td[3]/span[2]/b/text()').extract_first().strip('%').strip('+'),
			'price' : row.xpath('td[3]/a/text()').extract_first().strip('$'),
			'volume_24h' : row.xpath('td[6]/span[3]/span/text()').extract_first().strip('USD'),
			'timestamp' : currentTime,
			'source': "bitinfocharts",
		}
        
