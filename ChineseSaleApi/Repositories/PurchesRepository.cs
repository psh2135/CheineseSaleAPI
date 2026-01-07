using ChineseSaleApi.Data;
using ChineseSaleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseSaleApi.Repositories
{
    public interface IPurchaseRepository
    {
        Purchase Create(Purchase purchase);
        Purchase? GetById(int id);
        IEnumerable<Purchase> GetAll();
        Purchase Update(Purchase purchase);
        Purchase? Delete(int id);
    }

    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _context;

        public PurchaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public Purchase Create(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
            return purchase;
        }

        public Purchase? GetById(int id)
        {
            return _context.Purchases
                .Include(p => p.Package)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Purchase> GetAll()
        {
            return _context.Purchases
                .Include(p => p.Package)
                .ToList();
        }

        public Purchase Update(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
            _context.SaveChanges();
            return purchase;
        }

        public Purchase? Delete(int id)
        {
            var purchase = _context.Purchases.FirstOrDefault(p => p.Id == id);
            if (purchase == null) return null;

            _context.Purchases.Remove(purchase);
            _context.SaveChanges();
            return purchase;
        }
    }
}
