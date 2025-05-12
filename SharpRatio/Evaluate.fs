module Evaluate

open Simulate
open DataLoader
open MathTools

let operationTrainedWeights (cols: int list) (matrix: float list list) (weights: float list) =
    let selectedPrices = extractColumns matrix cols
    let selectedReturns = toReturns selectedPrices

    let result = 
        let Rp = multiply selectedReturns [weights] 
        let RpT = List.transpose Rp
        let dailyReturns = RpT[0] 
        
        let stdDevDaily = stdDev dailyReturns
        let volatility = stdDevDaily * sqrt 252.0

        let avgReturn = List.average dailyReturns
        let annualReturn = avgReturn * 252.0

        let sharpeRatio = annualReturn / volatility

        {
            Stocks = cols
            Weights = weights
            Sharpe = sharpeRatio
            AnnualReturn = annualReturn
            Volatility = volatility
        }
        
    toJson "data/output_trained_weights.json" result 0.0