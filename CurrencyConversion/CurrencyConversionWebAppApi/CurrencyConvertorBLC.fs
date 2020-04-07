module CurrencyConvertorBLC 

    open FSharp.Data
    open FSharp.Data.JsonExtensions
    open FSharp.Configuration
    open System.Web.Script.Serialization
    open System.Collections.Generic
    open CurrencyTypes

   

    let key = Settings.AppId.ToString().Split('-')|>Array.fold(fun a x-> a+x)""
    //Can be used for API calls on live
    let url = "https://openexchangerates.org/api/latest.json?app_id="+key
    //let doc = CurrencyJson.AsyncLoad(url)
    let jsonValueFile = JsonValue.Load("../../data/latest.json")
    let commoncurrenciesData = JsonValue.Load("../../data/Common-Currency.json")
    let currencyJson = JsonValue.Load("../../data/currencies.json")
    let jsonValueURL = JsonValue.Load(url)

    let deserealiseObject (dataString :string) =
        let jss =new JavaScriptSerializer()
        let data  = jss.DeserializeObject(dataString) :?> Dictionary<string,obj>
        data|>Seq.map(fun x->x.Key,x.Value)

    let getCurrencyNameFromCode (code : string) =
        let (?) (o:obj) name : 'a = (o :?> Dictionary<string,obj>).[name] :?> 'a
        let data = deserealiseObject (commoncurrenciesData.ToString())
        let dataMap = data |>Map.ofSeq
        match dataMap.TryFind(code) with
        |Some a -> 
                    a?name
                      
        |None -> "--"

    let currencyDataAndCode (code:string) = 
        let data = deserealiseObject(currencyJson.ToString())|>Map.ofSeq
        match data.TryFind(code) with
        |Some cur -> cur.ToString()
        |None -> "--"

    let jsonValue (fileValue :JsonValue) = 
        deserealiseObject (((fileValue?rates).ToString()))        

    let currencyGridData (value: seq<string * obj>) =
        value|> Seq.map(fun (a,b) ->{CurrencyDataDef with
                                            currencyCode = a
                                            currencyValue = b.ToString().AsDecimal()
                                            currencyName = currencyDataAndCode a})

    let ddlTypeBinding (value: seq<string * obj>) = 
        value |>Seq.map(fun (a,b)-> {DDLDef with
                                        text = currencyDataAndCode a
                                        value = b.ToString()})

    let fileValue = currencyGridData (jsonValue jsonValueFile)
    let urlValue = currencyGridData (jsonValue jsonValueURL)

    let fileDDL = ddlTypeBinding (jsonValue jsonValueFile)
    let urlDDL = ddlTypeBinding (jsonValue jsonValueURL)