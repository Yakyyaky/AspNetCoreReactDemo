using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetCoreReactDemo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreReactDemo.Auth
{
    public class DenyUnlessLoggedInAuthorizationHandler : AuthorizationHandler<DenyUnlessLoggedInRequirement>, IAuthorizationRequirement
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IOptions<JsonOptions> _jsonOptions;

        public DenyUnlessLoggedInAuthorizationHandler(IHttpContextAccessor accessor, IOptions<JsonOptions> jsonOptions)
        {
            _accessor = accessor;
            _jsonOptions = jsonOptions;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DenyUnlessLoggedInRequirement requirement)
        {
            try
            {
                _accessor.HttpContext.Request.EnableBuffering();
                var stream = new StreamReader(_accessor.HttpContext.Request.Body);
                var body = await stream.ReadToEndAsync();
                if (string.IsNullOrWhiteSpace(body))
                {
                    // while we expect data to deserialize, I'm going to treat whitespace/empty string as {} with our 1 optional property requirement.
                    context.Succeed(requirement);
                    return;
                }

                var model = JsonSerializer.Deserialize<DenyUnlessLoggedInType>(body, _jsonOptions.Value.JsonSerializerOptions);
                var denyUnlessLoggedIn = model.DenyUnlessLoggedIn.GetValueOrDefault(false);
                if (!denyUnlessLoggedIn || IsLoggedIn(context))
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            catch (Exception)
            {
                // TODO: Log Something, someone's likely to decorate the API incorrectly or used the incorrect model.
            }
            finally
            {
                _accessor.HttpContext.Request.Body.Position = 0;
            }

            context.Fail();
        }

        private bool IsLoggedIn(AuthorizationHandlerContext context)
        {
            var user = context.User;
            var userIsAnonymous = user?.Identity == null || !user.Identities.Any(i => i.IsAuthenticated);
            return !userIsAnonymous;
        }
    }

    public class DenyUnlessLoggedInRequirement : IAuthorizationRequirement {}
}
