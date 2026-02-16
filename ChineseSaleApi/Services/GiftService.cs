using ChineseSaleApi.Data;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using AutoMapper;

public interface IGiftService
{
    GiftDto CreateGift(CreateGiftDto dto);
    GiftDto? UpdateGift(GiftDto dto);
    void DeleteGift(int id);
    GiftDto? GetGiftById(int id);
    IEnumerable<GiftDto> GetAllGifts();
    IEnumerable<GiftDto> GetGiftsByCategory(int categoryId);
    IEnumerable<object> GetMostPopularGifts();
    IEnumerable<GiftDto> GetMostExpensiveGift();
    UserDto? GetWinner(int id);
}
public class GiftService : IGiftService
{
    private readonly IGiftRepository _repository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public GiftService(IGiftRepository repository, IMapper mapper, AppDbContext context)
    {
        _repository = repository;
        _mapper = mapper;
        _context = context;
    }

    public GiftDto CreateGift(CreateGiftDto dto)
    {
        //_logger.LogInformation("Creating gift for DonorId={DonorId}", dto.DonorId);

        var gift = _mapper.Map<Gift>(dto);

        gift.Categories =  _repository.GetCategoriesByIds(dto.CategoryIds);

        _repository.Add(gift);

        //_logger.LogInformation("Gift created successfully with Id={GiftId}", gift.Id);

        return _mapper.Map<GiftDto>(gift);
    }
    public GiftDto? UpdateGift(GiftDto dto)
    {
        var gift = _repository.GetById(dto.Id);
        if (gift == null) return null;

        _mapper.Map(dto, gift);
        _context.SaveChanges();

        return _mapper.Map<GiftDto>(gift);
    }

    public void DeleteGift(int id)
    {
        var gift = _repository.GetById(id);
        if (gift == null) return;

        _repository.Delete(gift);
        _context.SaveChanges();
    }
    public GiftDto? GetGiftById(int id)
    {
        var gift = _repository.GetById(id);
        if (gift == null) return null;

        return _mapper.Map<GiftDto>(gift);
    }

    public IEnumerable<GiftDto> GetAllGifts()
    {
        var gifts = _repository.GetAll();
        return _mapper.Map<IEnumerable<GiftDto>>(gifts);
    }
    public IEnumerable<GiftDto> GetGiftsByCategory(int categoryId)
    {
        var gifts = _repository.GetByCategory(categoryId);
        return _mapper.Map<IEnumerable<GiftDto>>(gifts);
    }
    public IEnumerable<object> GetMostPopularGifts()
    {
        return _repository.GetMostPopularGifts();
    }
    public IEnumerable<GiftDto> GetMostExpensiveGift()
    {
        var gifts = _repository.GetMostExpensiveGift();
        return _mapper.Map<IEnumerable<GiftDto>>(gifts);
    }

    public UserDto? GetWinner(int id)
    {
        var winner = _repository.GetWinner(id);
        return _mapper.Map<UserDto>(winner);
    }

}
