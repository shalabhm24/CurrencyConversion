namespace FsWeb.Controllers

open System.Web
open System.Web.Mvc
open CurrencyConvertorBLC
[<HandleError>]
type HomeController() =
    inherit Controller()
    member this.Index () =
        this.View():>ActionResult

    member this.Currency() =
        this.Json(fileValue,JsonRequestBehavior.AllowGet)

    member this.currencyURL() =
        this.Json(urlValue,JsonRequestBehavior.AllowGet)
    
    member this.fileDDL() =
        this.Json(fileDDL,JsonRequestBehavior.AllowGet)

    member this.urlDDL() =
        this.Json(urlDDL,JsonRequestBehavior.AllowGet)