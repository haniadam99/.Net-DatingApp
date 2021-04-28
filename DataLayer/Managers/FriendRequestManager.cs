using DataLayer.Repositories;
using DatingProjekt.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Managers
{//klar
    public class FriendRequestManager
    {
        private readonly FriendRequestRepository requestRepository;


        public FriendRequestManager()
        {
            requestRepository = new FriendRequestRepository();
        }
        // Anropar metoden GetSentByid från requestRepository
        public List<FriendRequest> GetSentById(string userId)
        {
            try
            {
                return requestRepository.GetSentById(userId);
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying to recover all friend requests");
            }
        }

        // Anropar metoden GetPendingByid från requestRepository
        public List<FriendRequest> GetPendingById(string userId)
        {
            try
            {
                return requestRepository.GetPendingById(userId);
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying to recover all friend requests on pending");
            }
        }
        //Anropar metoden Add från requestRepository
        public FriendRequest Add(FriendRequest request)
        {
            try
            {
                return requestRepository.Add(request);
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying to add");
            }
        }

        //. Anropar metoden Delete från requestRepository
        public FriendRequest Delete(FriendRequest request)
        {
            try
            {
                return requestRepository.Delete(request);
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying to delete");
            }
        }
        // Anropar metoden GetAll från requestRepository
        public List<FriendRequest> GetAll(string userId)
        {
            try
            {
                return requestRepository.GetAll(userId);
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying to recover all friends");

            }
        }
        //Anropar metoden Exists från requestRepository.
        public bool Exists(string currentUserId, string friendUserId)
        {
            try
            {
                return requestRepository.Exists(currentUserId, friendUserId);

            }
            catch (Exception e)
            {
                //Log Error
                throw new Exception("An error occurred while trying to retrieve the friendship relationship.");
            }
        }
    }
}
