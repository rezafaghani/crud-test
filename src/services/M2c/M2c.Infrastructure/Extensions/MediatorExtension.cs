using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M2c.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace M2c.Infrastructure.Extensions
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, M2CDbContext ctx)
        {
            IEnumerable<EntityEntry<Entity>> domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            {
                List<EntityEntry<Entity>> entityEntries = domainEntities.ToList();
                List<INotification> domainEvents = entityEntries
                    .SelectMany(x => x.Entity.DomainEvents)
                    .ToList();

                entityEntries.ToList()
                    .ForEach(entity => entity.Entity.ClearDomainEvents());

                foreach (INotification domainEvent in domainEvents)
                    await mediator.Publish(domainEvent);
            }
        }
    }
}