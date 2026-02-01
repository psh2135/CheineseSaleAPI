using ChineseSaleApi.Data;
using ChineseSaleApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
public interface IGiftRepository
{
    Gift Add(Gift gift);
    Gift? GetById(int id);
    IEnumerable<Gift> GetAll();
    void Update(Gift gift);
    void Delete(Gift gift);
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

    public Gift? GetById(int id)
    {
        return _context.Gifts
            .Include(g => g.Categories)
            .FirstOrDefault(g => g.Id == id);
    }

    public IEnumerable<Gift> GetAll()
    {
        return _context.Gifts.AsNoTracking().ToList();
    }

    public void Update(Gift gift)
    {
        _context.Gifts.Update(gift);
    }

    public void Delete(Gift gift)
    {
        _context.Gifts.Remove(gift);
    }
}
