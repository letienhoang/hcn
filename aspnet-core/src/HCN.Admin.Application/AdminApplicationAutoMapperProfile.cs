using AutoMapper;
using HCN.Admin.Catalog.FormulaCategories;
using HCN.Formulas;

namespace HCN.Admin;

public class AdminApplicationAutoMapperProfile : Profile
{
    public AdminApplicationAutoMapperProfile()
    {
        //Formula Category
        CreateMap<FormulaCategory, FormulaCategoryDto>();
        CreateMap<FormulaCategory, FormulaCategoryInListDto>();
        CreateMap<CreateUpdateFormulaCategoryDto, FormulaCategory>();

        ////Formula
        //CreateMap<Formula, FormulaDto>();
        //CreateMap<Formula, FormulaInListDto>();
        //CreateMap<CreateUpdateFormulaDto, Formula>();

        ////Role
        //CreateMap<IdentityRole, RoleDto>().ForMember(x => x.Description,
        //    map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
        //    ? x.ExtraProperties[RoleConsts.DescriptionFieldName]
        //    : null));
        //CreateMap<IdentityRole, RoleInListDto>()
        //    .ForMember(x => x.Description,
        //    map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
        //    ? x.ExtraProperties[RoleConsts.DescriptionFieldName]
        //    : null));
        //CreateMap<CreateUpdateRoleDto, IdentityRole>();

        ////User
        //CreateMap<IdentityUser, UserDto>();
        //CreateMap<IdentityUser, UserInListDto>();
    }
}