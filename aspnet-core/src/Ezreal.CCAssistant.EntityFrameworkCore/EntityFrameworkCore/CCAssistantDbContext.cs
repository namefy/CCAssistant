using Ezreal.CCAssistant.Customers;
using Ezreal.CCAssistant.Histories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Ezreal.CCAssistant.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class CCAssistantDbContext :
    AbpDbContext<CCAssistantDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Customer> Customers { get; set; }

    public DbSet<History> Histories { get; set; }

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
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public CCAssistantDbContext(DbContextOptions<CCAssistantDbContext> options)
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

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(CCAssistantConsts.DbTablePrefix + "YourEntities", CCAssistantConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.UseCollation("Chinese_PRC_CS_AS_KS_WS");

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        builder.Entity<History>(b =>
        {
            b.ToTable(CCAssistantConsts.DbTablePrefix + nameof(Histories), CCAssistantConsts.DbSchema, p => p.HasComment("历史记录"));

            b.ConfigureByConvention();

            b.Property(p => p.Id).HasComment("Id");

            b.Property(p => p.HistoryRecords)
            .IsRequired()
            .HasConversion
            (
                p => JsonSerializer.Serialize(p, jsonSerializerOptions),
                p => JsonSerializer.Deserialize<List<HistoryRecord>>(p, jsonSerializerOptions)
            )
            .HasComment("历史记录");
        });

        builder.Entity<Customer>(b =>
        {
            b.ToTable(CCAssistantConsts.DbTablePrefix + nameof(Customers), CCAssistantConsts.DbSchema, p => p.HasComment("客户信息"));

            b.ConfigureByConvention();

            b.Property(p => p.Id).HasComment("Id");

            b.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(CustomerConsts.NameMaxLength)
            .HasComment("客户名称");

            b.Property(p => p.Gender)
            .IsRequired()
            .HasComment("客户性别");

            b.Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(CustomerConsts.PhoneLength)
            .HasComment("客户手机号码");

            b.Property(p => p.Amount)
            .HasComment("账户余额");

            b.Property(p => p.LastExpenditureTime)
            .HasColumnType("datetime2")
            .IsRequired(false)
            .HasComment("最近消费时间");

        });


    }
}
