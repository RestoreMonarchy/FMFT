namespace FMFT.Web.Client.Brokers.MemoryStorages
{
    public partial class MemoryStorageBroker : IMemoryStorageBroker
    {
        private readonly Dictionary<string, object> memoryStorage;

        public MemoryStorageBroker()
        {
            this.memoryStorage = new Dictionary<string, object>();
        }

        private T GetValue<T>(string key)
        {
            object value = memoryStorage.GetValueOrDefault(key, default(T));
            return (T)value;
        }

        private void SetValue<T>(string key, T value)
        {
            memoryStorage[key] = value;
        }
    }
}
