using FunDoNotes.DataAccess.Context;
using FunDoNotes.Model.DTOs.Email;
using FunDoNotes.Model.DTOs;
using FunDoNotes.Model.Entities;
using FunDoNotes.BusinessLogic.Interfaces;
using FunDoNotes.BusinessLogic.Messaging;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly FunDoNotesDbContext _context;
        private readonly JwtService _jwtService;
         
        private readonly IRabbitMqPublisher _rabbitMqService;

        public UserService(FunDoNotesDbContext context, JwtService jwtService,IRabbitMqPublisher rabbitMqService)
        {
            _context = context;
            _jwtService = jwtService;
            _rabbitMqService = rabbitMqService;
        }

        public void Register(RegisterUserDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                throw new Exception("Email already exists");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            _rabbitMqService.Publish(new SendEmailDto
            {
                ToEmail = user.Email,
                Subject = "Welcome to FunDoo Notes",
                Body = $"Hi {user.FirstName}, Welcome to FunDoo Notes! \n" +
                $"🎉\r\nWe’re excited to have you on board.\r\n\r\nFunDoo " +
                $"Notes is designed to help you capture ideas instantly, " +
                $"stay organized effortlessly, and turn thoughts into action—all in one simple, " +
                $"secure place.\r\n\r\nHere’s what you can do starting now:\r\n• " +
                $"✍️ Create and manage notes anytime, anywhere\r\n• " +
                $"🗂️ Organize your ideas the way you like\r\n• " +
                $"🔒 Keep your notes safe and accessible\r\n• " +
                $"⚡ Stay productive without distractions\r\n\r\nYour ideas matter, " +
                $"and we’re here to help you never lose them.\r\n\r\nIf you ever need help or have suggestions, " +
                $"our support team is always happy to hear from you.\r\n\r\nHappy noting," +
                $"\r\nThe FunDoo Notes Team 🚀"
            });
        }

        public string Login(LoginUserDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
                throw new Exception("Invalid email or password");

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!isPasswordValid)
                throw new Exception("Invalid email or password");

            
            return _jwtService.GenerateToken(user);
        }

        public IEnumerable<Note> GetArchivedNotes(int userId)
        {
            return _context.Notes
                .Where(n =>
                    n.UserId == userId &&
                    n.IsArchived &&
                    !n.IsTrashed)
                .OrderByDescending(n => n.UpdatedAt)
                .ToList();
        }

    }
}
