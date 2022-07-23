using System;
using System.Collections.Generic;
using MediatR;

namespace M2c.Domain.SeedWork
{

   public abstract class Entity
    {
        private List<INotification> _domainEvents;
    

      
        public bool Deleted { get; set; }
        public DateTime CreateDateTime { get;  set; }
        public DateTime? UpdateDateTime { get;  set; }
        public DateTime? DeleteDateTime { get;  set; }
        public long CreatedBy { get; protected set; }

        public void SetCreateDateTime()
        {
            CreateDateTime = DateTime.Now;
        }

        public void SetCreatedBy(long createdBy)
        {
            CreatedBy = createdBy;
        }



        public void SetUpdateDateTime()
        {
            UpdateDateTime = DateTime.Now;
        }

        public void SetDeleteDateTime()
        {
            DeleteDateTime = DateTime.Now;
        }

        public void SetDeleted()
        {
            Deleted = true;
        }

        public void UndoDeleted()
        {
            Deleted = false;
        }

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }






        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
