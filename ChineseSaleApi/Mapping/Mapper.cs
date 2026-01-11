using AutoMapper;
using ChineseSaleApi.DTO;
using ChineseSaleApi.DTO.ChineseSaleApi.DTO;
using ChineseSaleApi.Models;

namespace ChineseSaleApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //user
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<User, CreateUserDto>();
            
            //gift
            CreateMap<GiftDto, Gift>();
            CreateMap<Gift, GiftDto>();
            CreateMap<CreateGiftDto, Gift>();
            CreateMap<Gift, CreateGiftDto>();
            
            //lottery
            //CreateMap<RunLotteryDto, LotteryResult>();
            //CreateMap<LotteryResult, RunLotteryDto>();
            //CreateMap<LotteryResultDto, LotteryResult>();
            //CreateMap<LotteryResult, LotteryResultDto>();
            
            //package
            CreateMap<PackageDto, Package>();
            CreateMap<Package, GiftDto>();
            CreateMap<CreatePackageDto, Package>();
            CreateMap<Package, CreatePackageDto>();

            //purches
            CreateMap<CreatePurchaseDto, Purchase>();
            CreateMap<Purchase, PurchaseDto>();

            //ticket
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<Ticket, TicketDto>();

            CreateMap<Ticket, TicketAdminDto>()
                .ForMember(d => d.TicketId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.BuyerId, o => o.MapFrom(s => s.Purchase.BuyerId));


        }
    }

}
