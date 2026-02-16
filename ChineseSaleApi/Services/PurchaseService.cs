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
        PurchaseDto CompletePurchase(int buyerId);
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
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddToCartAsync(int buyerId, AddToCartDto dto)
        {
            var draft = _repo.GetOrCreateDraft(buyerId);

            draft.GiftsAtCart.Add(dto.GiftId);

            _repo.Update(draft);
        }

        public PurchaseDto CompletePurchase(int buyerId)
        {
            var draft = _repo.GetOrCreateDraft(buyerId);
            if (!draft.GiftsAtCart.Any()) throw new Exception("Cart is empty");
           
            // 1. חישוב המחיר
            int totalTicketsInCart = draft.GiftsAtCart.Count;
            draft.TotalPrice = CalculateBestPrice(totalTicketsInCart);
            
            // המרת ה-IDs מהסל לכרטיסים ממשיים
            foreach (var giftId in draft.GiftsAtCart)
            {
                draft.Tickets.Add(new Ticket { GiftId = giftId });
            }

            draft.GiftsAtCart.Clear(); 
            draft.Status = PurchaseStatus.Completed;

            _repo.Update(draft);

            _repo.GetOrCreateDraft(buyerId);

            return _mapper.Map<PurchaseDto>(draft);
        }
        private decimal CalculateBestPrice(int ticketCount)
        {
            // שליפת כל החבילות מה-DB ומיונן מהגדולה לקטנה
            var packages = _repo.GetAllPackages().OrderByDescending(p => p.TicketsCount).ToList();
            decimal totalPrice = 0;
            int remainingTickets = ticketCount;

            foreach (var package in packages)
            {
                if (remainingTickets >= package.TicketsCount && package.TicketsCount > 0)
                {
                    int numPackages = remainingTickets / package.TicketsCount;
                    totalPrice += numPackages * package.Price;
                    remainingTickets %= package.TicketsCount;
                }
            }

            return totalPrice;
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

            return _mapper.Map<IEnumerable<GiftDto>>(gifts);
        }
    }
}