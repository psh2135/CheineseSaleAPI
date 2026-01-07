using ChineseSaleApi.Data;
using ChineseSaleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseSaleApi.Repositories
{
    public interface ITicketRepository
    {
        Ticket Create(Ticket ticket);
        IEnumerable<Ticket> GetByGift(int giftId);
        IEnumerable<Ticket> GetByBuyer(int buyerId);
        IEnumerable<Ticket> GetAll();
    }

    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public Ticket Create(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public IEnumerable<Ticket> GetByGift(int giftId)
        {
            return _context.Tickets
                .Where(t => t.GiftId == giftId)
                .ToList();
        }

        public IEnumerable<Ticket> GetByBuyer(int buyerId)
        {
            return _context.Tickets
                .Include(t => t.Purchase)
                .Where(t => t.Purchase.BuyerId == buyerId)
                .ToList();
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _context.Tickets
               .Include(t => t.Purchase)
               .ToList();
        }
    }
}
