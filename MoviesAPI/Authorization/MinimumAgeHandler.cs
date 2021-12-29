using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MoviesAPI.Authorization
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == ClaimTypes.DateOfBirth))
                return Task.CompletedTask;

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(claim =>
                claim.Type == ClaimTypes.DateOfBirth).Value);
            
            var age = DateTime.Today.Year - dateOfBirth.Year;

            if (dateOfBirth > DateTime.Today.AddYears(-age))
                age--;

            if (age >= requirement.MinimumAge)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
