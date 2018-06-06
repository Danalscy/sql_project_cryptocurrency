# -*- coding: utf-8 -*-
import scrapy
from time import gmtime, strftime

class CryptoSpider(scrapy.Spider):
  name = 'coinmarketcap' 
  allowed_domains = ['www.coinmarketcap.com/all/views/all/']
  start_urls = ['http://www.coinmarketcap.com/all/views/all/']
  def parse(self, response):
    currentTime = strftime("%Y-%m-%d %H:%M:%S", gmtime())
    rows = response.xpath('//table[@id="currencies-all"]/tbody/tr')
    for row in rows:
        price = row.xpath('td[5]/a/text()').extract_first().strip('$');
        if price is not "?":
          yield{
			'name': row.xpath('td[2]/a/text()').extract_first(),
            'id': row.xpath('td[3]/text()').extract_first(),
			'market_cap' :row.xpath('normalize-space(td[4]/text())').extract_first().strip('$'),
			'price' : '{:.20f}'.format(float(price)),
			'circulating_supply' : row.xpath('normalize-space(td[6]/a/text())').extract_first(),
			'volume_24h' : row.xpath('td[7]/a/text()').extract_first().strip('$'),
			'change_1h' : row.xpath('td[8]/text()').extract_first().strip('%'),
			'change_24h' : row.xpath('td[9]/text()').extract_first().strip('%'),
			'change_7d' : row.xpath('td[10]/text()').extract_first().strip('%'),
			'timestamp' : currentTime,
            'source' : "coinmarketcap",
		  }
        
