using System.ComponentModel;

public enum QuestionLevel
{
    [Description("Iniciante")]      // Sintaxe básica, operadores e conceitos fundamentais.
    Beginner,

    [Description("Intermediário")]  // Lógica de programação, estruturas de dados simples e algoritmos básicos.
    Intermediate,

    [Description("Avançado")]       // Problemas mais complexos, estruturas de dados avançadas e algoritmos complexos.
    Advanced,

    [Description("Especialista")]   // Conhecimento profundo de arquitetura de software, padrões de design e resolução avançada de problemas.
    Expert
}
