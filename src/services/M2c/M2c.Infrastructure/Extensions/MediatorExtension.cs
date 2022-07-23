using System.Linq;
using System.Threading.Tasks;
using M2c.Domain.SeedWork;
using MediatR;

namespace M2c.Infrastructure.Extensions
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, M2CDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            {
                var entityEntries = domainEntities.ToList();
                var domainEvents = entityEntries
                    .SelectMany(x => x.Entity.DomainEvents)
                    .ToList();

                entityEntries.ToList()
                    .ForEach(entity => entity.Entity.ClearDomainEvents());

                foreach (var domainEvent in domainEvents)
                    await mediator.Publish(domainEvent);
            }
        }
    }
}