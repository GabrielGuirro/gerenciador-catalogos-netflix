# Gerenciador de Catálogos Netflix

Este projeto gerencia catálogos de filmes e séries da Netflix usando Azure Functions e Cosmos DB.

## Funcionalidades
- Upload de arquivos para o Azure Blob Storage.
- Armazenamento de catálogos no Cosmos DB.
- Filtros e listagem de catálogos.

## Como Rodar Localmente
1. Clone este repositório.
2. Instale as dependências (se houver).
3. Configure as funções Azure com as credenciais corretas no arquivo `local.settings.json`.
4. Inicie as funções Azure localmente com o comando:
   ```bash
   func start

Como Usar
Upload de Arquivos: Envie um arquivo de catálogo usando a função UploadArquivo.
Salvar no Cosmos DB: Use a função SalvarNoCosmosDB para salvar os dados no banco.
Filtrar Catálogos: Use a função FiltrarCatalogosCosmos para filtrar catálogos por gênero ou ano.
Listar Catálogos: A função ListarCatalogosCosmos retorna todos os catálogos armazenados no Cosmos DB.
