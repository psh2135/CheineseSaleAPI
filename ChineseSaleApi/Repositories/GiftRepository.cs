using ChineseSaleApi.Data;
using ChineseSaleApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
public interface IGiftRepository
{
    Gift Add(Gift gift);
    void Update(Gift gift);
    void Delete(Gift gift);
    Gift? GetById(int id);
    IEnumerable<Gift> GetAll();
    IEnumerable<Gift> GetByCategory(int categoryId);
    IEnumerable<object> GetMostPopularGifts();
    IEnumerable<Gift> GetMostExpensiveGift();
    User? GetWinner(int id);
   
}
public class GiftRepository : IGiftRepository
{
    private readonly AppDbContext _context;

    public GiftRepository(AppDbContext context)
    {
        _context = context;
    }

    public Gift Add(Gift gift)
    {
        _context.Gifts.Add(gift);
        return gift;

    }
    public void Update(Gift gift)
    {
        _context.Gifts.Update(gift);
    }

    public void Delete(Gift gift)
    {
        _context.Gifts.Remove(gift);
    }
    public Gift? GetById(int id)
    {
        return _context.Gifts
            .Include(g => g.Categories)
            .FirstOrDefault(g => g.Id == id);
    }

    public IEnumerable<Gift> GetAll()
    {
        return _context.Gifts
            .Include(g => g.Categories)
            .AsNoTracking()
            .ToList();
    }
    public IEnumerable<Gift> GetByCategory(int categoryId)
    {
        return _context.Gifts
            .Include(g => g.Categories)
            .Where(g => g.Categories.Any(c => c.Id == categoryId))
            .AsNoTracking()
            .ToList();
    }
    public IEnumerable<object> GetMostPopularGifts()
    {
        return _context.Gifts
            .Select(g => new
            {
                GiftName = g.Title,
                TicketCount = g.Tickets.Count
            })
            .OrderByDescending(x => x.TicketCount)
            .ToList();
    }
    public IEnumerable<Gift> GetMostExpensiveGift()
    {
        var maxPrice = _context.Gifts.Max(g => g.Price);

        return _context.Gifts
            .Where(g => g.Price == maxPrice)
            .ToList();
    }

    public User? GetWinner(int id)
    {
        return _context.Gifts
            .Where(g => g.Id == id)
            .Select(g => g.Winner)
            .FirstOrDefault();
    }


}
