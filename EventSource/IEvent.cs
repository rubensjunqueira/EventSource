namespace EventSource
{
    public interface IEvent
    {
        DateTime ShootingDate { get; }
        string Message { get; }
        Guid UserId { get; }
        string UserName { get; }
        string UrlEvent { get; }
        bool Durable { get; }
    }

    public class EventBase : IEvent
    {
        public DateTime ShootingDate { get; private set; }

        public Guid UserId { get; private set; }

        public string UserName { get; private set; }

        public string? UrlEvent { get; private set; }

        public string Message { get; private set; }

        public bool Durable { get; private set; }

        public EventBase(DateTime shootingDate, Guid userId, string userName, string message, string? urlEvent = null, bool durable = false)
        {
            ShootingDate = shootingDate;
            UserId = userId;
            UserName = userName;
            Message = message;
            UrlEvent = urlEvent;
            Durable = durable;
        }
    }
}