using System.Text.Json.Serialization;

namespace APPLICATION.DOMAIN.DTOS.RESPONSE.OPENAPI;

/// <summary>
/// Response da Api de completions do chatGPT.
/// </summary>
public class OpenAiCompletionsResponse
{
    /// <summary>
    /// Id de retorno da API.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// Tipo de objeto.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; }

    /// <summary>
    /// Valor de create.
    /// </summary>
    [JsonPropertyName("created")]
    public int Created { get; set; }

    /// <summary>
    /// Tipo de modelo de texto.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; }

    /// <summary>
    /// Lista de escolhas.
    /// </summary>
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; }

    /// <summary>
    /// Dados de uso da API.
    /// </summary>
    [JsonPropertyName("usage")]
    public Usage Usages { get; set; }

    /// <summary>
    /// Dados de escolhas.
    /// </summary>
    public class Choice
    {
        /// <summary>
        /// Texto
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; }

        /// <summary>
        /// Log
        /// </summary>
        [JsonPropertyName("logprobs")]
        public object LogProbs { get; set; }

        /// <summary>
        /// razão de finalização
        /// </summary>
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }

    /// <summary>
    /// Dados de uso.
    /// </summary>
    public class Usage
    {
        /// <summary>
        /// Quantidade de tokens usados.
        /// </summary>
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }

        /// <summary>
        /// Quantidade de tokens de competions.
        /// </summary>
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }

        /// <summary>
        /// Total de tokens.
        /// </summary>
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
