using AutoMapper;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using ChineseSaleApi.Repositories;

namespace ChineseSaleApi.Services
{
    public interface IPurchaseService
    {
        PurchaseDto Create(CreatePurchaseDto dto);
        PurchaseDto? GetById(int id);
        IEnumerable<PurchaseDto> GetAll();
        PurchaseDto? Delete(int id);
    }

    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repository;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public PurchaseDto Create(CreatePurchaseDto dto)
        {
            var entity = _mapper.Map<Purchase>(dto);
            var saved = _repository.Create(entity);
            return _mapper.Map<PurchaseDto>(saved);
        }

        public PurchaseDto? GetById(int id)
        {
            var entity = _repository.GetById(id);
            return entity == null ? null : _mapper.Map<PurchaseDto>(entity);
        }

        public IEnumerable<PurchaseDto> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.Map<IEnumerable<PurchaseDto>>(entities);
        }

        public PurchaseDto? Delete(int id)
        {
            var entity = _repository.Delete(id);
            return entity == null ? null : _mapper.Map<PurchaseDto>(entity);
        }
    }
}
