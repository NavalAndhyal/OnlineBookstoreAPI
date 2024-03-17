using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Domain.Models.Filter
{
    public class SortingUtility
    {
        public List<SortingParams>? SortingParams { get; set; }
    }
    public enum SortOrders
    {
        Asc = 1,
        Desc = 2
    }
    public class SortingParams
    {
        public SortOrders SortOrder { get; set; } = SortOrders.Asc;
        public string ColumnName { get; set; }
    }
}
