# F1 project by Guilherme Caetano — Estatísticas Reais de Fórmula 1 observadas através de uma API pública em tempo real.

Aplicativo de console em C# que consome dados **reais** de Fórmula 1
através de uma API pública (Jolpica-F1).

## Funcionalidades

- Classificação de pilotos por temporada
- Classificação de construtores (equipes) por temporada
- Busca de informações de um piloto específico
- Comparação entre dois pilotos (pontos, vitórias, posição)
- Resultados da última corrida realizada


## Tecnologias e conceitos usados

- Consumo de API REST com `HttpClient`
- Desserialização de JSON com `System.Text.Json`
- Programação assíncrona (`async`/`await`)
- Organização em camadas (Models / Services / Program)

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
