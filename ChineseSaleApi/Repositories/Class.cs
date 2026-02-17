using ChineseSaleApi.Data;
using ChineseSaleApi.Models;

namespace ChineseSaleApi.Repositories
{
    public class RaffleRepository
    {
        public RaffleRepository(AppDbContext context)
        {
            _context = context;
        }
        private readonly AppDbContext _context;

        public Raffle GetCurrentRaffle()
        {
            return _context.Raffles.First();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
