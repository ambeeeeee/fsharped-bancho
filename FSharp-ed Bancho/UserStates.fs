namespace FSharp_ed_Bancho

open System
open System.Collections.Generic

type FetchUserMessage =
    { Channel: AsyncReplyChannel<Option<User>>
      Id: UserId }

type UserStatesMessage =
    | FetchUser of UserId * AsyncReplyChannel<Option<User>>
    | AddUser of User
    | RemoveUser of UserId


type UserStates() =
    static let mutable users = new Dictionary<UserId, User>()

    static let GetUser userId =
        lock
            users
            (fun () ->
                if users.ContainsKey userId then
                    Some(users.Item(userId))
                else
                    None)

    static let AddUser user =
        lock users (fun () -> users.Add(user, user))


    static let RemoveUser user =
        lock users (fun () -> users.Remove(user) |> ignore)

    member this.Mailbox =
        MailboxProcessor.Start
            (fun inbox ->
                let rec loop =
                    async {
                        let! msg = inbox.Receive()

                        match msg with
                        | FetchUser (id, channel) -> channel.Reply <| GetUser id
                        | AddUser user -> AddUser user
                        | RemoveUser user -> RemoveUser user
                    }

                loop)
