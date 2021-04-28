using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

#nullable disable

namespace DatingProjekt.Models
{//Användare profil
    public partial class User
    {
        public User()
        {
            FriendRequestUserPendingNavigations = new HashSet<FriendRequest>();
            FriendRequestUserSentNavigations = new HashSet<FriendRequest>();
            FriendUser1Navigations = new HashSet<Friend>();
            FriendUser2Navigations = new HashSet<Friend>();
            PostsProfile = new HashSet<Post>();
        }

        [Key]
        [Column("dat_user_id")]
        [Required]
        public string UserId { get; set; }
        [Column("dat_user_name")]
        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; }
        [Column("dat_user_bir_dat")]
        [Required]
        [Display(Name = "Födelsedatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthday { get; set; }
        [Column("dat_user_gen")]
        [Required]
        [Display(Name = "Kön")]
        public string Gender { get; set; }
        [Column("dat_user_ori")]
        [Required]
        [Display(Name = "Sexuell läggning")]
        public string Orientation { get; set; }
        [Column("dat_user_vis_sea")]
        [Required]
        [Display(Name = "Synlig vid sökning?")]
        public string VisibleSearch { get; set; }

        [Column("dat_user_ima_pat")]
        [Required]
        [Display(Name = "Profilbild")]
        public string ImagePath { get; set; }    
        [NotMapped]
        [Required]
        [Display(Name = "Profilbild")]
        public IFormFile FileName { get; set; }

        public virtual ICollection<FriendRequest> FriendRequestUserPendingNavigations { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestUserSentNavigations { get; set; }
        public virtual ICollection<Friend> FriendUser1Navigations { get; set; }
        public virtual ICollection<Friend> FriendUser2Navigations { get; set; }
        public virtual ICollection<Post> PostsProfile { get; set; }

        //public static implicit operator string(User v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
