using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmAppServer.Models;

namespace FarmAppServer.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            /// ApiMethod
            CreateMap<ApiMethod, ApiMethodDto>().ReverseMap();

            /// CodeAthType
            CreateMap<CodeAthType, CodeAthTypeDto>().ReverseMap();

            /// RegionType
            CreateMap<RegionType, RegionTypeDto>().ReverseMap();

            /// Role
            CreateMap<Role, RoleDto>().ReverseMap();

            /// Log
            CreateMap<Log, LogDto>();

            /// Pharmacy
            CreateMap<PharmacyDto, Pharmacy>();
            CreateMap<Pharmacy, PharmacyDto>().ForMember(x => x.RegionName, y => y.MapFrom(m => m.Region.RegionName));

            /// Vendor
            CreateMap<VendorDto, Vendor>();
            CreateMap<Vendor, VendorDto>().ForMember(x => x.RegionName, y => y.MapFrom(m => m.Region.RegionName));

            /// Region
            CreateMap<RegionDto, Region>();
            CreateMap<Region, RegionDto>().ForMember(x => x.RegionTypeName, y => y.MapFrom(m => m.RegionType.RegionTypeName));

            /// Stock
            CreateMap<StockDto, Stock>();
            CreateMap<Stock, StockDto>().ForMember(x => x.PharmacyName, y => y.MapFrom(m => m.Pharmacy.PharmacyName))
                                        .ForMember(x => x.DrugName, y => y.MapFrom(m => m.Drug.DrugName));

            /// DosageFormType
            CreateMap<DosageFormType, DosageFormDto>().ReverseMap();

            /// Drug
            CreateMap<Drug, DrugDto>().ForMember(x => x.Code, o => o.MapFrom(s => s.CodeAthType.Code))
                                      .ForMember(x => x.VendorName, o => o.MapFrom(s => s.Vendor.VendorName))
                                      .ForMember(x => x.DosageForm, o => o.MapFrom(s => s.DosageFormType.DosageForm))
                                      .ForMember(x => x.IsDomestic, o => o.MapFrom(s => s.Vendor.IsDomestic));

            /// Sale
            CreateMap<SaleDto, Sale>();
            CreateMap<Sale, SaleDto>().ForMember(x => x.DrugName, o => o.MapFrom(s => s.Drug.DrugName))
                                      .ForMember(x => x.PharmacyName, o => o.MapFrom(s => s.Pharmacy.PharmacyName));

            /// ApiMethodRole
            CreateMap<ApiMethodRoleDto, ApiMethodRole>();
            CreateMap<ApiMethodRole, ApiMethodRoleDto>().ForMember(x => x.ApiMethodName, o => o.MapFrom(s => s.ApiMethod.ApiMethodName))
                                                        .ForMember(x => x.RoleName, o => o.MapFrom(s => s.Role.RoleName));

            /// User
            CreateMap<User, UserResponseDto>().ForMember(x => x.Role, o => o.MapFrom(s => s.Role));
            CreateMap<User, UserDto>().ForMember(x => x.RoleName, o => o.MapFrom(s => s.Role.RoleName))
                                      .ForMember(x => x.Password, o => o.MapFrom(o => "******"));
            CreateMap<UserDto, User>();
        }
    }
}