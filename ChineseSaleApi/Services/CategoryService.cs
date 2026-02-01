using AutoMapper;
using ChineseSaleApi.Data;
using ChineseSaleApi.DTO;
using ChineseSaleApi.DTO.ChineseSaleApi.DTOs;
using ChineseSaleApi.Models;

public interface ICategoryService
{
    IEnumerable<CategoryDto> GetAllCategories();

    CategoryDto GetCategoryById(int id);

    CategoryDto CreateCategory(CreateCategoryDto categoryDto);

    CategoryDto UpdateCategory(int id, CategoryDto categoryDto);

    bool DeleteCategory(int id);
}

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<CategoryDto> GetAllCategories()
    {
        var categories = _repository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public CategoryDto GetCategoryById(int id)
    {
        var category = _repository.GetById(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public CategoryDto CreateCategory(CreateCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        _repository.Add(category);
        _repository.Save();
        return _mapper.Map<CategoryDto>(category);
    }

    public CategoryDto UpdateCategory(int id, CategoryDto categoryDto)
    {
        // 1. שליפת הישות הקיימת מה-Repo
        var existingCategory = _repository.GetById(id);
        if (existingCategory == null) return null;

        // 2. עדכון הערכים מה-DTO לישות הקיימת (Mapping)
        _mapper.Map(categoryDto, existingCategory);

        // 3. עדכון ב-Repo ושמירה
        _repository.Update(existingCategory);
        _repository.Save();

        // 4. החזרת ה-DTO המעודכן
        return _mapper.Map<CategoryDto>(existingCategory);
    }

    public bool DeleteCategory(int id)
    {
        var category = _repository.GetById(id);
        if (category == null) return false;

        _repository.Delete(id);
        return _repository.Save();
    }
}