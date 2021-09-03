namespace FSharp_ed_Bancho

open System

type User(id) =
    inherit UserId(id) 

    let userStatus: Option<UserStatus> = None
    
    let mailbox =
        new MailboxProcessor<string>(User.mailboxHandler)

    static member mailboxHandler =
        fun inbox ->
            let rec loop () =
                async {
                    printfn "hi"

                    let! msg = inbox.Receive()
                    do! loop ()
                }

            loop ()
