namespace Assist.July._2022.BE2.Application.Dtos.Blob
{
    public class BlobResponse
    {
        public string? Status { get; set; }
        public bool Error { get; set; }
        public BlobFile Blob { get; set; }
        public BlobResponse()
        {
            Blob = new BlobFile();
        }
    }
}
