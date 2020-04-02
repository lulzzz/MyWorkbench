namespace Ignyt.BusinessInterface.Communication
{
    public interface IEmailAttachment
    {
        string Content { get; }
        string Type { get; }
        string FileName { get; }
    }
}
