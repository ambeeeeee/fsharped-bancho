namespace FSharp_ed_Bancho

type UserMessage =
    | FetchId of AsyncReplyChannel<UserId>
    | FetchStatus of AsyncReplyChannel<Option<UserStatus>>
    | FetchUserInfo of AsyncReplyChannel<Option<UserInfo>>

type User(id) =
    inherit UserId(id)

    member this.userStatus: Option<UserStatus> = None

    member this.userInfo: Option<UserInfo> = None

    member this.Mailbox =
        MailboxProcessor.Start
            (fun inbox ->
                let rec loop =
                    async {
                        let! msg = inbox.Receive()

                        match msg with
                        | FetchId reply -> reply.Reply(this)
                        | FetchStatus reply -> reply.Reply(this.userStatus)
                        | FetchUserInfo reply -> reply.Reply(this.userInfo)
                    }

                loop)
