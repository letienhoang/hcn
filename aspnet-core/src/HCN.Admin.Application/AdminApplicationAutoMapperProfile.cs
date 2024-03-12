using AutoMapper;
using HCN.Admin.Catalog.FormulaCategories;
using HCN.Admin.Catalog.MaterialCategories;
using HCN.Admin.Catalog.ToolCategories;
using HCN.Admin.Catalog.Topics;
using HCN.Admin.System.Roles;
using HCN.Admin.System.Users;
using HCN.Formulas;
using HCN.Materials;
using HCN.Roles;
using HCN.Stories;
using HCN.Tools;
using Volo.Abp.Identity;

namespace HCN.Admin;

public class AdminApplicationAutoMapperProfile : Profile
{
    public AdminApplicationAutoMapperProfile()
    {
        //Formula Category
        CreateMap<FormulaCategory, FormulaCategoryDto>();
        CreateMap<FormulaCategory, FormulaCategoryInListDto>();
        CreateMap<CreateUpdateFormulaCategoryDto, FormulaCategory>();

        //Material Category
        CreateMap<MaterialCategory, MaterialCategoryDto>();
        CreateMap<MaterialCategory, MaterialCategoryInListDto>();
        CreateMap<CreateUpdateMaterialCategoryDto, MaterialCategory>();

        //Tool Category
        CreateMap<ToolCategory, ToolCategoryDto>();
        CreateMap<ToolCategory, ToolCategoryInListDto>();
        CreateMap<CreateUpdateToolCategoryDto, ToolCategory>();


        //Topic
        CreateMap<Topic, TopicDto>();
        CreateMap<Topic, TopicInListDto>();
        CreateMap<CreateUpdateTopicDto, Topic>();

        ////Formula
        //CreateMap<Formula, FormulaDto>();
        //CreateMap<Formula, FormulaInListDto>();
        //CreateMap<CreateUpdateFormulaDto, Formula>();

        //Role
        CreateMap<IdentityRole, RoleDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ? x.ExtraProperties[RoleConsts.DescriptionFieldName]
            : null));
        CreateMap<IdentityRole, RoleInListDto>()
            .ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ? x.ExtraProperties[RoleConsts.DescriptionFieldName]
            : null));
        CreateMap<CreateUpdateRoleDto, IdentityRole>();

        //User
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentityUser, UserInListDto>();
    }
}