namespace FSharp_ed_Bancho

type UserStatus(action, info_text, map_md5, mods, mode, map_id) =
    let action: uint8 = action
    let info_text: string = info_text

    let map_md5: string = map_md5

    let mods: int32 = mods

    let mode: uint8 = mode

    let map_id: int32 = map_id
