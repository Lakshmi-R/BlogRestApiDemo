using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Author id must be greater than 0.")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
