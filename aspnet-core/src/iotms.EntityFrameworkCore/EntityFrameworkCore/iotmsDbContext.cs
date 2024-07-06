using iotms.Devices;
using iotms.Accounts;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace iotms.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class iotmsDbContext :
    AbpDbContext<iotmsDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<Device> Devices { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public iotmsDbContext(DbContextOptions<iotmsDbContext> options)
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
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(iotmsConsts.DbTablePrefix + "YourEntities", iotmsConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Account>(b =>
            {
                b.ToTable(iotmsConsts.DbTablePrefix + "Accounts", iotmsConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Account.Name)).HasMaxLength(AccountConsts.NameMaxLength);
                b.Property(x => x.Location).HasColumnName(nameof(Account.Location)).HasMaxLength(AccountConsts.LocationMaxLength);
                b.Property(x => x.Address).HasColumnName(nameof(Account.Address)).HasMaxLength(AccountConsts.AddressMaxLength);
                b.Property(x => x.Contact).HasColumnName(nameof(Account.Contact));
                b.Property(x => x.Email).HasColumnName(nameof(Account.Email));
                b.Property(x => x.Web).HasColumnName(nameof(Account.Web)).HasMaxLength(AccountConsts.WebMaxLength);
                b.Property(x => x.Rooms).HasColumnName(nameof(Account.Rooms));
                b.Property(x => x.Status).HasColumnName(nameof(Account.Status));
                b.HasMany(x => x.Devices).WithOne().HasForeignKey(x => x.AccountId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Device>(b =>
            {
                b.ToTable(iotmsConsts.DbTablePrefix + "Devices", iotmsConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Device.Name)).IsRequired().HasMaxLength(DeviceConsts.NameMaxLength);
                b.Property(x => x.Status).HasColumnName(nameof(Device.Status));
                b.Property(x => x.Temp).HasColumnName(nameof(Device.Temp));
                b.Property(x => x.LDR).HasColumnName(nameof(Device.LDR));
                b.Property(x => x.PIR).HasColumnName(nameof(Device.PIR));
                b.Property(x => x.Door).HasColumnName(nameof(Device.Door));
                b.Property(x => x.MinTempAlert).HasColumnName(nameof(Device.MinTempAlert)).HasMaxLength((int)DeviceConsts.MinTempAlertMaxLength);
                b.Property(x => x.TempAlertFreq).HasColumnName(nameof(Device.TempAlertFreq)).IsRequired().HasMaxLength((int)DeviceConsts.TempAlertFreqMaxLength);
                b.Property(x => x.MinLDRAlert).HasColumnName(nameof(Device.MinLDRAlert)).HasMaxLength((int)DeviceConsts.MinLDRAlertMaxLength);
                b.Property(x => x.LDRAlertFreq).HasColumnName(nameof(Device.LDRAlertFreq)).IsRequired().HasMaxLength((int)DeviceConsts.LDRAlertFreqMaxLength);
                b.Property(x => x.Connection).HasColumnName(nameof(Device.Connection));
                b.HasOne<Account>().WithMany(x => x.Devices).HasForeignKey(x => x.AccountId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}