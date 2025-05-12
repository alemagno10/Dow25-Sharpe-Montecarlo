open FSharp.Data
open DataLoader
open Simulate

[<EntryPoint>]
let main (argv: string[]) : int = 
    let timer = System.Diagnostics.Stopwatch.StartNew()
    let records = readCsv "data/dow_jones_close_prices_aug_dec_2024.csv"
    let matrix = toMatrix records

    // Thread-safe collection to store results
    let results = System.Collections.Concurrent.ConcurrentBag<SharpResult>()

    let simulate cols =
        printfn "Simulating for: %A" cols
        System.Console.Out.Flush()
        let best = operation cols matrix
        results.Add(best)

    processInParallelLazy 1000 (lazyCombinations 25 [0..29]) simulate

    let bestOverall = results |> Seq.maxBy (fun r -> r.Sharpe)

    let printResult r =
        printfn "========================"
        printfn "Best Sharpe Ratio: %.4f" r.Sharpe
        printfn "Annual Return:     %.4f" r.AnnualReturn
        printfn "Volatility:        %.4f" r.Volatility
        printfn "Selected Stocks:   %A" r.Stocks
        printfn "Portfolio Weights: %A" r.Weights
        printfn "========================"

    let timeTaken = timer.Elapsed.TotalSeconds
    toJson "data/output.json" bestOverall timeTaken

    printResult bestOverall 
    0