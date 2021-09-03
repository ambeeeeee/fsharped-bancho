open System

open FSharp_ed_Bancho
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful

let userStates = new UserStates()

userStates.Mailbox.Post <| AddUser(new User(727))

let getUser id =
    userStates.Mailbox.PostAndReply(fun r -> FetchUser(id, r))

let getUserRoute id =
    let userId = new UserId(id)
    let user = getUser userId
    user.ToString()

let app =
    GET >=> pathScan "/get-user/%d" (fun id -> OK <| getUserRoute id )

startWebServer defaultConfig app