namespace TodoListApp
{
    public class TaskItem
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem(string description, DateTime dueDate)
        {
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;
        }
    }
}