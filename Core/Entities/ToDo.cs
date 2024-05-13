

using NodaTime;

namespace Core.Entities
{
    public class ToDo
    {
        private readonly IClock _clock;
        public long Id { get; set; }
        public string Name { get; set; }

        public long TenantId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? DeletedAtUtc { get; set; }

        public ToDo(string name, long tenantId, IClock clock)
        {
            Name = name;
            TenantId = tenantId;
            CreatedAtUtc = clock.GetCurrentInstant().ToDateTimeUtc();
        }

        public ToDo()
        {
        }
    }
}
