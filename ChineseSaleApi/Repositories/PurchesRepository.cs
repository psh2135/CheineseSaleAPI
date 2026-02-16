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
        Task<IEnumerable<Purchase>> GetCompleted();
        IEnumerable<Package> GetAllPackages();
        IEnumerable<Purchase> GetUserPurchaseHistory(int buyerId);
        Task<decimal> GetTotalRevenue();
        Task<int> GetTotalTicketsCount();
        Task<int> GetUniqueParticipantsCount();
        Task<Gift?> GetGiftByIdAsync(int giftId);
        Task<Purchase?> GetDraftByBuyerIdAsync(int buyerId);
        Task<IEnumerable<Gift>> GetGiftsByIdsAsync(List<int> ids);
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


        public async Task<IEnumerable<Purchase>> GetCompleted()
        {
            return await _context.Purchases.Where(p => p.Status == PurchaseStatus.Completed).ToListAsync();
        }


        public async Task<int> GetTotalTicketsCount()
        {
            return await _context.Tickets.CountAsync();
        }

        public async Task<int> GetUniqueParticipantsCount()
        {
            return await _context.Purchases
                .Where(p => p.Status == PurchaseStatus.Completed)
                .Select(p => p.BuyerId)
                .Distinct()
                .CountAsync();
        }

        public IEnumerable<Package> GetAllPackages()
        {
            return _context.Packages
                .AsNoTracking()
                .ToList();
        }
        public IEnumerable<Purchase> GetUserPurchaseHistory(int buyerId)
        {
            return _context.Purchases
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Gift)
                .Where(p => p.BuyerId == buyerId && p.Status == PurchaseStatus.Completed)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }
        public async Task<decimal> GetTotalRevenue()
        {
            return await _context.Purchases
                .Where(p => p.Status == PurchaseStatus.Completed)
                .SumAsync(p => p.TotalPrice);
        }
        public async Task<Gift?> GetGiftByIdAsync(int giftId)
        {
            return await _context.Gifts
                .FirstOrDefaultAsync(g => g.Id == giftId);
        }

        public async Task<Purchase?> GetDraftByBuyerIdAsync(int buyerId)
        {
            return await _context.Purchases
                .FirstOrDefaultAsync(p =>
                    p.BuyerId == buyerId &&
                    p.Status == PurchaseStatus.Draft);
        }

        public async Task<IEnumerable<Gift>> GetGiftsByIdsAsync(List<int> ids)
        {
            return await _context.Gifts
                .Where(g => ids.Contains(g.Id))
                .AsNoTracking()
                .ToListAsync();
        }

    }
}