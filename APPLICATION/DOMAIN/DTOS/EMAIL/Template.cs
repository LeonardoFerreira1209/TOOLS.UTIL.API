namespace APPLICATION.DOMAIN.DTOS.EMAIL;

public class Template
{
    public static async Task<string> GetBaseTemplate(string titulo, string conteudoTexto, string linkBotao, string textoBotao)
    {
        var conteudo = @"  
            <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional //EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:v='urn:schemas-microsoft-com:vml' lang='en'>
  
              <head><link rel='stylesheet' type='text/css' hs-webfonts='true' href='https://fonts.googleapis.com/css?family=Lato|Lato:i,b,bi'>
                <title>Email template</title>
                <meta property='og:title' content='Email template'>
    
            <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>

            <meta http-equiv='X-UA-Compatible' content='IE=edge'>

            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    
                <style type='text/css'>
   
                  a{ 
                    text-decoration: underline;
                    color: inherit;
                    font-weight: bold;
                    color: #253342;
                  }
      
                  h1 {
                    font-size: 56px;
                  }
      
                    h2{
                    font-size: 28px;
                    font-weight: 900; 
                  }
      
                  p {
                    font-weight: 100;
                  }
      
                  td {
                vertical-align: top;
                  }
      
                  #email {
                    margin: auto;
                    width: 600px;
                    background-color: white;
                  }
      
                  button{
                    font: inherit;
                    background-color: #FF7A59;
                    border: none;
                    padding: 10px;
                    text-transform: uppercase;
                    letter-spacing: 2px;
                    font-weight: 900; 
                    color: white;
                    border-radius: 5px; 
                    box-shadow: 3px 3px #d94c53;
                  }
      
                  .subtle-link {
                    font-size: 9px; 
                    text-transform:uppercase; 
                    letter-spacing: 1px;
                    color: #CBD6E2;
                  }
      
                </style>
    
              </head>
    
                <body bgcolor='#F5F8FA' style='width: 100%; margin: auto 0; padding:0; font-family:Lato, sans-serif; font-size:18px; color:#33475B; word-break:break-word'>
  
             <! View in Browser Link --> 
      
            <div id='email'>
                  <table align='right' role='presentation'>
                    <tr>
                      <td>
                      <a class='subtle-link' href='#'>View in Browser</a>
                      </td>
                      <tr>
                  </table>
  
  
              <! Banner --> 
                     <table role='presentation' width='100%'>
                        <tr>
         
                          <td bgcolor='#00A4BD' align='center' style='color: white;'>
            
                         <img alt='Flower' src='https://hs-8886753.f.hubspotemail.net/hs/hsstatic/TemplateAssets/static-1.60/img/hs_default_template_images/email_dnd_template_images/ThankYou-Flower.png' width='400px' align='middle'>
                
                            <h1> Welcome! </h1>
                
                          </td>
                    </table>
                <! First Row --> 
              <table role='presentation' border='0' cellpadding='0' cellspacing='10px' style='padding: 30px 30px 30px 60px; width: 100%;'>
                 <tr style='text-align: center;'>
                   <td>
                    <h2>__titulo__</h2>
                        <p>
                         __content__
                        </p>
                            <a href='__link-botao__' target='_blank'><button style='cursor: pointer;'>__texto-botao__</button></a>
                      </td> 
                      </tr>
                      </table>
                  </div>
                </body>
                  </html>";

        return await Task.FromResult(conteudo.Replace("__titulo__", titulo).Replace("__content__", conteudoTexto).Replace("__link-botao__", linkBotao).Replace("__texto-botao__", textoBotao));
    }
}
