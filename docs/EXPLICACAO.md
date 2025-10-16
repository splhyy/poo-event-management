\# Documentação Técnica - Sistema de Gerenciamento de Eventos



\## Seção 1: Introdução



\### Problema Resolvido

O sistema gerencia eventos, palestrantes e locais, aplicando técnicas avançadas de programação defensiva para garantir a robustez e confiabilidade da aplicação. O foco principal é prevenir erros comuns como `NullReferenceException` e validar dados de entrada antes do processamento.



\### Decisões de Design

\- Fail-Fast Principle: Validações ocorrem no construtor para detectar erros o mais cedo possível

\- Null Safety: Uso extensivo de nullable reference types e atributos de análise

\- Separation of Concerns: Cada classe tem responsabilidade bem definida

\- Imutabilidade: Propriedades essenciais são readonly após construção



\## Seção 2: Guard Clauses Implementados



\### Guard.AgainstNull

```csharp

public static void AgainstNull<T>(\[NotNull] ref T? value, string paramName)

&nbsp;   where T : class

{

&nbsp;   if (value is null)

&nbsp;       throw new ArgumentNullException(paramName, $"{paramName} cannot be null.");

}





Uso no Speaker:



public Speaker(int speakerId, string fullName, string email)

{

&nbsp;   Guard.AgainstNull(ref fullName!, nameof(fullName));

&nbsp;   // Continua apenas se fullName não for null

}



Guard.AgainstNegativeOrZero

Valida identificadores numéricos para garantir que são positivos:



public static void AgainstNegativeOrZero(int value, string paramName)

{

&nbsp;   if (value <= 0)

&nbsp;       throw new ArgumentOutOfRangeException(paramName, 

&nbsp;           $"{paramName} must be greater than zero.");

}



Guard.AgainstPastDate

Impede o agendamento de eventos em datas passadas:



public static void AgainstPastDate(DateTime date, string paramName)

{

&nbsp;   if (date < DateTime.Now)

&nbsp;       throw new ArgumentException($"{paramName} cannot be in the past.", paramName);

}



Seção 3: TryParseNonEmpty

Implementação



public static bool TryParseNonEmpty(string? s, \[NotNullWhen(true)] out string? result)

{

&nbsp;   if (!string.IsNullOrWhiteSpace(s)) 

&nbsp;   { 

&nbsp;       result = s; 

&nbsp;       return true; 

&nbsp;   }

&nbsp;   result = null; 

&nbsp;   return false;

}



Uso em SetDescription



public void SetDescription(string? description)

{

&nbsp;   if (Guard.TryParseNonEmpty(description, out string? validDescription))

&nbsp;   {

&nbsp;       \_description = validDescription;

&nbsp;   }

&nbsp;   else

&nbsp;   {

&nbsp;       \_description = null;

&nbsp;   }

}



Vantagens:



Evita exceções para casos comuns (strings vazias)



Performance melhorada (exceções são custosas)



Código mais limpo e expressivo



Seção 4: \[MemberNotNull] - Lazy Loading

Implementação no Event.Venue



private Venue? \_venue;



\[MemberNotNull(nameof(\_venue))]

public Venue Venue

{

&nbsp;   get

&nbsp;   {

&nbsp;       EnsureVenue();

&nbsp;       return \_venue;

&nbsp;   }

}



\[MemberNotNull(nameof(\_venue))]

private void EnsureVenue()

{

&nbsp;   \_venue ??= Venue.Default;

}



Benefícios

Carregamento Sob Demanda: Venue só é carregado quando acessado pela primeira vez



Segurança: Compilador garante que \_venue não será null após EnsureVenue()



Performance: Evita criação desnecessária de objetos



Manutenibilidade: Código mais limpo e fácil de entender



Seção 5: \[DisallowNull] vs \[AllowNull]

\[DisallowNull] em EventCode



\[DisallowNull]

public string EventCode

{

&nbsp;   get => \_eventCode;

&nbsp;   set => SetEventCode(value);

}



Comportamento:



Setter nunca aceita null (validação no SetEventCode)



Getter nunca retorna null (inicializado com string vazia)



Semântica de "valor obrigatório"



\[AllowNull] em Notes



\[AllowNull]

public string Notes

{

&nbsp;   get => \_notes ?? string.Empty;

&nbsp;   set => \_notes = value ?? string.Empty;

}



Comportamento:



Setter aceita null (converte para string vazia)



Getter nunca retorna null



Semântica de "valor opcional"



Comparação Prática

Atributo	Setter	Getter	Uso

\[DisallowNull]	Não aceita null	Nunca retorna null	Valores obrigatórios

\[AllowNull]	Aceita null	Nunca retorna null	Valores opcionais

Seção 6: Métodos de Identidade

Implementação em Speaker



public override bool Equals(object? obj)

{

&nbsp;   return obj is Speaker speaker \&\& SpeakerId == speaker.SpeakerId;

}



public override int GetHashCode()

{

&nbsp;   return SpeakerId.GetHashCode();

}



Importância

Comparação Correta: Permite comparação lógica entre objetos baseada no ID



Coleções: Necessário para uso correto em Dictionary, HashSet, etc.



Performance: HashCode otimizado para buscas em coleções



Consistência: Garante que dois objetos com mesmo ID sejam considerados iguais



Seção 7: Validações Customizadas

IsValidEmail



public static bool IsValidEmail(string? email)

{

&nbsp;   return !string.IsNullOrWhiteSpace(email) \&\& email.Contains('@');

}



Abordagem: Validação básica mas eficaz para o contexto acadêmico.



AgainstPastDate



public static void AgainstPastDate(DateTime date, string paramName)

{

&nbsp;   if (date < DateTime.Now)

&nbsp;       throw new ArgumentException($"{paramName} cannot be in the past.", paramName);

}



Seção 8: Testes Unitários

Estratégia de Testes

Testes de Estado: Verificam o estado dos objetos após operações



Testes de Exceção: Validam que exceções são lançadas corretamente



Testes de Comportamento: Verificam interações entre objetos



Teoria vs Fact: Uso de Theory para múltiplos casos de teste similares



Exemplo de Teste Completo



\[Fact]

public void Constructor\_ComDadosValidos\_DeveCriarSpeaker()

{

&nbsp;   // Arrange \& Act

&nbsp;   var speaker = new Speaker(1, "John Doe", "john@email.com");



&nbsp;   // Assert

&nbsp;   Assert.Equal(1, speaker.SpeakerId);

&nbsp;   Assert.Equal("John Doe", speaker.FullName);

&nbsp;   Assert.Equal("john@email.com", speaker.Email);

}



Cobertura de Testes

Speaker: 15 testes cobrindo todos os cenários



Venue: 12+ testes com foco em validações



Event: 18+ testes incluindo lazy loading



Total: 65 testes (superando a meta de 45)



Seção 9: Desafios Encontrados

1\. Lazy Loading com MemberNotNull

Problema: Garantir que o compilador entendesse que \_venue não seria null após EnsureVenue().



Solução: Uso do atributo \[MemberNotNull] no método EnsureVenue() para informar ao analisador de fluxo.



2\. Validação de Email

Problema: Balancear simplicidade com robustez na validação.



Solução: Implementação básica com verificação de '@', deixando validação mais complexa como possível melhoria futura.



3\. Venue.Default com ID Zero

Problema: O Venue.Default usava ID 0, mas a validação não permitia zero.



Solução: Alteração para ID 1, mantendo a semântica de objeto padrão.



4\. Diferença entre ArgumentNullException e ArgumentException

Problema: Testes falhando devido a diferença no tipo de exceção lançada.



Solução: Adaptação dos testes para verificar o tipo correto de exceção baseado no cenário.



Seção 10: Conclusão

Resumo da Implementação

✅ Todas as classes implementadas com validações robustas



✅ 65 testes unitários com cobertura abrangente



✅ Aplicação de todos os conceitos de programação defensiva



✅ Documentação completa e clara



✅ Programa de demonstração funcional



Conceitos Consolidados

Programação defensiva com Guard Clauses



Null safety com atributos do C# moderno



Lazy loading patterns com MemberNotNull



Test-driven development com xUnit



Princípios de design orientado a objetos



Próximos Passos Possíveis

Implementar persistência em banco de dados



Adicionar interface web com ASP.NET Core



Implementar autenticação e autorização



Adicionar mais validações de negócio



Implementar padrão Repository para acesso a dados



Documentação técnica criada para fins educacionais - 2025











