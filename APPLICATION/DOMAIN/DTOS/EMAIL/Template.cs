namespace APPLICATION.DOMAIN.DTOS.EMAIL;

public class Template
{
    public static async Task<string> TemplateWelcome(string titulo, string conteudoTexto, string linkBotao, string textoBotao)
    {
        var conteudo = File.ReadAllText("E:/PROJETOS/PROJETO GRAPHQL HOT CHOCOLATE/GRAPHQL HOT CHOCOLATE/TOOLS.MAIL.API/TOOLS.MAIL.API/wwwroot/email/templates/Welcome.html");

        return await Task.FromResult(conteudo.Replace("__titulo__", titulo).Replace("__content__", conteudoTexto).Replace("__link-botao__", linkBotao).Replace("__texto-botao__", textoBotao));
    }
}
