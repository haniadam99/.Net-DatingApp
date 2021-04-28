using DataLayer.Repositories;
using DatingProjekt.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Managers
{
    public class UserManager
    {

        private readonly UserRepository userRepository;


        public UserManager()
        {
            userRepository = new UserRepository();
        }

        public User Add(User user)
        {
            try
            {
                return userRepository.Add(user);
            }
            catch (Exception)
            {
                throw new Exception("Error occured while trying to add");
            }
        }

        public User GetByUserId(string userId)
        {
            try
            {
                return userRepository.GetByUserId(userId);
            }
            catch(Exception)
            {
                throw new Exception("Error occurred while trying to recover by user id");
            }

        }

        public User GetByName(string name)
        {
            try
            {
                return userRepository.GetByName(name);
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying to recover by name");
            }

        }


        public List<User> GetAll()
        {
            try
            {
                return userRepository.GetAll();
            }
            catch (Exception)
            {
                throw new Exception("Error occurred while trying to recover all users");
            }

        }



        //Denna metod gör det möjligt att kunna söka på användare. Anropar metoden GetSearch från uuserRepository
        public List<User> GetSearch(string searchString, string genderTarget)
        {
            try
            {
                return userRepository.GetSearch(searchString, genderTarget);
            }

            catch (Exception)
            {
                throw new Exception("Error occurred while trying to search");
            }
        }
       
        public User Update(User user)
        {
            try
            {
                return userRepository.Update(user);
            }
            catch (Exception)
            {
                throw new Exception("Error occured while trying to add");
            }
        }
    }
}
