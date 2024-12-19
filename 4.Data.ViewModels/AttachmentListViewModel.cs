namespace _4.Data.ViewModels;

public class AttachmentListViewModel
{

}

public class FileReady
{
    public required MemoryStream FileStream { get; set; }
    public required string FileName { get; set; }
}

public class UploadFileSetting
{
    public string? AttachmentFolder { get; set; }
    public string? ExtensionAllowed { get; set; }
    public string? ContentTypeAllowed { get; set; }
}
