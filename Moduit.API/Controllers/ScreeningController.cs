using Microsoft.AspNetCore.Mvc;
using Moduit.Interface;

namespace Moduit.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScreeningController : ControllerBase
    {
        private readonly IScreeningService _iScreeningService;
        public ScreeningController(IScreeningService screeningService)
        {
            _iScreeningService = screeningService;
        }

        /// <summary>
        /// This Controller For Get Question One
        /// </summary>
        /// <returns></returns>
        [Route("[action]", Name = "QuestionOne")]
        [HttpGet]
        public async Task<ActionResult> QuestionOne()
        {
            var resp = await _iScreeningService.GetQuestionOne();
            return Ok(resp);
        }

        /// <summary>
        /// This Controller For Get Question Two
        /// </summary>
        /// <returns></returns>
        [Route("[action]", Name = "QuestionTwo")]
        [HttpGet]
        public async Task<ActionResult> QuestionTwo()
        {
            var resp = await _iScreeningService.GetQuestionTwo();
            return Ok(resp);
        }

        /// <summary>
        /// This Controller For Get Question Three
        /// </summary>
        /// <returns></returns>
        [Route("[action]", Name = "QuestionThree")]
        [HttpGet]
        public async Task<ActionResult> QuestionThree()
        {
            var resp = await _iScreeningService.GetQuestionThree();
            return Ok(resp);
        }
    }
}
