module CurrencyTypes
    open System
    open FSharp.Data
    open FSharp.Configuration

     type Settings = AppSettings<"App.config">

     type CurrencyData =
        {
        currencyName : string
        currencyCode : string
        currencyValue : decimal
        }

    let CurrencyDataDef = 
        {
        currencyName = String.Empty
        currencyCode = String.Empty
        currencyValue = 0.0M
        }

    type DDL =
        { text :string
          value :string
        }

    let DDLDef = 
        {
            text = String.Empty
            value =  String.Empty
        }
    