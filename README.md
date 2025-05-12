# Sharpe Ratio Simulation

Este projeto realiza simulações de carteiras utilizando as 30 ações que compõem o índice **Dow Jones**, com o objetivo de calcular o **Índice de Sharpe** de milhares de carteiras sorteadas.

A ideia principal é:
- Gerar todas as combinações possíveis de 25 ações entre as 30 disponíveis (~142.506 combinações).
- Para cada combinação, simular 1.000 carteiras com diferentes pesos aleatórios.
- Calcular, para cada carteira:
  - Retorno anualizado
  - Volatilidade anualizada
  - Índice de Sharpe

## Estrutura do Projeto

```
/
├─ SharpRatio/                 # Projeto principal F#
│   ├─ DataLoader.fs           # Lê os arquivos CSV e transforma os dados em uma matriz de preços
│   ├─ Evaluate.fs             # Avalia o desempenho do portfólio (e.g., cálculo do índice de Sharpe)
│   ├─ MathTools.fs            # Ferramentas matemáticas: média, desvio padrão, operações com vetores
│   ├─ Simulate.fs             # Contém as funções de simulação de portfólios e estratégias
│   └─ Program.fs              # Ponto de entrada do programa; carregamento, simulação e avaliação
│
├─ data/                       # Dados históricos de preços das ações do Dow Jones
│   ├─ dow_jones_close_prices_aug_dec_2024.csv       # Fechamento diário das ações (ago-dez 2024)
│   ├─ dow_jones_close_prices_jan_mar_2025.csv       # Fechamento diário das ações (jan-mar 2025)
│   ├─ fetch_data.py                                 # Dados de ago-dez 2024 do Yahoo Finance
│   └─ fetch_data_2025.py                            # Dados de jan-mar 2025 do Yahoo Finance
│
└─ results/                          # Resultados gerados pelas simulações
    ├─ output.csv                    # Resultados das simulações em formato tabular
    └─ output_trained_weights.txt    # Pesos treinados para o portfólio otimizados pelas simulações

```

---

## Requisitos

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- [F#](https://fsharp.org/)
- Editor recomendado: [Visual Studio Code](https://code.visualstudio.com/) com extensão Ionide-fsharp

---

## Como Instalar

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/sharpe-ratio-simulation.git
   cd sharpe-ratio-simulation
   ```

1. Download dos dados via Python:
    ```bash
    python -m venv env
    env/bin/activate
    python fech_data.py
    ```

1. Executar Scripts F#
    ```bash
    dotnet run --project SharpRatio
    ```

## Observações
O índice de Sharpe é calculado como:
- Sharpe = (Retorno Anualizado - Taxa Livre de Risco) / Volatilidade Anualizada
- Neste projeto, a taxa livre de risco é considerada como 0.
- Os dados utilizados são de Agosto a Dezembro de 2024.

## Restrições 
- Alocar apenas 25 ações das 30 disponíveis no índice Dow Jones
- Carteira long-only: wi > 0
- Nenhum ativo pode ter mais de 20% da carteira: wi < 0.2

## Composição do Portfólio Otimizado
| Ação | Peso (%) |
| ---- | -------- |
| AAPL | 6.91%    |
| AMGN | 0.70%    |
| AMZN | 2.51%    |
| AXP  | 0.92%    |
| BA   | 0.31%    |
| CAT  | 2.33%    |
| CRM  | 7.26%    |
| CSCO | 10.21%   |
| DIS  | 10.14%   |
| HD   | 1.42%    |
| HON  | 6.21%    |
| IBM  | 2.86%    |
| JNJ  | 9.00%    |
| KO   | 0.52%    |
| MCD  | 7.41%    |
| MMM  | 3.59%    |
| MSFT | 0.98%    |
| NVDA | 1.86%    |
| PG   | 0.94%    |
| SHW  | 0.69%    |
| TRV  | 0.40%    |
| UNH  | 0.02%    |
| V    | 10.71%   |
| VZ   | 0.42%    |
| WMT  | 11.68%   |

### Métricas de Performance (Treinamento)
- Sharpe Ratio: 3.2252
- Retorno Anualizado: 37.63%
- Volatilidade: 11.67%
- Tempo de Execução: 6771.16 segundos

## Avaliação
Após encontrar o portfólio ótimo com base no Sharpe Ratio no período de treinamento (de agosto a dezembro), foi realizada uma avaliação dos mesmos pesos no período seguinte, de janeiro a março.

Foram utilizados os preços diários das seguintes ações:
### Resultados da Avaliação (Jan 01 – Mar 31)
- Sharpe Ratio: -0.3268
- Retorno Anualizado: -4.34%
- Volatilidade: 13.28%

A diferença de desempenho entre o período de treinamento e avaliação indica um possível overfitting aos dados históricos, ou uma mudança no regime de mercado após dezembro. Esse resultado reforça a importância de validar modelos de alocação em diferentes períodos de tempo para verificar sua robustez fora da amostra.
