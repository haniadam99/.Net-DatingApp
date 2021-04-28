using DataLayer.Repositories;
using DatingProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DatingProjekt.DataLayer.Repositories
{
    public class FriendRepository
    {
        public Friend GetById(string currentUser, string friendId)
        {
            using (var cntx = new DatingDBContext())
            {
                var friend = cntx.Set<Friend>().Where(x => x.User1.Equals(friendId) && x.User2.Equals(currentUser));
                return friend.FirstOrDefault();
            }
        }

        public List<Friend> GetAll(string userId)
        {
            using (var cntx = new DatingDBContext())
            {
                var friends = cntx.Set<Friend>().Where(x => x.User1.Equals(userId) || x.User2.Equals(userId));
                return friends.ToList();
            }
        }

        public Friend Add(Friend friend)
        {
            using (var cntx = new DatingDBContext())
            {
                var search = cntx.Set<Friend>().Any(x => x.User1 == friend.User2
                || x.User2 == friend.User1);
                if (!search)
                {
                    cntx.Entry<Friend>(friend).State = EntityState.Added;
                    cntx.SaveChanges();
                }
                return friend;
            }
        }

        public Friend Delete(Friend friend)
        {
            using (var cntx = new DatingDBContext())
            {
                cntx.Entry(friend).State = EntityState.Deleted;
                cntx.SaveChanges();
                return friend;
            }
        }

        public bool AreFriends(string userId, string friendUserId)
        {
            using (var _ctx = new DatingDBContext())
            {
                var friends = _ctx.Set<Friend>().Where(x => (x.User1 == userId && x.User2 == friendUserId) || (x.User1 == friendUserId && x.User2 == userId));
                return friends.FirstOrDefault() != null;
            }
        }
    }  
}
