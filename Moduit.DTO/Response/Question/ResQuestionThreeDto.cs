namespace Moduit.DTO.Response.Question
{
    public class ResQuestionThreeDto
    {
        public long id { get; set; }
        public int category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
