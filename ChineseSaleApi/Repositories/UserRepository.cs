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
        Task<User?> GetByIdAsync(int id);

        User GetByEmail(string email);
        IEnumerable<User> GetUsersByRole(UserRole role);
        Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);

        void Add(User user);
        bool Save();
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetUsersByRole(UserRole role)
        {
            return _context.Users.Where(u => u.Role == role).ToList();
        }
        public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
        {
            return await _context.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
