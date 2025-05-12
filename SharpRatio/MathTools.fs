module MathTools

let dotProduct v1 v2 =
    List.map2 (*) v1 v2 |> List.sum

let transpose matrix =
    matrix |> List.transpose

let multiply (a: float list list) (b: float list list) =
    [ for rowA in a ->
        [ for colB in b ->
            dotProduct rowA colB
        ]
    ]

let stdDev (xs: float list) =
    let mean = List.average xs
    let variance = xs |> List.map (fun x -> (x - mean) ** 2.0) |> List.average
    sqrt variance