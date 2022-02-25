namespace Moduit.DTO.Response.Question
{
    public class ResQ3FromModuitDto
    {
        public ResQ3FromModuitDto()
        {
            items = new List<ItemsDto>();
        }
        public long id { get; set; }
        public int category { get; set; }
        public List<ItemsDto>? items { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
