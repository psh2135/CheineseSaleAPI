using ChineseSaleApi.Data;
using ChineseSaleApi.Models;

namespace ChineseSaleApi.Repositories
{
    public class RaffleRepository
    {
        private readonly AppDbContext _context;

        public Raffle GetCurrentRaffle()
        {
            return _context.Raffles.Single();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
