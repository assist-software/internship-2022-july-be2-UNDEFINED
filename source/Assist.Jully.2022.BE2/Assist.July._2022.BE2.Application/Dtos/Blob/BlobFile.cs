namespace Assist.July._2022.BE2.Application.Dtos.Blob
{
    public class BlobFile
    {
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Content { get; set; }
    }
}
