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

## Schema Components
- `Url` - The URL of the product
- `PriceSelector` - An object that describes how to find the price on the page
- `StockSelector` - An object that describes where to find the availability of a product (using RegEx to test the value)
- `Title` - A reference name for the product

### Selectors
Valid types are `Html`, `Json`, `LinkedJson`

#### HTML Selector
- `Selector` - A DOM query
- `Property` - Optional property to refer to one of the element's attributes. If not provided, text is used

#### JSON Selector
- `PathTemplate` - A JSON path (i.e `offers.availability` or `offers[0].availability`)
