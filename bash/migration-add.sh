#!/bin/bash

echo "Adicionar uma nova migração"

# Defina as variáveis de contexto e caminhos dos projetos
_context='DataContextSolution'
_service='../2-Service/LearnLogic.Services/LearnLogic.Services.csproj'
_infra='../5-Infra/5.1-Data/LearnLogic.Infra.Data/LearnLogic.Infra.Data.csproj'

# Nome da nova migração
MIGRATION_NAME=$1

if [ -z "$MIGRATION_NAME" ]; then
    echo "Por favor, forneça um nome para a migração."
    exit 1
fi

# Adiciona a nova migração
dotnet ef migrations add "$MIGRATION_NAME" --context $_context --project $_infra --startup-project $_service

# Verifica se o comando foi bem-sucedido
if [ $? -eq 0 ]; then
    echo "Migração '$MIGRATION_NAME' adicionada com sucesso."
else
    echo "Falha ao adicionar a migração."
    exit 1
fi
