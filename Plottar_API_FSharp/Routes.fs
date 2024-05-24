namespace Plottar_API_FSharp

open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

module Routes =
  open Microsoft.AspNetCore.Http
  let webApp : HttpFunc -> HttpContext -> HttpFuncResult =
    choose [
        route "/test"   >=> json {|message="Working"|}
        route "/"       >=> htmlFile "/pages/index.html" ]
