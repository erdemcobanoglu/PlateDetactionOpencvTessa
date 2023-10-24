using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface ICache
    {
        void Add(string key, object value, TimeSpan expiration);
        object Get(string key);
        void Remove(string key);
    }
}
