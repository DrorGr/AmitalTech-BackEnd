namespace AmitalBE.Request
{
    public class NewTaskRequest
    {

        public string Subject { get; set; }
        public int UserId { get; set; }
        public DateTime TargetDate { get; set; }
        public bool IsCompleted { get; set; }

    }
}
