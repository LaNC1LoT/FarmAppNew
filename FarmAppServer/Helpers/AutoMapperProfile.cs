using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmAppServer.Models;
using FarmAppServer.Models.ApiMethods;
using FarmAppServer.Models.CodeAthTypes;
using FarmAppServer.Models.Drugs;
using FarmAppServer.Models.Users;

namespace FarmAppServer.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pharmacy, PharmacyDto>().ForMember(x => x.RegionName, y => y.MapFrom(m => m.Region.RegionName));
            CreateMap<PharmacyDto, Pharmacy>();

            CreateMap<Vendor, VendorDto>().ForMember(x => x.RegionName, y => y.MapFrom(m => m.Region.RegionName));
            CreateMap<VendorDto, Vendor>();

            CreateMap<CodeAthType, CodeAthTypeDto>().ReverseMap();

            CreateMap<RegionType, RegionTypeDto>().ReverseMap();

            CreateMap<Region, RegionDto>().ForMember(x => x.RegionTypeName, y => y.MapFrom(m => m.RegionType.RegionTypeName));
            CreateMap<RegionDto, Region>();

            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<DosageFormType, DosageFormDto>().ReverseMap();

            CreateMap<Drug, DrugDto>().ForMember(x => x.Code, o => o.MapFrom(s => s.CodeAthType.Code))
                                      .ForMember(x => x.VendorName, o => o.MapFrom(s => s.Vendor.VendorName))
                                      .ForMember(x => x.DosageForm, o => o.MapFrom(s => s.DosageFormType.DosageForm))
                                      .ForMember(x => x.IsDomestic, o => o.MapFrom(s => s.Vendor.IsDomestic));
            CreateMap<PostDrugDto, Drug>().ReverseMap();

            CreateMap<Sale, SaleDto>().ForMember(x => x.DrugName, o => o.MapFrom(s => s.Drug.DrugName))
                                      .ForMember(x => x.PharmacyName, o => o.MapFrom(s => s.Pharmacy.PharmacyName));
            CreateMap<SaleDto, Sale>();
                











            //users map
            CreateMap<Role, UserRoleDto>().ReverseMap();

            CreateMap<User, UserModelDto>()
                .ForMember(x => x.Role,
                    o => o.MapFrom(s => s.Role));

            CreateMap<User, AuthResponseDto>()
                .ForMember(x => x.Role,
                    o => o.MapFrom(s => s.Role));

            CreateMap<RegisterModelDto, User>()
                .ForMember(x => x.UserName,
                    o => o.MapFrom(s => s.FirstName + " " + s.LastName));
            CreateMap<UpdateModelDto, User>()
                .ForMember(x => x.UserName,
                    o => o.MapFrom(s => s.FirstName + " " + s.LastName));

            //map for UserFilterByRole 
            CreateMap<User, UserFilterByRoleDto>()
                .ForMember(x => x.UserName,
                    o => o.MapFrom(s => s.FirstName + " " + s.LastName));




  







            //drugs

            //sales
            //CreateMap<Sale, SaleDto>()
            //    .ForMember(x => x.DrugName,
            //        o => o.MapFrom(s => s.Drug.DrugName))
            //    .ForMember(x => x.PharmacyName,
            //        o => o.MapFrom(s => s.Pharmacy.PharmacyName))
            //    .ForMember(x => x.Amount,
            //        o => o.MapFrom(s => Multiply(s.Price, s.Quantity)))
            //    .ForMember(x => x.SaleImportFileName,
            //        o => o.MapFrom(s => s.SaleImportFile.FileName))
            //    .ReverseMap();

            //CreateMap<Sale, UpdateSaleDto>();
            //CreateMap<UpdateSaleDto, Sale>()
            //    .ForMember(x => x.Amount,
            //        o => o.MapFrom(s => Multiply(s.Price, s.Quantity)))
            //    .ForMember(x => x.Price, o => o.Ignore());

            //CreateMap<PostSaleDto, Sale>()
            //    .ForMember(x => x.Amount,
            //        o => o.MapFrom(s => Multiply(s.Price, s.Quantity)))
            //    .ReverseMap();



            //ApiMethods
            CreateMap<ApiMethod, ApiMethodDto>().ReverseMap();
            CreateMap<ApiMethodDto, UpdateApiMethodDto>().ReverseMap();

            //ApiMethodRoles
            CreateMap<ApiMethodRole, ApiMethodRoleDto>()
                .ForMember(x => x.ApiMethodName,
                    o => o.MapFrom(s => s.ApiMethod.ApiMethodName))
                .ForMember(x => x.RoleName,
                    o => o.MapFrom(s => s.Role.RoleName));
        }
    }
}