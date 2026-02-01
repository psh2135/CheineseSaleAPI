using ChineseSaleApi.Data;
using ChineseSaleApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    Category? GetById(int id);
    void Add(Category category);
    void Update(Category category);
    void Delete(int id);
    bool Save();
}
public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context) => _context = context;

    public IEnumerable<Category> GetAll()
    {
       return _context.Categories.ToList();
    }
    public Category? GetById(int id) => _context.Categories.Find(id);
    public void Add(Category category) => _context.Categories.Add(category);
    public void Update(Category category) => _context.Categories.Update(category);
    public void Delete(int id)
    {
        var category = GetById(id);
        if (category != null) _context.Categories.Remove(category);
    }
    public bool Save() => _context.SaveChanges() >= 0;
}