using Moduit.Constant;
using Moduit.DTO.Response.Question;
using Moduit.Interface;
using Moduit.Util;
using Moduit.Util.HttpClientManager;

namespace Moduit.BusinessService
{
    public class ScreeningService : IScreeningService
    {
        private string BaseURL { get; }

        public ScreeningService()
        {
            BaseURL = AppConfiguration.AppSetting["URL:BaseURL"];
        }

        public virtual async Task<ResQuestionOneDto> GetQuestionOne()
        {
            string QuestionOne = AppConfiguration.AppSetting["URL:QuestionOne"];
            var Resp = await HttpHelper.GetRequest<ResQuestionOneDto>(BaseURL, QuestionOne, null, null);
            return Resp;
        }

        public virtual async Task<List<ResQuestionTwoDto>> GetQuestionTwo()
        {
            string QuestionTwo = AppConfiguration.AppSetting["URL:QuestionTwo"];
            var Resp = await HttpHelper.GetRequest<List<ResQuestionTwoDto>>(BaseURL, QuestionTwo, null, null);
            Resp = (from r in Resp
                    from r2 in r.tags?? new List<string>()
                    where (r.description.Contains(QuestionConstant.Ergonomics) || r.title.Contains(QuestionConstant.Ergonomics)) && r2.Contains(QuestionConstant.Sports)
                    orderby r.id descending
                    select r).Take(3).ToList();
            return Resp;
        }

        public virtual async Task<List<ResQuestionThreeDto>> GetQuestionThree()
        {
            string QuestionThree = AppConfiguration.AppSetting["URL:QuestionThree"];
            var Resp = await HttpHelper.GetRequest<List<ResQ3FromModuitDto>>(BaseURL, QuestionThree, null, null);
            

            var newResponse = (from r in Resp
                                from r2 in r.items?? new List<ItemsDto>()
                                select new ResQuestionThreeDto()
                                {
                                    id = r.id,
                                    category = r.category,
                                    createdAt = r.createdAt,
                                    title = r2.title,
                                    description = r2.description,
                                    footer = r2.footer
                                }).ToList();
            return newResponse;
        }
    }
}