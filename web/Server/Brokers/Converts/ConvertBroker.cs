namespace FMFT.Web.Server.Brokers.Converts
{
    public class ConvertBroker : IConvertBroker
    {
        public async ValueTask<byte[]> GetBytesFromFormFileAsync(IFormFile formFile)
        {
            await using MemoryStream memoryStream = new();
            await formFile.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
