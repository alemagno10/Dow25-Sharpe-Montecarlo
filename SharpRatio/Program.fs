open FSharp.Data
open DataLoader
open Simulate
open Evaluate

[<EntryPoint>]
let main (argv: string[]) : int = 
    let timer = System.Diagnostics.Stopwatch.StartNew()
    let records = readCsv "data/dow_jones_close_prices_aug_dec_2024.csv"
    let matrix = toMatrix records

    Thread-safe collection to store results
    let results = System.Collections.Concurrent.ConcurrentBag<SharpResult>()

    let simulate cols =
        let best = operation cols matrix
        results.Add(best)

    processInParallelLazy 1000 (lazyCombinations 25 [0..29]) simulate

    let bestOverall = results |> Seq.maxBy (fun r -> r.Sharpe)
    let timeTaken = timer.Elapsed.TotalSeconds
    toJson "data/output.json" bestOverall timeTaken

    // Evaluate Weights
    let bestSharp = readJson "data/output.json"
    let records = readCsv "data/dow_jones_close_prices_jan_mar_2025.csv"
    let matrix = toMatrix records

    operationTrainedWeights bestSharp.Stocks matrix bestSharp.Weights
    0