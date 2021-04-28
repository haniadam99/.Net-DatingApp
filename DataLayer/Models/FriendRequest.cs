using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DatingProjekt.Models
{// användare blir vänner genom vänförfrågan
    public partial class FriendRequest
    {
        [Key]
        [ForeignKey("user")]
        [Column("dat_fri_req_user_sent")]
        [Required]
        public string UserSent { get; set; }
        [Key]
        [ForeignKey("user")]
        [Column("dat_fri_req_user_pending")]
        [Required]
        public string UserPending { get; set; }

        public virtual User UserPendingNavigation { get; set; }
        public virtual User UserSentNavigation { get; set; }
    }
}
