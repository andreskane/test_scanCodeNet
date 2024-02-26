using Domain.Entities.Common;
using Infrastructure.Persistence;
using MediatR;

namespace Infrastructure;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<DomainEvent>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
