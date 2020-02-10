using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Entities
{

    public class tblBlog
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name can`t exceed 100 chareccters.")]
        public string Name { get; set; }
        [Required]
        public string PrewText { get; set; }
        [Required]
        public string FullText { get; set; }
        //[Required]
        public string Img { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Name can`t exceed 30 chareccters.")]
        public string Author { get; set; }
        [Required]
        public DateTime Date { get; set; }
  
    }
}
