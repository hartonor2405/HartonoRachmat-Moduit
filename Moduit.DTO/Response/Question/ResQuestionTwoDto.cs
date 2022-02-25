namespace Moduit.DTO.Response.Question
{
    public class ResQuestionTwoDto
    {
        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public DateTime? createdAt { get; set; }
        public List<string>? tags { get; set; }
    }
}
