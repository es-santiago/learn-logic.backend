using LearnLogic.Domain.Enum;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Infra.CrossCutting.Identity;

namespace LearnLogic.Application.Services
{
    public class UserClaimsAccessor : IUserClaimsAccessor
    {
        private ILookup<string, string> claims;

        public Guid UserId => Guid.Parse(claims[CustomClaimTypes.UserId].First());

        public string UserName => claims[CustomClaimTypes.UserName].First();

        public UserType UserType => claims == null ? UserType.Undefined : (UserType)int.Parse(claims[CustomClaimTypes.Role].First());

        public void LoadClaims(ILookup<string, string> claims)
        {
            this.claims = claims;
        }
    }
}
