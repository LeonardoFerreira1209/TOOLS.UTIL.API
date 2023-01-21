namespace APPLICATION.DOMAIN.DTOS.RESPONSE.FILE;

/// <summary>
/// Response de arquivo.
/// </summary>
public class FileResponse
{
    /// <summary>
    /// Url do arquivo gravado no blob.
    /// </summary>
    public string FileUri { get; set; }
}
