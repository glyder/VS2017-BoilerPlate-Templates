using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Data.API.App.Repository
{
    internal class ColumnMap
    {
        private readonly Dictionary<string, string> forward = new Dictionary<string, string>();
        private readonly Dictionary<string, string> reverse = new Dictionary<string, string>();

        public void Add(string t1, string t2)
        {
            forward.Add(t1, t2);
            reverse.Add(t2, t1);
        }
        public string this[string index]
        {
            get
            {
                // Check for a custom column map.
                if (forward.ContainsKey(index))
                    return forward[index];
                if (reverse.ContainsKey(index))
                    return reverse[index];

                // If no custom mapping exists, return the value passed in.
                return index;
            }
        }
    }
}
