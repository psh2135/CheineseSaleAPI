using AutoMapper;
using ChineseSaleApi.DTO;
using ChineseSaleApi.DTO.ChineseSaleApi.DTO;
using ChineseSaleApi.DTO.ChineseSaleApi.DTOs;
using ChineseSaleApi.Models;

namespace ChineseSaleApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //user
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => UserRole.Buyer));

            CreateMap<CreateDonorDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => UserRole.Donor));



            //gift
            CreateMap<GiftDto, Gift>();
            CreateMap<Gift, GiftDto>()
             .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
            CreateMap<CreateGiftDto, Gift>().ReverseMap();

            //category
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>().ReverseMap();

            CreateMap<Ticket, TicketDto>()
                .ForMember(d => d.GiftName, o => o.MapFrom(s => s.Gift != null ? s.Gift.Title : ""));

            //purches
            CreateMap<CreatePurchaseDto, Purchase>().ReverseMap();
            CreateMap<Purchase, PurchaseDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Tickets, o => o.MapFrom(s => s.Tickets));

            CreateMap<Ticket, TicketAdminDto>()
                .ForMember(d => d.TicketId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.BuyerId, o => o.MapFrom(s => s.Purchase != null ? s.Purchase.BuyerId : 0))
                .ForMember(d => d.GiftId, o => o.MapFrom(s => s.GiftId))
                .ForMember(d => d.PurchaseId, o => o.MapFrom(s => s.PurchaseId))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt));

            CreateMap<Gift, LotteryResultDto>()
                .ForMember(dest => dest.GiftTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.WinnerName, opt => opt.MapFrom(src =>
                    src.Winner != null ? $"{src.Winner.UserName}" : "טרם הוגרל"));
        }   
    }
}