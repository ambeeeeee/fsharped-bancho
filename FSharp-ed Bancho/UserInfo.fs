namespace FSharp_ed_Bancho

type UserInfo(username, ranked_score, accuracy, plays, total_score, rank, performance) =
    let username: string = username
    
    let ranked_score: int64 = ranked_score
    
    let accuracy: float = accuracy
    
    let plays: int32 = plays
    
    let total_score: int64 = total_score
    
    let rank: int32 = rank
    
    let performance: int16 = performance