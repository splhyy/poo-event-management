# Sistema de Gerenciamento de Eventos

## 📋 Descrição
Sistema completo para gerenciamento de eventos, palestrantes e locais, desenvolvido em C# com foco em programação defensiva e null safety. Este projeto foi desenvolvido como trabalho acadêmico para a disciplina de Programação Orientada a Objetos.

## 🚀 Tecnologias Utilizadas
- .NET 9.0
- C# 13
- xUnit para testes unitários
- Git para controle de versão

## 🏗️ Arquitetura
O projeto segue uma arquitetura em camadas com separação clara de responsabilidades:

## 🧪 Testes
O projeto conta com **65 testes unitários** cobrindo:

✅ Validações de entrada com Guard Clauses  
✅ Comportamento de propriedades com [AllowNull] e [DisallowNull]  
✅ Lazy loading com [MemberNotNull]  
✅ Métodos de identidade (Equals e GetHashCode)  
✅ Cenários válidos e inválidos  

Para executar os testes:
```bash
dotnet test --verbosity normal

- EventManagement.Domain: Camada de domínio com entidades e regras de negócio
- EventManagement.Console: Interface de linha de comando para demonstração
- EventManagement.Domain.Tests: Testes unitários para validação do domínio

📚 Conceitos Aplicados
✅ Guard Clauses para validação defensiva

✅ TryParseNonEmpty para validação sem exceções

✅ [MemberNotNull] para lazy loading seguro

✅ [DisallowNull] e [AllowNull] para controle de nullability

✅ Métodos de identidade (Equals/GetHashCode)

✅ Testes unitários abrangentes (65 testes)

✅ Programação orientada a objetos

✅ Princípios SOLID

🎯 Entidades Implementadas
Speaker (Palestrante)
Validações: ID positivo, nome não vazio, email válido

Propriedades: Biography (opcional), Company ([AllowNull]), LinkedInProfile ([AllowNull])

Venue (Local)
Validações: ID positivo, nome não vazio, endereço não vazio, capacidade positiva

Propriedade estática Default para eventos online

Event (Evento)
Validações: ID positivo, título não vazio, data futura, duração mínima

Lazy loading de Venue com [MemberNotNull]

EventCode com [DisallowNull]

Requirements e Notes com [AllowNull]

👤 Autor
Shara Palharini Lima - Estudante de Ciência da Computação

## ⚙️ Como Executar

### Pré-requisitos
- .NET SDK 9.0+
- Git

### Passos
```bash
# Clonar repositório
git clone https://github.com/splhyy/poo-event-management.git
cd poo-event-management

# Restaurar dependências
dotnet restore

# Executar testes
dotnet test

# Executar aplicação
dotnet run --project src/EventManagement.Console

Desenvolvido como parte do curso de Programação Orientada a Objetos - 2025
