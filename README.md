# F1 Stats — Estatísticas Reais de Fórmula 1

Aplicativo de console em C# (.NET 8) que consome dados **reais** de Fórmula 1
através da API pública e gratuita [Jolpica-F1](https://github.com/jolpica/jolpica-f1)
(sucessora da antiga Ergast API).

## Funcionalidades

- Classificação de pilotos por temporada
- Classificação de construtores (equipes) por temporada
- Busca de informações de um piloto específico
- Comparação entre dois pilotos (pontos, vitórias, posição)
- Resultados da última corrida realizada

## Como rodar

### Pelo Visual Studio 2022
1. Abra a pasta do projeto (`Abrir` → `Pasta...`) ou crie um novo projeto de
   Console App (.NET 8) e substitua os arquivos pelos deste repositório.
2. Pressione `F5` ou `Ctrl+F5` para rodar.

### Pelo terminal (com .NET SDK instalado)
```bash
cd F1Stats
dotnet run
```

## Requisitos

- .NET 8 SDK instalado
- Conexão com a internet (o programa faz requisições HTTP para a API)

## Tecnologias e conceitos usados

- Consumo de API REST com `HttpClient`
- Desserialização de JSON com `System.Text.Json`
- Programação assíncrona (`async`/`await`)
- Organização em camadas (Models / Services / Program)

## Fonte dos dados

Todos os dados vêm da API pública [Jolpica-F1](https://github.com/jolpica/jolpica-f1),
um projeto open source e gratuito, sucessor da Ergast API.


### Menu principal
![Menu principal](screenshots/opcoes-de-pesquisa.png)

### Classificação de pilotos por temporada
![Classificação de pilotos](screenshots/classificacao-por-temporada.png)

### Classificação de construtores por temporada
![Classificação de construtores](screenshots/classificacao-construtores-por-temporada.png)

### Busca de piloto específico
![Busca de piloto](screenshots/pesquisa-pessoal-piloto.png)

### Comparação entre pilotos
![Comparação de pilotos](screenshots/comparacao-pilotos.png)

### Resultado da última corrida
![Resultado da última corrida](screenshots/resultado-ultima-corrida.png)
