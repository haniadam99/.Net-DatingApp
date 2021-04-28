using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DatingProjekt.Models
{//Posts, använvds för inlägg både på egen profil och en väns profil 
    public partial class Post
    {
        public Post()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        [ForeignKey("user")]
        public string Author { get; set; }
        [ForeignKey("user")]
        public string Profile { get; set; }
        public virtual User ProfileNavigation { get; set; }
    }
}
