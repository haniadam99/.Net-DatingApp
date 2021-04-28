using DataLayer.Repositories;
using DatingProjekt.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Managers
{//klar
    public class PostManager
    {

        private readonly PostRepository postRepository;


        public PostManager()
        {
            postRepository = new PostRepository();
        }


        //Denna metod hämtar alla inlägg genom att lista. Anropar metoden GetAll från postRepostiory
        public List<Post> GetAll(string userId)
        {
            try
            {
                return postRepository.GetAll(userId);
            }

            catch (Exception)
            {
                throw new Exception("Error occured while trying to recover all posts");
            }

        }
        //Denna metod gör det möjligt att lägga till ett inlägg. Anropar metoden Add från postRepository
        public Post Add(Post post)
        {
            try
            {
                return postRepository.Add(post);
            }

            catch (Exception)
            {
                throw new Exception("Error occured while trying to add");
            }

        }

        //Denna metod uppdaterar ett inlägg. Anropar metoden Update från postRepository
        public Post Update(int postId, string text)
        {
            try
            {
                return postRepository.Update(postId, text);
            }

            catch (Exception)
            {
                throw new Exception("Error occured while trying to update the post");
            }

        }
        //Denna metod raderar ett inlägg. Anropar metoden delete från postRepository
        public Post Delete (int postId)
        {
            try
            {
                return postRepository.Delete(postId);
            }

            catch (Exception)
            {
                throw new Exception("Error occured while trying to delete");
            }

        }
    }
}
