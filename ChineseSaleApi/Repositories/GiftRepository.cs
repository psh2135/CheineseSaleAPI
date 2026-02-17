using ChineseSaleApi.Data;
using ChineseSaleApi.DTO;
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
    List<Category> GetCategoriesByIds(List<int> categoryIds);
    Task<IEnumerable<WinnerGiftDto>> GetAllWinnersAsync();


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
        _context.SaveChanges();
        return gift;

    }
    public void Update(Gift gift)
    {
        _context.Gifts.Update(gift);
        _context.SaveChanges();

    }

    public void Delete(Gift gift)
    {
        _context.Gifts.Remove(gift);
        _context.SaveChanges();

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
                GiftId = g.Id,
                GiftName = g.Title,
                TicketCount = g.Tickets.Count
            })
            .OrderByDescending(x => x.TicketCount)
            .ToList();
    }
    public IEnumerable<Gift> GetMostExpensiveGift()
    {
        var maxPrice = _context.Gifts.Max(g => g.Price);
       
        if (maxPrice == null)
            return Enumerable.Empty<Gift>();

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
    public async Task<IEnumerable<WinnerGiftDto>> GetAllWinnersAsync()
    {
        return await _context.Gifts
            .Where(g => g.IsDrawn && g.WinnerUserId != null)
            .Select(g => new WinnerGiftDto
            {
                GiftId = g.Id,
                GiftTitle = g.Title,
                Price = g.Price,
                WinnerId = g.Winner!.Id,
                WinnerName = g.Winner.UserName,
                Email = g.Winner.Email
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public List<Category> GetCategoriesByIds(List<int> categoryIds)
    {
        return _context.Categories
            .Where(c => categoryIds.Contains(c.Id))
            .ToList();
    }
}
