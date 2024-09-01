
using Microsoft.AspNetCore.Mvc;
using services.contacts;
using services.auth;
using System.Security.Claims;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsHandler _contactsHandler;
        public ContactsController(IContactsHandler contactsHandler)
        {
            _contactsHandler = contactsHandler;
        }

        [SupabaseAuthorize]
        [HttpGet]
        public async Task<dynamic> GetContacts(int? p = 1, int? c = 10, string? search = null)
        {
            var userId = new Guid(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var contacts = await _contactsHandler.RetrieveContacts(p, c, userId, search);

            return contacts;
        }
    }
}