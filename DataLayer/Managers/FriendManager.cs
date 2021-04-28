using DatingProjekt.DataLayer.Repositories;
using DatingProjekt.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DataLayer.Managers
{//Klar
    public class FriendManager
    {
        private readonly FriendRepository friendRepository;

        public FriendManager()
        {
            friendRepository = new FriendRepository();
        }
        //Anropar metoden GetByid som finns i friendRepository
        public Friend GetByID(string currentUser, string friendId)
        {
            try
            {
                return friendRepository.GetById(currentUser, friendId);
            }
            catch(Exception)
            {
                throw new Exception("Error occurred while trying to get a friends profile");
            }
        }


        //Anropar metoden GetAll från friendRepository
        public List<Friend> GetAll(string userId)
        {
            try
            {
                return friendRepository.GetAll(userId);
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying to recover all friends");

            }
        }

       // Anropar metoden Add från friendRepository
        public Friend Add(Friend friend)
        {
            try
            {
                return friendRepository.Add(friend);
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying add");
            }
        }

        // Anropar metoden Delete från friendRepository
        public Friend Delete(Friend friend)
        {
            try
            {
                return friendRepository.Delete(friend);
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying to delete");
            }
        }
        //Anropar metoden AreFriends från friendRepository
        public bool AreFriends(string currentUserId, string friendUserId)
        {
            try
            {
                return friendRepository.AreFriends(currentUserId, friendUserId);

            }
            catch (Exception e)
            {
                //Log Error
                throw new Exception("An error occurred while trying to retrieve the friendship relationship.");
            }
        }
    }
}
