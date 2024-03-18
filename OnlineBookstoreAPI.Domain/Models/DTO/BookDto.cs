using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Domain.Models.DTO
{
    public class BookDto
    {
        public string BookId { get; set; }
        public string? BookTitle { get; set; }
        public string? BookAuthor { get; set; }
        public short? YearOfPublication { get; set; }
        public string? Publisher { get; set; }
        public string? ImageUrlS { get; set; }
        public string? ImageUrlM { get; set; }
        public string? ImageUrlL { get; set; }
    }
}
