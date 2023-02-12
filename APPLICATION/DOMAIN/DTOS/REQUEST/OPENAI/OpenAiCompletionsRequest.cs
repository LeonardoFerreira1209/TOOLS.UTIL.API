using System.Text.Json.Serialization;

namespace APPLICATION.DOMAIN.DTOS.REQUEST.OPENAPI;

/// <summary>
/// Classe response por ter os dados de envio para API de Completions do OpenApi.
/// </summary>
public class OpenAiCompletionsRequest
{
    /// <summary>
    /// ID do modelo a ser usado. Você pode usar a API de modelos de lista para ver todos os seus modelos disponíveis ou consultar nossa visão geral do modelo para obter as descrições deles.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; }

    /// <summary>
    /// Os prompts para gerar conclusões, codificados como uma string, array de strings, array de tokens ou array de token arrays.
    /// Observe que<|endoftext|> é o separador de documento que o modelo vê durante o treinamento, portanto, se um prompt não for especificado, o modelo será gerado como se estivesse no início de um novo documento.
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; }

    /// <summary>
    /// O sufixo que vem após a conclusão do texto inserido.
    /// </summary>
    [JsonPropertyName("suffix")]
    public string Suffix { get; set; } = null;

    /// <summary>
    /// O número máximo de tokens a serem gerados na conclusão.
    /// </summary>
    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; } = 16;

    /// <summary>
    /// Qual temperatura de amostragem usar. Valores mais altos significam que o modelo assumirá mais riscos. 
    /// Tente 0,9 para aplicativos mais criativos e 0 (amostragem argmax) para aqueles com uma resposta bem definida.
    /// </summary>
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; } = 1;

    /// <summary>
    /// Uma alternativa para amostragem com temperatura, chamada de amostragem de núcleo, onde o modelo considera os resultados dos tokens com massa de probabilidade top_p. Portanto, 0,1 significa que apenas os tokens que compõem a massa de probabilidade de 10% são considerados.
    /// Geralmente recomendamos alterar isso ou temperature, mas não ambos.
    /// </summary>
    [JsonPropertyName("top_p")]
    public double Topp { get; set; } = 1;

    /// <summary>
    /// Quantas conclusões gerar para cada prompt.
    /// Observação: como esse parâmetro gera muitas conclusões, ele pode consumir rapidamente sua cota de token.Use com cuidado e certifique-se de ter configurações razoáveis ​​para max_tokense stop.
    /// </summary>
    [JsonPropertyName("n")]
    public int N { get; set; } = 1;

    /// <summary>
    /// Se o progresso parcial deve ser transmitido de volta. Se definido, os tokens serão enviados como eventos enviados pelo servidor apenas de dados à medida que forem disponibilizados, com o fluxo finalizado por uma data: [DONE]mensagem.
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream { get; set; } = false;

    /// <summary>
    /// Inclua as probabilidades de log nos logprobstokens mais prováveis, bem como os tokens escolhidos. Por exemplo, se logprobsfor 5, a API retornará uma lista dos 5 tokens mais prováveis. A API sempre retornará o logprobtoken amostrado, portanto, pode haver até logprobs+1elementos na resposta.
    /// O valor máximo para logprobsé 5. Se precisar de mais do que isso, entre em contato conosco por meio de nossa Central de Ajuda e descreva seu caso de uso.
    /// </summary>
    [JsonPropertyName("logprobs")]
    public int? LogProbs { get; set; } = null;

    /// <summary>
    /// Retorne o prompt além da conclusão
    /// </summary>
    [JsonPropertyName("echo")]
    public bool Echo { get; set; } = false;

    /// <summary>
    /// Até 4 sequências em que a API deixará de gerar mais tokens. O texto retornado não conterá a sequência de parada.
    /// </summary>
    [JsonPropertyName("stop")]
    public List<string> Stop { get; set; }

    /// <summary>
    /// Número entre -2,0 e 2,0. Valores positivos penalizam novos tokens com base em sua presença no texto até o momento, aumentando a probabilidade do modelo falar sobre novos tópicos.
    /// </summary>
    [JsonPropertyName("presence_penalty")]
    public double PresencePenalty { get; set; } = 0;

    /// <summary>
    /// Gera best_of conclusões do lado do servidor e retorna o "melhor" (aquele com a maior probabilidade de log por token). Os resultados não podem ser transmitidos.
    /// Quando usado com n, best_of controla o número de conclusões de candidatos e nespecifica quantos devem ser retornados – best_ofdeve ser maior que n.
    /// Observação: como esse parâmetro gera muitas conclusões, ele pode consumir rapidamente sua cota de token. Use com cuidado e certifique-se de ter configurações razoáveis ​​para max_tokense stop.
    /// </summary>
    [JsonPropertyName("best_of")]
    public int BestOf { get; set; } = 1;

    /// <summary>
    /// Um identificador exclusivo que representa seu usuário final, que pode ajudar a OpenAI a monitorar e detectar abusos.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; set; } = null;
}
