module FSharp_ed_Bancho.UsersState

open System.Threading
type UsersState() =
    static let mutable state: int = 0

    static member Increment =
        state <- state + 1
        $"%d{state}"