namespace FSharp_ed_Bancho

type UserId (id) =
    member this.id: int = id
    
    override this.Equals other =
        match other with
        | :? UserId as p -> p.id = this.id
        | _ -> false