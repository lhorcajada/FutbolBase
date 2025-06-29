using System.Text.Json.Serialization;
using MediatR;

namespace FutbolBase.Catalog.Api.App.Domain.Entities
{
    public class BaseEntity
    {
        public string Id { get; protected set; } = Guid.NewGuid().ToString();

        private readonly List<INotification> _domainEvents = new();
        
        [JsonIgnore]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
