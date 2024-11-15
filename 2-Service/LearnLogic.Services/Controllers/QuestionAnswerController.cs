using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.ViewModels;
using LearnLogic.Services.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLogic.API.Controllers
{
    public class QuestionAnswerController : BaseCQRSController
    {
        private readonly IQuestionApplication _service;
        public QuestionAnswerController(IQuestionApplication service)
        {
            _service = service;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Register([FromBody] QuestionAnswerViewModel vm)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await this._service.RegisterResponse(vm));

        [HttpGet, Route("questions-resolved"), EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, MaxExpansionDepth = 3), ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult QuestionsResolved([FromQuery] ODataQueryOptions<QuestionDTO> options = null)
        {
            var list = this._service.QuestionsResolved().ToList();

            Response.Headers.Add("X-Count", $"{list.Count()}");
            options.ApplyTo(list.AsQueryable());

            return Ok(list);
        }

        [HttpGet, Route("list/by-user"), EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, MaxExpansionDepth = 3), ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult AnswerUserList([FromQuery] ODataQueryOptions<QuestionAnswerDTO> options = null)
        {
            var list = this._service.ListByUser().ToList();

            Response.Headers.Add("X-Count", $"{list.Count()}");
            options.ApplyTo(list.AsQueryable());

            return Ok(list);
        }

        [HttpPut, Route("comment")]
        public async Task<IActionResult> Comment([FromBody] QuestionAnswerViewModel vm)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await this._service.Comment(vm));
    }
}
