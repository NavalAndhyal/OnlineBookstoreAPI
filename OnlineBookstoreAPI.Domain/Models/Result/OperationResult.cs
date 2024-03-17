using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Domain.Models.Result
{
    public class OperationResult<T> where T : class
    {
        public OperationResult(int totalRecords, int totalPages, int currentPage, bool hasNext, bool hasPrevious, List<T> records)
        {
            TotalRecords = totalRecords;
            TotalPages = totalPages;
            CurrentPage = currentPage;
            HasNext = hasNext;
            HasPrevious = hasPrevious;
            Records = records;
        }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public List<T> Records { get; set; }
    }
}
