using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DatingProjekt.Models
{
    public partial class Friend
    {//Visar om man är vän 
        [Key]
        [ForeignKey("user")]
        [Column("dat_fri_user1")]
        [Required]
        public string User1 { get; set; }
        [Key]
        [ForeignKey("user")]
        [Column("dat_fri_user2")]
        [Required]
        public string User2 { get; set; }

        public virtual User User1Navigation { get; set; }
        public virtual User User2Navigation { get; set; }
        public int Id { get; internal set; }
    }
}
