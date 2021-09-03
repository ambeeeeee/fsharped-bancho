open System

open FSharp_ed_Bancho
open FSharp_ed_Bancho.UsersState
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful

let response id =
    lock UsersState (fun() -> UsersState.Increment)

let app =
    GET >=> pathScan "/increment/%d" (fun id -> OK <| response id)

startWebServer defaultConfig app