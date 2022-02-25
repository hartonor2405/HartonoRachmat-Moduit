using Moduit.DTO.Response.Question;

namespace Moduit.Interface
{
    public interface IScreeningService
    {
        Task<ResQuestionOneDto> GetQuestionOne();
        Task<List<ResQuestionTwoDto>> GetQuestionTwo();
        Task<List<ResQuestionThreeDto>> GetQuestionThree();
    }
}