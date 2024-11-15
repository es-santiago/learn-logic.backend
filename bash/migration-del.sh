#!/bin/bash

echo "Update database"
_context='DataContextSolution'
_service='../2-Service/LearnLogic.Services/LearnLogic.Services.csproj'
_infra='../5-Infra/5.1-Data/LearnLogic.Infra.Data/LearnLogic.Infra.Data.csproj'

dotnet ef migrations remove --context $_context --project $_infra --startup-project $_service