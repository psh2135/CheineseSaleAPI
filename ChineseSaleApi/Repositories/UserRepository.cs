using AutoMapper;
using ChineseSaleApi.Data;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using System.Linq;

namespace ChineseSaleApi.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByEmail(string email);
        IEnumerable<User> GetUsersByRole(string role); // פונקציה גנרית לקבלת משתמשים לפי תפקיד
        void Add(User user);
        bool Save();
    }
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User GetById(int id) => _context.Users.Find(id);

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetUsersByRole(string role)
        {
            return _context.Users.Where(u => u.Role == role).ToList();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
