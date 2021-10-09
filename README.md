# Product Stock Watcher
A simple way to keep track of product availability and price on any eCommerce website

## Getting Started
Simply create a new JSON file somewhere on your computer using the schema below. Once created, run the program with the environment variable `PRODUCT_JSON_PATH` which points to the file location.

## JSON Schema Example
```json
[
  {
    "Url": "https://www.walmart.com/ip/Boxis-AutoShred-50-Sheet-Auto-Feed-Microcut-Paper-Shredder-Includes-a-12-Pack-of-Shredcare-Lubricant-Sheets/382914472",
    "PriceSelector": {
      "Type": "LinkedJson",
      "PathTemplate": "offers.price"
    },
    "StockSelector": {
      "Type": "LinkedJson",
      "PathTemplate": "offers.availability",
      "RegexTest": "InStock"
    },
    "Title": "Paper Shredder I Want"
  },
  {
    "Url": "https://www.sparkfun.com/products/18567",
    "PriceSelector": {
      "Type": "Html",
      "Selector": "span[itemprop='price']",
      "Property": "content"
    },
    "StockSelector": {
      "Type": "Html",
      "Selector": "link[itemprop='availability']",
      "Property": "href",
      "RegexTest": "InStock"
    },
    "Title": "SparkFun Qwiic WiFi Shield - DA16200"
  }
]
```
