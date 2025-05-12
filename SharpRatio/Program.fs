open FSharp.Data
open DataLoader

let records = readCsv "data/dow_jones_close_prices_aug_dec_2024.csv"
let matrix = toMatrix records

printfn "%A" (matrix[0])


