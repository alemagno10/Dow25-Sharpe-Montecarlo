module DataLoader

open System
open System.IO
open Simulate

type StockRecord = {
    Date: DateTime
    AAPL: float
    AMGN: float
    AMZN: float
    AXP: float
    BA: float
    CAT: float
    CRM: float
    CSCO: float
    CVX: float
    DIS: float
    GS: float
    HD: float
    HON: float
    IBM: float
    JNJ: float
    JPM: float
    KO: float
    MCD: float
    MMM: float
    MRK: float
    MSFT: float
    NKE: float
    NVDA: float
    PG: float
    SHW: float
    TRV: float
    UNH: float
    V: float
    VZ: float
    WMT: float
}

let parseLine (line: string) =
    let parts = line.Split(',') 
    {
        Date = DateTime.Parse(parts.[0])
        AAPL = float parts.[1]
        AMGN = float parts.[2]
        AMZN = float parts.[3]
        AXP = float parts.[4]
        BA = float parts.[5]
        CAT = float parts.[6]
        CRM = float parts.[7]
        CSCO = float parts.[8]
        CVX = float parts.[9]
        DIS = float parts.[10]
        GS = float parts.[11]
        HD = float parts.[12]
        HON = float parts.[13]
        IBM = float parts.[14]
        JNJ = float parts.[15]
        JPM = float parts.[16]
        KO = float parts.[17]
        MCD = float parts.[18]
        MMM = float parts.[19]
        MRK = float parts.[20]
        MSFT = float parts.[21]
        NKE = float parts.[22]
        NVDA = float parts.[23]
        PG = float parts.[24]
        SHW = float parts.[25]
        TRV = float parts.[26]
        UNH = float parts.[27]
        V = float parts.[28]
        VZ = float parts.[29]
        WMT = float parts.[30]
    }

let readCsv path =
    File.ReadAllLines(path)
    |> Seq.skip 1
    |> Seq.map parseLine
    |> Seq.toList

let toMatrix (records: StockRecord list) =
    records
    |> List.map (fun record -> 
        [ record.AAPL; record.AMGN; record.AMZN; record.AXP; record.BA; record.CAT;
          record.CRM; record.CSCO; record.CVX; record.DIS; record.GS; record.HD;
          record.HON; record.IBM; record.JNJ; record.JPM; record.KO; record.MCD;
          record.MMM; record.MRK; record.MSFT; record.NKE; record.NVDA; record.PG;
          record.SHW; record.TRV; record.UNH; record.V; record.VZ; record.WMT ]
    )

let saveToCsv (path: string) (sr: SharpResult) (time: float) =
    use writer = new StreamWriter(path)

    writer.WriteLine("Sharpe,AnnualReturn,Volatility,Stocks,Weights,Time")

    let stocks = String.Join(";", sr.Stocks)
    let weights = String.Join(";", sr.Weights |> List.map string)
    writer.WriteLine($"{sr.Sharpe},{sr.AnnualReturn},{sr.Volatility},{stocks},{weights},{time}")
    
    
