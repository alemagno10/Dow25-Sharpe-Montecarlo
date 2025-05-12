module Simulate

open System.Threading.Tasks
open System
open MathTools

type SharpResult = {
    Stocks: int list
    Weights: float list
    Sharpe: float
    AnnualReturn: float
    Volatility: float
}

let rnd = Random()

let rec lazyCombinations k list =
    seq {
        match k, list with
        | 0, _ -> yield []
        | _, [] -> ()
        | k, x::xs ->
            for tail in lazyCombinations (k - 1) xs do
                yield x :: tail
            yield! lazyCombinations k xs
    }

let processInParallelLazy (chunkSize: int) (items: seq<'a>) (operation: 'a -> unit) =
    items
    |> Seq.chunkBySize chunkSize
    |> Seq.iter (fun chunk ->
        chunk
        |> Seq.map (fun item -> async { return operation item })
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore
    )

let extractColumns (matrix: 'a list list) (cols: int list) : 'a list list =
    matrix |> List.map (fun row -> cols |> List.map (fun i -> row.[i]))

let generateWeights (quantity: int) =
    let rec tryGenerate () =
        let raw = List.init quantity (fun _ -> rnd.NextDouble())
        let total = List.sum raw
        let normalized = raw |> List.map (fun x -> x / total)

        if normalized |> List.forall (fun x -> x <= 0.2) then
            [ normalized ]
        else
            tryGenerate () 
    tryGenerate ()

let toReturns (prices: float list list) : float list list =
    let transposedPrices = List.transpose prices
    
    let returns = 
        transposedPrices
        |> List.map (fun ts ->
            ts
            |> List.pairwise
            |> List.map (fun (yesterday, today) -> (today - yesterday) / yesterday)
        )
    
    List.transpose returns

let operation (cols: int list) (matrix: float list list) =
    let selectedPrices = extractColumns matrix cols
    let selectedReturns = toReturns selectedPrices

    let results = 
        [1 .. 1000]
        |> List.map(fun _ -> 
            let weights = generateWeights 29

            let Rp = multiply selectedReturns weights 
            let RpT = List.transpose Rp
            let dailyReturns = RpT[0] 
            
            // Volatidade anualizado
            let stdDevDaily = stdDev dailyReturns
            let volatility = stdDevDaily * sqrt 252.0

            // Retorno anualizado
            let avgReturn = List.average dailyReturns
            let annualReturn = avgReturn * 252.0

            let sharpeRatio = annualReturn / volatility

            {
                Stocks = cols
                Weights = weights[0]
                Sharpe = sharpeRatio
                AnnualReturn = annualReturn
                Volatility = volatility
            }
        )
    
    List.maxBy (fun r -> r.Sharpe) results