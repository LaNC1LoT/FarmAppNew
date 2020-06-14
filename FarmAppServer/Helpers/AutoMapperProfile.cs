using System;
using System.Collections.Generic;
using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmAppServer.Models;
using FarmAppServer.Models.ApiMethods;
using FarmAppServer.Models.CodeAthTypes;
using FarmAppServer.Models.Drugs;
using FarmAppServer.Models.Pharmacies;
using FarmAppServer.Models.Regions;
using FarmAppServer.Models.Roles;
using FarmAppServer.Models.Sales;
using FarmAppServer.Models.Users;
using FarmAppServer.Models.Vendors;
using static System.Decimal;

namespace FarmAppServer.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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

            //vendor
            CreateMap<Vendor, VendorDto>().ReverseMap();
            CreateMap<Vendor, PostVendorDto>().ReverseMap();
            CreateMap<Vendor, UpdateVendorDto>().ReverseMap();

            //regions
            CreateMap<Region, RegionDto>()
                .ForMember(x => x.ParentRegionName,
                    o => o.MapFrom(s => s.ParentRegion.RegionName))
                .ForMember(x => x.RegionTypeName,
                    o => o.MapFrom(s => s.RegionType.RegionTypeName))
                .ForMember(x => x.RegionTypeId,
                    o => o.MapFrom(s => s.RegionType.Id));

            CreateMap<Region, RegionSearchDto>()
                .ForMember(x => x.ChildRegion,
                    o =>
                        o.MapFrom(s => s.RegionId));
            
            CreateMap<Region, UpdateRegionDto>().ReverseMap();
            CreateMap<PostRegionDto, Region>()
                .ForMember(x => x.RegionId,
                    o => o.MapFrom(s => s.ParentId));

            //region types
            CreateMap<RegionType, RegionTypeDto>().ReverseMap();
            CreateMap<RegionType, PostRegionTypeDto>().ReverseMap();
            
            //roles
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, UpdateRoleDto>().ReverseMap();
            CreateMap<Role, PostRoleDto>().ReverseMap();

            //pharmacies
            CreateMap<Pharmacy, PharmacyDto>().ReverseMap();
            CreateMap<Pharmacy, PharmacyFilterDto>()
                .ForMember(x => x.ParentPharmacyId,
                    o => o.MapFrom(s => s.PharmacyId))
                .ForMember(x => x.ParentPharmacyName,
                    o => o.MapFrom(s => s.ParentPharmacy.PharmacyName))
                .ForMember(x => x.RegionName,
                    o => o.MapFrom(s => s.Region.RegionName));
            CreateMap<PharmacyFilterDto,Pharmacy>();
            CreateMap<PharmacyDto, Pharmacy>().ReverseMap();
            CreateMap<PostPharmacyDto, Pharmacy>().ReverseMap();

            //drugs
            CreateMap<Drug, DrugDto>()
                .ForMember(x => x.CodeAthTypeName,
                    o => o.MapFrom(s => s.CodeAthType.NameAth))
                .ForMember(x => x.Code,
                    o => o.MapFrom(s => s.CodeAthType.Code))
                .ForMember(x => x.VendorId,
                    o => o.MapFrom(s => s.Vendor.Id))
                .ForMember(x => x.VendorName,
                    o => o.MapFrom(s => s.Vendor.VendorName));
                //.ForMember(x => x.IsDomestic,
                 //   o => o.MapFrom(s => s.Vendor.IsDomestic));
            CreateMap<PostDrugDto, Drug>().ReverseMap();

            //sales
            CreateMap<Sale, SaleDto>()
                .ForMember(x => x.DrugName,
                    o => o.MapFrom(s => s.Drug.DrugName))
                .ForMember(x => x.PharmacyName,
                    o => o.MapFrom(s => s.Pharmacy.PharmacyName))
                .ForMember(x => x.Amount,
                    o => o.MapFrom(s => Multiply(s.Price, s.Quantity)))
                .ForMember(x => x.SaleImportFileName,
                    o => o.MapFrom(s => s.SaleImportFile.FileName))
                .ReverseMap();

            CreateMap<Sale, UpdateSaleDto>();
            CreateMap<UpdateSaleDto, Sale>()
                .ForMember(x => x.Amount,
                    o => o.MapFrom(s => Multiply(s.Price, s.Quantity)))
                .ForMember(x => x.Price, o => o.Ignore());

            CreateMap<PostSaleDto, Sale>()
                .ForMember(x => x.Amount,
                    o => o.MapFrom(s => Multiply(s.Price, s.Quantity)))
                .ReverseMap();

            //CodeAthType
            CreateMap<CodeAthType, CodeAthTypeDto>()
                .ForMember(x => x.ParentCodeAthId,
                    o => o.MapFrom(s => s.CodeAthId))
                .ForMember(x => x.ParentCodeName,
                    o => o.MapFrom(s => s.CodeAth.Code));

            CreateMap<UpdateRegionDto, CodeAthType>().ReverseMap();

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