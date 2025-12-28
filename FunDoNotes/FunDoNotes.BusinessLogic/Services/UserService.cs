using FunDoNotes.DataAccess.Context;
using FunDoNotes.Model.DTOs;
using FunDoNotes.Model.Entities;
using FunDoNotes.BusinessLogic.Interfaces;
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

        public UserService(FunDoNotesDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;

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
