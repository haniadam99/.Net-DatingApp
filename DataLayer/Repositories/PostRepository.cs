using DatingProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{//klar
	class PostRepository
	{
		public List<Post> GetAll(string userId)
		{
			using (var cntx = new DatingDBContext())
			{
				var post = cntx.Set<Post>().Where(x => x.Author.Equals(userId)).OrderByDescending(x => x.PublishDate);
				return post.ToList();
			}
		}

		public Post Add(Post post)
		{
			using (var cntx = new DatingDBContext())
			{
				cntx.Entry<Post>(post).State = (Microsoft.EntityFrameworkCore.EntityState)System.Data.Entity.EntityState.Added;
				cntx.SaveChanges();

				return post;
			}
		}

		public Post Update(int postId, string text)
		{
			var post = new Post() { PostId = postId, Text = text };
			using (var cntx = new DatingDBContext())
			{
				cntx.Entry(post).Property(x => x.Text).IsModified = true;
				cntx.SaveChanges();
				return post;
			}
		}

		public Post Delete(int postId)
		{
			using (var cntx = new DatingDBContext())
			{
				var post = cntx.Set<Post>().Where(x => x.PostId == postId).FirstOrDefault();
				cntx.Entry<Post>(post).State = EntityState.Deleted;
				cntx.SaveChanges();
				return post;
			}
		}
	}
}
