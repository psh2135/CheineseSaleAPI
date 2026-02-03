using AutoMapper;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using ChineseSaleApi.Repositories;

namespace ChineseSaleApi.Services
{
    public interface IPurchaseService
    {
        void AddToCart(AddToCartDto dto);
        PurchaseDto CompletePurchase(int buyerId);
        PurchaseDto? GetById(int id);
        void RemoveFromCart(AddToCartDto dto);
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

        public void AddToCart(AddToCartDto dto)
        {
            var draft = _repo.GetOrCreateDraft(dto.BuyerId);
            draft.GiftsAtCart.Add(dto.GiftId);
            _repo.Update(draft);
        }

        public PurchaseDto CompletePurchase(int buyerId)
        {
            var draft = _repo.GetOrCreateDraft(buyerId);

            // המרת ה-IDs מהסל לכרטיסים ממשיים
            foreach (var giftId in draft.GiftsAtCart)
            {
                draft.Tickets.Add(new Ticket { GiftId = giftId });
            }

            draft.GiftsAtCart.Clear(); // ריקון הסל
            draft.Status = PurchaseStatus.Completed;

            _repo.Update(draft);

            // פתיחת טיוטה חדשה אוטומטית למשתמש
            _repo.GetOrCreateDraft(buyerId);

            return _mapper.Map<PurchaseDto>(draft);
        }

        public PurchaseDto? GetById(int id) => _mapper.Map<PurchaseDto>(_repo.GetById(id));

        public void RemoveFromCart(AddToCartDto dto)
        {
            var draft = _repo.GetOrCreateDraft(dto.BuyerId);

            // הסרת המופע הראשון של ה-GiftId מהרשימה
            if (draft.GiftsAtCart.Contains(dto.GiftId))
            {
                draft.GiftsAtCart.Remove(dto.GiftId);
                _repo.Update(draft);
            }
        }
    }
}