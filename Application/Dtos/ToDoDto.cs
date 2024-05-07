namespace Application.Dtos
{
    public class ToDoListDto
    {
        public List<ToDoDto> ToDos { get; set; }
    }
    public class ToDoDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
