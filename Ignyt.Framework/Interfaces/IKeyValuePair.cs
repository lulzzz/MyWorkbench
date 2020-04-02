using System.Collections.Generic;

namespace Ignyt.Framework.Interfaces
{
    public interface IKeyValuePair<T, V>
    {
        KeyValuePair<T, V> KeyValuePair { get; }
    }
}
