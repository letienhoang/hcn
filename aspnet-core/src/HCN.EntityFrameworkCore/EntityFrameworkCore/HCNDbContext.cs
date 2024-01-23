using HCN.Configurations.Formulas;
using HCN.Configurations.Materials;
using HCN.Configurations.Reviews;
using HCN.Configurations.Stories;
using HCN.Configurations.Tags;
using HCN.Configurations.Tools;
using HCN.Configurations.Units;
using HCN.Formulas;
using HCN.Materials;
using HCN.Reviews;
using HCN.Stories;
using HCN.Tags;
using HCN.Tools;
using HCN.Units;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace HCN.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class HCNDbContext :
    AbpDbContext<HCNDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    #region Entities from Holwn Custom
    // HCN Entities
    public DbSet<Formula> Formulas { get; set; }
    public DbSet<FormulaCategory> FormulaCategories { get; set; }
    public DbSet<FormulaMaterial> MaterialFormulas { get; set; }
    public DbSet<FormulaStep> FormulaSteps { get; set; }
    public DbSet<FormulaTool> ToolFormulas { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<MaterialCategory> MaterialCategories { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TagFormula> TagFormulas { get; set; }
    public DbSet<TagMaterial> TagMaterials { get; set; }
    public DbSet<TagStep> TagSteps { get; set; }
    public DbSet<TagStory> TagStorys { get; set; }
    public DbSet<TagTool> TagTools { get; set; }
    public DbSet<Tool> Tools { get; set; }
    public DbSet<ToolCategory> ToolCategorys { get; set; }
    public DbSet<Unit> Units { get; set; }

    #endregion

    public HCNDbContext(DbContextOptions<HCNDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.ApplyConfiguration(new FormulaCategoryConfiguration());
        builder.ApplyConfiguration(new FormulaConfiguration());
        builder.ApplyConfiguration(new FormulaMaterialConfiguration());
        builder.ApplyConfiguration(new FormulaStepConfiguration());
        builder.ApplyConfiguration(new FormulaToolConfiguration());
        builder.ApplyConfiguration(new MaterialCategoryConfiguration());
        builder.ApplyConfiguration(new MaterialConfiguration());
        builder.ApplyConfiguration(new ReviewConfiguration());
        builder.ApplyConfiguration(new StoryConfiguration());
        builder.ApplyConfiguration(new TopicConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new TagFormulaConfiguration());
        builder.ApplyConfiguration(new TagMaterialConfiguration());
        builder.ApplyConfiguration(new TagStepConfiguration());
        builder.ApplyConfiguration(new TagStoryConfiguration());
        builder.ApplyConfiguration(new TagToolConfiguration());
        builder.ApplyConfiguration(new ToolCategoryConfiguration());
        builder.ApplyConfiguration(new ToolConfiguration());
        builder.ApplyConfiguration(new UnitConfiguration());
    }
}
