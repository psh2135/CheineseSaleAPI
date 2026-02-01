using ChineseSaleApi.Data;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using AutoMapper;

public interface IGiftService
{
    GiftDto CreateGift(CreateGiftDto dto);
    GiftDto? GetGiftById(int id);
    IEnumerable<GiftDto> GetAllGifts();
    GiftDto? UpdateGift(GiftDto dto);
    void DeleteGift(int id);
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
        var gift = _mapper.Map<Gift>(dto);

        _repository.Add(gift);
        _context.SaveChanges();

        return _mapper.Map<GiftDto>(gift);
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
}
