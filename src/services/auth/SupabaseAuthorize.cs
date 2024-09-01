
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace services.auth;

// for some reason, [Authorize] does not work for Supabase.
public class SupabaseAuthorize : Attribute, IAsyncAuthorizationFilter
{
    public SupabaseAuthorize()
    {
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userId = new Guid(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

        if (userId == null)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}