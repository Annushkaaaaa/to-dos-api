namespace Core.Entities
{
    public class ToDo
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long TenantId { get; set; }

        public ToDo(string name, long tenantId)
        {
            Name = name;
            TenantId = tenantId;
        }

        public ToDo()
        {
        }
    }
}
