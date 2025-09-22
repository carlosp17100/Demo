@"
# Contribuir

## Flujo (Trunk-Based Development)
- Rama corta desde \`main\`: \`feature/*\`, \`fix/*\`, \`chore/*\`
- PR pequeño → CI verde → 2 aprobaciones (incluye Maintainer) → **Squash merge**
- Mantener PR actualizado con \`main\`

## Convenciones
- .NET 8 LTS
- Commits: Conventional Commits (feat, fix, chore, docs, test)
- Estilo: .editorconfig

## Cómo correr
- \`dotnet restore && dotnet build\`
- \`dotnet test -c Release\`

## Calidad
- Cobertura objetivo ≥ 80% (coverlet)
- Agregar/actualizar Swagger & Postman si cambian endpoints

## Seguridad
- No exponer secretos; usar GitHub Secrets/Environments
"@ | Out-File -Encoding utf8 CONTRIBUTING.md
