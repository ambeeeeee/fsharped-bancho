namespace FSharp_ed_Bancho

type UserMessage =
    | FetchId of AsyncReplyChannel<UserId>
    | FetchStatus of AsyncReplyChannel<Option<UserStatus>>
    | FetchUserInfo of AsyncReplyChannel<Option<UserInfo>>
    | SetStatus of Option<UserStatus>
    | SetUserInfo of Option<UserInfo>

type User(id) =
    inherit UserId(id)

    let mutable userStatus: Option<UserStatus> = None

    let mutable userInfo: Option<UserInfo> = None

    member this.Mailbox =
        MailboxProcessor.Start
            (fun inbox ->
                let rec loop =
                    async {
                        let! msg = inbox.Receive()

                        match msg with
                        | FetchId reply -> reply.Reply(this)
                        | FetchStatus reply -> reply.Reply(userStatus)
                        | FetchUserInfo reply -> reply.Reply(userInfo)
                        | SetStatus status -> userStatus <- status
                        | SetUserInfo info -> userInfo <- info
                    }

                loop)
