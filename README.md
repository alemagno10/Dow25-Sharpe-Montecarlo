# Dow25-Sharpe-Montecarlo

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

- `DataLoader.fs`: Lê e transforma os dados de preços em matriz.
- `MathTools.fs`: Ferramentas matemáticas como desvio padrão, média e multiplicação por pesos.
- `Simulate.fs`: Funções que realizam simulações de portfólios.
- `Program.fs`: Ponto de entrada principal que executa as funções de simulação.
- `data/`: Pasta com os dados CSV de preços históricos das ações.

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

1. Download dos dados via Python:
    ```bash
    python -m venv env
    env/bin/activate
    python fech_data.py

1. Executar Scripts F#
    ```bash
    dotnet run --project SharpRatio

## Observações
O índice de Sharpe é calculado como:
Sharpe = (Retorno Anualizado - Taxa Livre de Risco) / Volatilidade Anualizada

Neste projeto, a taxa livre de risco é considerada como 0.

Os dados utilizados são de Agosto a Dezembro de 2024.

