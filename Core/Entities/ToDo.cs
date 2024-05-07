namespace Core.Entities
{
    public class ToDo
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ToDo(string name)
        {
            Name = name;
        }

        public ToDo()
        {
        }
    }
}
