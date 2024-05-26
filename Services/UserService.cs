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
        public async Task<bool> RegisterUser(string username, string password, string country, string email, int age)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentException("Username is required");
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Password is required");
                }

                if (string.IsNullOrWhiteSpace(country))
                {
                    throw new ArgumentException("Country is required");
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email is required");
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
                    Password = password,
                    Country = country,
                    Email = email,
                    Age = age,
                    Role = 1
                };

                await userRepository.BcryptPassword(user);
                await userRepository.Add(user);

                return true;
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }

        public async Task<string> AuthenticateUser(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentException("Username is required");
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Password is required");
                }

                var user = await userRepository.GetUserByUsername(username);

                if (user == null)
                {
                    throw new ArgumentException("Invalid username or password");
                }

                if (!userRepository.VerifyPassword(password, user.Password))
                {
                    return null;
                }
                return GenerateJwtToken(user);
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }

        public string GenerateJwtToken(Users user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "923/1",
                claims: new Claim[]
                {
                    new Claim("username", user.UserName),
                    new Claim("id", user.UserId.ToString()),
                    new Claim("role", user.Role.ToString())
                },
                signingCredentials: signinCredentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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