using ChineseSaleApi.Data;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

public interface IGiftService
{
    GiftDto CreateGift(CreateGiftDto dto);
    GiftDto? UpdateGift(int id, UpdateGiftDto dto);
    void DeleteGift(int id);
    GiftDto? GetGiftById(int id);
    IEnumerable<GiftDto> GetAllGifts();
    IEnumerable<GiftDto> GetGiftsByCategory(int categoryId);
    IEnumerable<object> GetMostPopularGifts();
    IEnumerable<GiftDto> GetMostExpensiveGift();
    UserDto? GetWinner(int id);
    Task<IEnumerable<WinnerGiftDto>> GetAllWinnersAsync();
}

public class GiftService : IGiftService
{
    private readonly IGiftRepository _repository;
    private readonly IRaffleStateService _stateService;
    private readonly IMapper _mapper;
    private readonly ILogger<GiftService> _logger;

    public GiftService(IGiftRepository repository, IMapper mapper, IRaffleStateService stateService, ILogger<GiftService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _stateService = stateService;
        _logger = logger;
    }

    public GiftDto CreateGift(CreateGiftDto dto)
    {
        if (_stateService.IsRaffleLocked())
            throw new InvalidOperationException("Raffle is locked");

        _logger.LogInformation("Creating gift for DonorId={DonorId}", dto.DonorId);
        var gift = _mapper.Map<Gift>(dto);
        gift.Categories = _repository.GetCategoriesByIds(dto.CategoryIds);
        _repository.Add(gift);
        _logger.LogInformation("Gift created successfully with Id={GiftId}", gift.Id);
        return _mapper.Map<GiftDto>(gift);
    }

    public GiftDto? UpdateGift(int id, UpdateGiftDto dto)
    {
        if (_stateService.IsRaffleLocked())
            throw new InvalidOperationException("Raffle is locked");

        var gift = _repository.GetById(id);
        if (gift == null)
        {
            _logger.LogWarning("UpdateGift: Gift with Id={GiftId} not found", id);
            return null;
        }

        gift.Title = dto.Title ?? gift.Title;
        gift.Description = dto.Description ?? gift.Description;
        gift.ImageUrl = dto.ImageUrl ?? gift.ImageUrl;
        gift.Price = dto.Price ?? gift.Price;
        gift.DonorId = dto.DonorId ?? gift.DonorId;

        if (dto.CategoryIds != null)
        {
            gift.Categories.Clear();
            var categories = _repository.GetCategoriesByIds(dto.CategoryIds);
            foreach (var category in categories)
            {
                gift.Categories.Add(category);
            }
        }

        _repository.Update(gift);
        _logger.LogInformation("Gift updated successfully with Id={GiftId}", id);
        return _mapper.Map<GiftDto>(gift);
    }

    public void DeleteGift(int id)
    {
        if (_stateService.IsRaffleLocked())
            throw new InvalidOperationException("Raffle is locked");

        var gift = _repository.GetById(id);
        if (gift == null)
        {
            _logger.LogWarning("DeleteGift: Gift with Id={GiftId} not found", id);
            return;
        }

        _repository.Delete(gift);
        _logger.LogInformation("Gift deleted with Id={GiftId}", id);
    }

    public GiftDto? GetGiftById(int id)
    {
        var gift = _repository.GetById(id);
        if (gift == null)
        {
            _logger.LogWarning("GetGiftById: Gift with Id={GiftId} not found", id);
            return null;
        }
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

    public async Task<IEnumerable<WinnerGiftDto>> GetAllWinnersAsync()
    {
        return await _repository.GetAllWinnersAsync();
    }
}