using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearnLogic.Services.Controllers
{
    public class UsersController : BaseController<UserViewModel, UserDTO, IUserApplication>
    {
        public UsersController(IUserApplication service) : base(service) { }

        [HttpGet, Route("me")]
        public IActionResult GetLogged()
            => CustomResponse(Service.GetLogged());

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel obj)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await this.Service.ChangePassword(obj));

        [HttpPut("register-initial-point")]
        public async Task<IActionResult> RegisterInitialPoint([FromBody] UserViewModel obj)
            => !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await this.Service.RegisterInitialPoint(obj));
    }
}
