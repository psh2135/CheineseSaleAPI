using AutoMapper;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Repositories;

namespace ChineseSaleApi.Services
{
    public interface ILotteryService
    {
        LotteryResultDto RunLottery(RunLotteryDto dto);
        IEnumerable<LotteryResultDto> GetAllLotteryResults();
    }

    public class LotteryService : ILotteryService
    {
        private readonly ILotteryRepository _repository;
        private readonly IMapper _mapper;

        public LotteryService(ILotteryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public LotteryResultDto RunLottery(RunLotteryDto dto)
        {
            var gift = _repository.GetGiftById(dto.GiftId);

            if (gift == null)
                throw new Exception("Gift not found");

            if (gift.IsDrawn)
                throw new Exception("Lottery already executed");

            var tickets = _repository.GetTicketsByGift(dto.GiftId);

            if (!tickets.Any())
                throw new Exception("No tickets for this gift");

            var random = new Random();
            var winner = tickets[random.Next(tickets.Count)];

            gift.WinnerUserId = winner.Purchase.BuyerId;
            gift.IsDrawn = true;

            _repository.Save();

            return new LotteryResultDto
            {
                GiftId = gift.Id,
                WinningTicketId = winner.Id,
                WinnerUserId = winner.Purchase.BuyerId
            };
        }
        public IEnumerable<LotteryResultDto> GetAllLotteryResults()
        {
            var drawnGifts = _repository.GetAllDrawnGifts();

            // המרה ל-DTO (אפשר ידנית או עם AutoMapper)
            return _mapper.Map<IEnumerable<LotteryResultDto>>(drawnGifts);
        }
    }
}
