using System.Reflection;

namespace FMFT.Extensions.DataTables
{
    public partial class DataTable<TItem>
    {
        public Type ItemType => typeof(TItem);
        public PropertyInfo[] GetProperties() => ItemType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
    }
}
