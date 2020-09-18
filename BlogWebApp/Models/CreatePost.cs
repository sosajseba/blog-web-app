using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogWebApp.Models
{
    public class CreatePost
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
        [Display(Name = "Imagen")]
        public string Image { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        public category Category { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime CreationDate { get; set; }
    }
}