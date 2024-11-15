using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLogic.Services.Controllers
{
    public class QuestionsController : BaseController<QuestionViewModel, QuestionDTO, IQuestionApplication>
    {
        public QuestionsController(IQuestionApplication service) : base(service) { }

        [HttpGet, Route("list"), EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, MaxExpansionDepth = 3), ApiExplorerSettings(IgnoreApi = true)]
        public override IActionResult List([FromQuery] ODataQueryOptions<QuestionDTO> options = null)
        {
            var list = this.Service.List().ToList();

            Response.Headers.Add("X-Count", $"{list.Count()}");
            options.ApplyTo(list.AsQueryable());

            return Ok(list);
        }

        [HttpPut, Route("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] QuestionViewModel vm)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await this.Service.UpdateStatus(vm));


        [HttpGet, Route("search-with-answer/{id}")]
        public virtual IActionResult SearchForCorrectItem(Guid id)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(this.Service.SearchWithAnswer(id));
    }
}
