@"
name: Feature request
description: Solicitar una mejora o feature
title: "[Feat] "
labels: ["enhancement"]
body:
  - type: textarea
    attributes: { label: Objetivo, description: ¿Qué problema resuelve? }
  - type: textarea
    attributes: { label: Alcance, description: ¿Qué incluye? ¿Qué no? }
  - type: textarea
    attributes: { label: Criterios de aceptación, description: Given/When/Then }
"@ | Out-File -Encoding utf8 .github\ISSUE_TEMPLATE\feature_request.md
