using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasty.Models;
using Microsoft.AspNetCore.Authorization;

namespace Tasty.Infrastructure
{
    public class ShopAuthorization
    {
        public class ShopAuthorizationRequirement : IAuthorizationRequirement
        {
            public bool AllowUsers { get; set; }
            public bool AllowOthers { get; set; }
        }

        public class ShopAuthorizationHandler
            : AuthorizationHandler<ShopAuthorizationRequirement>
        {

            protected override Task HandleRequirementAsync(
                    AuthorizationHandlerContext context,
                    ShopAuthorizationRequirement requirement)
            {
                Shop shop = context.Resource as Shop;
                string user = context.User.Identity.Name;
                StringComparison compare = StringComparison.OrdinalIgnoreCase;
                bool userRole = context.User.IsInRole("Administratorzy") || context.User.IsInRole("Kurier");
                if ((shop != null && user != null &&
                    ((requirement.AllowUsers && shop.Users.Any(u => u.UserName.Equals(user, compare))) || 
                    (requirement.AllowOthers && userRole))) || (shop == null && user != null && (requirement.AllowOthers && context.User.IsInRole("Administratorzy"))))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
                return Task.CompletedTask;
            }
        }
    }
}
