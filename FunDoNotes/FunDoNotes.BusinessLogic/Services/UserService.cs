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

        public UserService(FunDoNotesDbContext context)
        {
            _context = context;
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
    }
}
