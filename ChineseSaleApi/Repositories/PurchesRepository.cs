using ChineseSaleApi.Data;
using ChineseSaleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseSaleApi.Repositories
{
    public interface IPurchaseRepository
    {
        Purchase GetOrCreateDraft(int buyerId);
        void Update(Purchase purchase);
        Purchase? GetById(int id);
        IEnumerable<Purchase> GetAll();
    }

    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _context;
        public PurchaseRepository(AppDbContext context) => _context = context;

        public Purchase GetOrCreateDraft(int buyerId)
        {
            var draft = _context.Purchases
                .Include(p => p.Tickets)
                .FirstOrDefault(p => p.BuyerId == buyerId && p.Status == PurchaseStatus.Draft);

            if (draft == null)
            {
                draft = new Purchase { BuyerId = buyerId, Status = PurchaseStatus.Draft };
                _context.Purchases.Add(draft);
                _context.SaveChanges();
            }
            return draft;
        }

        public Purchase? GetById(int id) =>
            _context.Purchases.Include(p => p.Tickets).ThenInclude(t => t.Gift).FirstOrDefault(p => p.Id == id);

        public IEnumerable<Purchase> GetAll() =>
            _context.Purchases.Include(p => p.Tickets).ToList();

        public void Update(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
            _context.SaveChanges();
        }
    }
}