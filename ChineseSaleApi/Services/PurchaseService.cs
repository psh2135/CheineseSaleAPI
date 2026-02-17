using AutoMapper;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using ChineseSaleApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChineseSaleApi.Services
{
    public interface IPurchaseService
    {
        Task AddToCartAsync(int buyerId, AddToCartDto dto);
        Task<PurchaseDto> CompletePurchase(int buyerId);
        PurchaseDto? GetById(int id);
        void RemoveFromCart(int buyerId, AddToCartDto dto);
        IEnumerable<Package> GetAllPackages();
        IEnumerable<PurchaseDto> GetUserTickets(int buyerId);
        Task< AdminDashboardDto> GetAdminDashboardStats();
        Task<IEnumerable<GiftDto>> GetCartAsync(int buyerId);

    }

    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repo;
        private readonly IRaffleStateService _stateService;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository repo, IMapper mapper, IRaffleStateService stateService)
        {
            _repo = repo;
            _mapper = mapper;
            _stateService = stateService;
        }

        public async Task AddToCartAsync(int buyerId, AddToCartDto dto)
        {
            if (!_stateService.IsRaffleLocked())
                throw new InvalidOperationException("Raffle is not locked");

            var draft = _repo.GetOrCreateDraft(buyerId);

            draft.GiftsAtCart.Add(dto.GiftId);

            _repo.Update(draft);
        }

        public async Task<PurchaseDto> CompletePurchase(int buyerId)
        {
            if (!_stateService.IsRaffleLocked())
                throw new InvalidOperationException("Raffle is not locked");

            var draft = _repo.GetOrCreateDraft(buyerId);
            if (!draft.GiftsAtCart.Any()) throw new Exception("Cart is empty");

            // 1. חישוב המחיר
            decimal totalPrice = 0;

            foreach (var giftId in draft.GiftsAtCart)
            {
                var gift = await _repo.GetGiftByIdAsync(giftId); // או איך שאתה שולף Gift
                if (gift == null)
                    throw new Exception("Gift not found");

                totalPrice += gift.Price;

                draft.Tickets.Add(new Ticket { GiftId = giftId });
            }

            draft.TotalPrice = totalPrice;


           
            draft.GiftsAtCart.Clear(); 
            draft.Status = PurchaseStatus.Completed;

            _repo.Update(draft);

            _repo.GetOrCreateDraft(buyerId);

            return _mapper.Map<PurchaseDto>(draft);
        }
       

        public PurchaseDto? GetById(int id) => _mapper.Map<PurchaseDto>(_repo.GetById(id));

        public void RemoveFromCart(int buyerId, AddToCartDto dto)
        {
            var draft = _repo.GetOrCreateDraft(buyerId);

            // הסרת המופע הראשון של ה-GiftId מהרשימה
            if (draft.GiftsAtCart.Contains(dto.GiftId))
            {
                draft.GiftsAtCart.Remove(dto.GiftId);
                _repo.Update(draft);
            }
        }
        public IEnumerable<Package> GetAllPackages()
        {
            return _repo.GetAllPackages();
        }
        public IEnumerable<PurchaseDto> GetUserTickets(int buyerId)
        {
            var history = _repo.GetUserPurchaseHistory(buyerId);
            return _mapper.Map<IEnumerable<PurchaseDto>>(history);
        }
        

        public async Task<AdminDashboardDto> GetAdminDashboardStats()
        {
            return new AdminDashboardDto
            {
                TotalRevenue = await _repo.GetTotalRevenue(),
                TotalTicketsSold = await _repo.GetTotalTicketsCount(),
                TotalParticipants = await _repo.GetUniqueParticipantsCount()
            };
        }
        public async Task<IEnumerable<GiftDto>> GetCartAsync(int buyerId)
        {
            var draft = await _repo.GetDraftByBuyerIdAsync(buyerId);
            if (draft == null || !draft.GiftsAtCart.Any())
                return Enumerable.Empty<GiftDto>();

            var gifts = await _repo.GetGiftsByIdsAsync(draft.GiftsAtCart);
            var giftsDict = gifts.ToDictionary(g => g.Id);

            // מחזירים רשימה עם כפילויות לפי מה שנשמר בסל
            return draft.GiftsAtCart
                .Where(id => giftsDict.ContainsKey(id))
                .Select(id => _mapper.Map<GiftDto>(giftsDict[id]));
        }
    }
}