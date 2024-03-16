using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Domain.Models.Filter
{
    public class RootFilter
    {
        public List<Filter>? Filters { get; set; }
        public string? Logic { get; set; }
        
    }
}
