using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DatingProjekt.DataLayer.Repositories;
using DatingProjekt.Models;
using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace DataLayer.Repositories
{
    class FriendRequestRepository
    {
        public List<FriendRequest> GetSentById(string userId)
        {
            using (var cntx = new DatingDBContext())
            {
                var request = cntx.Set<FriendRequest>().Where(x => x.UserSent.Equals(userId));
                return request.ToList();
            }
        }

        public List<FriendRequest> GetPendingById(string userId)
        {
            using (var cntx = new DatingDBContext())
            {
                var request = cntx.Set<FriendRequest>().Where(x => x.UserPending.Equals(userId));
                return request.ToList();
            }
        }

        public FriendRequest Add(FriendRequest request)
        {
            using (var cntx = new DatingDBContext())
            {
                var search = cntx.Set<FriendRequest>().Any(x => x.UserSent.Equals(request)
                || x.UserPending.Equals(request));
                if (!search)
                {
                    cntx.Entry<FriendRequest>(request).State = EntityState.Added;
                    cntx.SaveChanges();
                }
                return request;
            }
        }

        public FriendRequest Delete(FriendRequest request)
        {
            using (var cntx = new DatingDBContext())
            {
                cntx.FriendRequests.Remove(request);
                cntx.SaveChanges();

                return request;
            }
        }

        public List<FriendRequest> GetAll(string userId)
        {
            using (var cntx = new DatingDBContext())
            {
                var request = cntx.Set<FriendRequest>().Where(x => x.UserPending.Equals(userId));
                return request.ToList();
            }
        }
        public bool Exists(string userId, string friendUserId)
        {
            using (var _ctx = new DatingDBContext())
            {
                var friends = _ctx.Set<FriendRequest>().Where(x => (x.UserSent == userId && x.UserPending == friendUserId) || (x.UserSent == friendUserId && x.UserPending == userId));
                return friends.FirstOrDefault() != null;
            }
        }
    }
}
