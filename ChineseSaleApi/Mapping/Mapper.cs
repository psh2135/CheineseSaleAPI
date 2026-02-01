using AutoMapper;
using ChineseSaleApi.DTO;
using ChineseSaleApi.DTO.ChineseSaleApi.DTO;
using ChineseSaleApi.DTO.ChineseSaleApi.DTOs;


//using ChineseSaleApi.DTO.ChineseSaleApi.DTO;
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
            CreateMap<CreateUserDto, User>()
     .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<User, CreateUserDto>();
            
            //gift
            CreateMap<GiftDto, Gift>();
            CreateMap<Gift, GiftDto>();
            CreateMap<CreateGiftDto, Gift>();
            CreateMap<Gift, CreateGiftDto>();

            //category
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CreateCategoryDto>();
            //lottery
            //CreateMap<RunLotteryDto, LotteryResult>();
            //CreateMap<LotteryResult, RunLotteryDto>();
            //CreateMap<LotteryResultDto, LotteryResult>();
            //CreateMap<LotteryResult, LotteryResultDto>();

            //package
            CreateMap<PackageDto, Package>();
            CreateMap<Package, PackageDto>();
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
                .ForMember(d => d.BuyerId, o => o.MapFrom(s => s.Purchase != null ? s.Purchase.BuyerId : 0))
                .ForMember(d => d.GiftId, o => o.MapFrom(s => s.GiftId))
                .ForMember(d => d.PurchaseId, o => o.MapFrom(s => s.PurchaseId))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt));



        }
    }

}
