using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Repositories;

namespace UBB_SE_2024_Music.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> RegisterUser(string username, string country, string email, int age)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentException("Username is required");
                }

                if (string.IsNullOrWhiteSpace(country))
                {
                    throw new ArgumentException("Country is required");
                }

                if (age < 0)
                {
                    throw new ArgumentException("Please select a valid age");
                }

                var potentialUserWithSameUsername = await userRepository.GetUserByUsername(username);
                if (potentialUserWithSameUsername != null)
                {
                    throw new ArgumentException("This username is already taken");
                }

                var user = new Users
                {
                    UserName = username,
                    Country = country,
                    Email = email,
                    Age = age,
                    Role = 1
                };

                await userRepository.Add(user);

                return true;
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }

        public async Task<bool> EnableOrDisableArtist(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    throw new ArgumentException("Invalid user ID");
                }

                var user = await userRepository.GetById(userId);

                if (user == null)
                {
                    throw new ArgumentException("Invalid username ID");
                }

                await userRepository.EnableOrDisableArtist(user);

                return true;
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }
    }
}