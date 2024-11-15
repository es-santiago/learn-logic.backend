#!/bin/bash

echo "Reverter uma migração"

# Defina as variáveis de contexto e caminhos dos projetos
_context='DataContextSolution'
_service='../2-Service/LearnLogic.Services/LearnLogic.Services.csproj'
_infra='../5-Infra/5.1-Data/LearnLogic.Infra.Data/LearnLogic.Infra.Data.csproj'

# Nome da migração para reverter
MIGRATION_NAME=$1

if [ -z "$MIGRATION_NAME" ]; then
    echo "Por favor, forneça o nome da migração para reverter."
    exit 1
fi

# Reverte a migração
dotnet ef database update "$MIGRATION_NAME" --context $_context --project $_infra --startup-project $_service

# Verifica se o comando foi bem-sucedido
if [ $? -eq 0 ]; then
    echo "Migração revertida com sucesso para o estado da migração '$MIGRATION_NAME'."
else
    echo "Falha ao reverter a migração."
    exit 1
fi