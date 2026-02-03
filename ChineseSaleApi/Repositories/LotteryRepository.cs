using ChineseSaleApi.Data;
using ChineseSaleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseSaleApi.Repositories
{
    public interface ILotteryRepository
    {
        Gift? GetGiftById(int giftId);
        List<Ticket> GetTicketsByGift(int giftId);
        IEnumerable<Gift> GetAllDrawnGifts();
        void Save();
    }
}


namespace ChineseSaleApi.Repositories
{
    public class LotteryRepository : ILotteryRepository
    {
        private readonly AppDbContext _context;

        public LotteryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Gift? GetGiftById(int giftId)
        {
            return _context.Gifts
                .Include(g => g.WinnerUserId)
                .FirstOrDefault(g => g.Id == giftId);
        }

        public List<Ticket> GetTicketsByGift(int giftId)
        {
            return _context.Tickets
                .Include(t => t.Purchase)
                .Where(t => t.GiftId == giftId)
                .ToList();
        }

        public IEnumerable<Gift> GetAllDrawnGifts()
        {
            return _context.Gifts
                .Include(g => g.Winner) // טעינת המשתמש שזכה
                .Where(g => g.IsDrawn == true)
                .AsNoTracking()
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
