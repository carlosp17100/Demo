@"
## Resumen
Describe qué cambia y por qué.

## Checklist
- [ ] CI verde (build + tests)
- [ ] 2 aprobaciones (incluye Maintainer – CODEOWNERS)
- [ ] Docs/Swagger/Postman actualizados si aplica
- [ ] No rompe compatibilidad (o explica mitigación)

## Evidencia
Logs, capturas, link a issue/story.

## Rollback
Cómo revertir en caso de problema.
"@ | Out-File -Encoding utf8 .github\pull_request_template.md
