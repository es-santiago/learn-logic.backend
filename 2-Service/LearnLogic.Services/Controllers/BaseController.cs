using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;

namespace LearnLogic.Services.Controllers
{
    [Authorize, ApiController]
    public class BaseController<TVm, TDto, TService> : BaseCQRSController where TVm : BaseViewModel, new() where TDto : BaseDTO, new() where TService : IBaseApplication<TVm, TDto>
    {
        protected readonly TService Service;

        public BaseController(TService service)
            => Service = service;

        [HttpPost, Route("")]
        public virtual async Task<IActionResult> Register([FromBody] TVm vm)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await this.Service.Register(vm));

        [HttpPut, Route("")]
        public virtual async Task<IActionResult> Update([FromBody] TVm vm)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await this.Service.Update(vm));

        [HttpPut, Route("change-status/{id}")]
        public virtual async Task<IActionResult> ChangeStatus(Guid id)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await this.Service.ChangeStatus(id));

        [HttpGet, Route("get/{id}")]
        public virtual IActionResult Get(Guid id)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(this.Service.Get(id));

        [HttpGet, Route("list"), EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, MaxExpansionDepth = 3), ApiExplorerSettings(IgnoreApi = true)]
        public virtual IActionResult List([FromQuery] ODataQueryOptions<TDto> options = null)
        {
            var list = this.Service.List();

            Response.Headers.Add("X-Count", $"{list.Count()}");
            options.ApplyTo(list.AsQueryable());

            return Ok(list);
        }
    }
}
