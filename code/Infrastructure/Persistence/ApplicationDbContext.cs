using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.DynamicFormAggregate;
using Domain.Entities.RulesAggregate;
using Infrastructure.Persistence.EntityConfig;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : ConnectureOS.Framework.Infrastructure.DbContextBase<ApplicationDbContext>
{
    public const string DEFAULT_SCHEMA = "Workflow";

    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<ApplicationDbContext> _logger;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;

    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {
    }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService, ILogger<ApplicationDbContext> logger) : base(options)
    {
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator, ICurrentUserService currentUserService, ILogger<ApplicationDbContext> logger) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _currentUserService = currentUserService;
        _logger = logger;
    }

    #region DBSets



    //public virtual DbSet<Employer> EmployerDbSet { get; set; }
    //public virtual DbSet<Pensioner> PensionerDbSet { get; set; }

    //public virtual DbSet<Employee> EmployeeDbSet { get; set; }

    //public virtual DbSet<Customer> PersonDbSet { get; set; }
    //public virtual DbSet<MaritalStatus> MaritalStatusDbSet { get; set; }
    //public virtual DbSet<PersonType> PersonTypeDbSet { get; set; }
    //public virtual DbSet<PersonType> StatusDbSet { get; set; }
    //public virtual DbSet<Gender> GenderDbSet { get; set; }
    //public virtual DbSet<City> CityDbSet { get; set; }
    //public virtual DbSet<Country> CountryDbSet { get; set; }
    //public virtual DbSet<Nationality> NationalityDbSet { get; set; }
    //public virtual DbSet<Province> ProvinceDbSet { get; set; }
    //public virtual DbSet<State> StateDbSet { get; set; }
    public virtual DbSet<DynamicForm> DynamicFormDbSet { get; set; }
    public virtual DbSet<DynamicFormItem> DynamicFormItemDbSet { get; set; }
    //   public virtual DbSet<DynamicFormProductAttributes> DynamicFormProductAttributesDbSet { get; set; }
    public virtual DbSet<DynamicFormPlan> DynamicFormPlan { get; set; }
    public virtual DbSet<DynamicFormComponentRule> DynamicFormComponentRule { get; set; }
    public virtual DbSet<RuleDynamic> RulesDbSet { get; set; }

    public virtual DbSet<BulkProcess> BulkProcessDbSet { get; set; }

    public virtual DbSet<BulckComponent> BulckComponentDbSet { get; set; }
    public virtual DbSet<RuleAction> RuleActionsDbSet { get; set; }
    public virtual DbSet<ActionParameter> ActionParametersDbSet { get; set; }


    #endregion

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        AddAuditData();
        var result = await base.SaveChangesAsync(cancellationToken);

        await _mediator.DispatchDomainEventsAsync(this);
        return result;
    }

    private void AddAuditData()
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserName ?? "NA";
                    entry.Entity.Created = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserName ?? "NA";
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }
    }

    private void StringsToUpper()
    {
        foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
        {
            foreach (var propInfo in entry.Entity.GetType().GetProperties())
            {
                if (propInfo.PropertyType == typeof(string) &&
                    propInfo.CanWrite &&
                    propInfo.GetSetMethod(true).IsPublic)
                {
                    var value = propInfo.GetValue(entry.Entity);
                    if (value != null)
                        propInfo.SetValue(entry.Entity, (value as string).ToUpper());
                }
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DynamicForm>().ToTable(nameof(DynamicForm));

        // Apply entity configurations
        modelBuilder.ApplyConfiguration(new DynamicFormConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}