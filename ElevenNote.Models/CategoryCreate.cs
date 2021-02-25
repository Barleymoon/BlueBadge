using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryCreate
    {
        [Required]
        [MinLength (1, ErrorMessage ="Title must be more than 1 character.")]
        [MaxLength (50, ErrorMessage ="Too many characters for a Title.")]
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Content { get; set; }
    }
}
