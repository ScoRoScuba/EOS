namespace EOS2.Infrastructure.Interfaces.Repository
{
    public interface IEventingContext
    {
        void StoreEvent(IEventData eventData);
    }
}
