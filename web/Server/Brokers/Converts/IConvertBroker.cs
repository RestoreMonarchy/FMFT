namespace FMFT.Web.Server.Brokers.Converts
{
    public interface IConvertBroker
    {
        ValueTask<byte[]> GetBytesFromFormFileAsync(IFormFile formFile);
    }
}
