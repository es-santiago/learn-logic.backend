using LearnLogic.Domain.Enum;
using System;
using System.Linq;

namespace LearnLogic.Domain.Interfaces.Application
{
    public interface IUserClaimsAccessor
    {
        UserType UserType { get; }

        string UserName { get; }

        Guid UserId { get; }

        void LoadClaims(ILookup<string, string> claims);
    }
}
