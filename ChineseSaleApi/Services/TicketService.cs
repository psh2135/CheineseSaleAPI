using AutoMapper;
using ChineseSaleApi.DTO;
using ChineseSaleApi.DTO.ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using ChineseSaleApi.Repositories;

namespace ChineseSaleApi.Services
{
    public interface ITicketService
    {
        IEnumerable<TicketDto> GetByGift(int giftId);
        IEnumerable<TicketDto> GetByBuyer(int buyerId);
        IEnumerable<TicketAdminDto> GetAll();
    }

    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public IEnumerable<TicketDto> GetByGift(int giftId)
        {
            var tickets = _repository.GetByGift(giftId);
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public IEnumerable<TicketDto> GetByBuyer(int buyerId)
        {
            var tickets = _repository.GetByBuyer(buyerId);
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public IEnumerable<TicketAdminDto> GetAll()
        {
            var tickets = _repository.GetAll();
            return _mapper.Map<IEnumerable<TicketAdminDto>>(tickets);
        }
    }
}

