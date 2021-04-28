using DatingProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DataLayer.Repositories
{
	public class UserRepository
	{
		public User Add(User user)
		{
			using (var cntx = new DatingDBContext())
			{
				cntx.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Added;
				cntx.SaveChanges();
				return user;
			}
		}

		public User GetByUserId(string userId)
		{
			using (var cntx = new DatingDBContext())
			{
				var user = cntx.Set<User>().Where(x => x.UserId.Equals(userId));
				return user.FirstOrDefault();

			}
		}
		public User GetByName(string name)
		{
			using (var cntx = new DatingDBContext())
			{
				var user = cntx.Set<User>().Where(x => x.Name.Equals(name));
				return user.FirstOrDefault();
			}
		}

		public List<User> GetAll()
		{
			using (var cntx = new DatingDBContext())
			{
				var user = cntx.Set<User>();
				return user.ToList();
			}
		}

		public List<User> GetSearch(string searchString, string genderTarget)
		{
			using (var cntx = new DatingDBContext())
			{
				var user = cntx.Set<User>().Where(x => x.Name.Equals(searchString) && x.Gender.Equals(genderTarget));
				return user.ToList();
			}
		}

		public User Update(User user)
		{
			using (var cntx = new DatingDBContext())
			{
				var oldProfile = GetByUserId(user.UserId);

				cntx.Entry(oldProfile).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Detached;
				cntx.Entry(user).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Modified;
				cntx.SaveChanges();
				return user;
			}
		}
	}
}
