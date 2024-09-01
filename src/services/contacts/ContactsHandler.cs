
using data.contacts;
using Microsoft.AspNetCore.Mvc;
using repo;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace services.contacts
{
    public interface IContactsHandler
    {
        public Task<Contact[]> RetrieveContacts(int? page, int? count, Guid userId, string? name = null);
    }

    public class ContactsHandler : IContactsHandler
    {
        private readonly PigeonContext _dbContext;
        public ContactsHandler(PigeonContext dbCtx)
        {
            _dbContext = dbCtx;
        }
        
        public async Task<Contact[]> RetrieveContacts(int? page, int? count, Guid userId, string? name = null)
        {
            if (name == null)
            {
                return await (from dm in _dbContext.DirectMessages
                                 join ua in _dbContext.Users on dm.UserAId equals ua.UserId
                                 join ub in _dbContext.Users on dm.UserBId equals ub.UserId
                                 select new Contact
                                 {
                                    ChatId = dm.ChatId,
                                    UserAId = dm.UserAId,
                                    UserAName = ua.Username,
                                    UserBId = dm.UserBId,
                                    UserBName = dm.UserB.Username,
                                    LastMessage = _dbContext.Messages.OrderByDescending(dm => dm.DateCreated).FirstOrDefault(m => m.DirectMessageId == dm.ChatId)
                                 }).Where(c => c.UserAId == userId).Skip((int)((page - 1) * count)).Take((int)count).ToArrayAsync();
            }
            else
            {
                return await (from dm in _dbContext.DirectMessages
                                 join ua in _dbContext.Users on dm.UserAId equals ua.UserId
                                 join ub in _dbContext.Users on dm.UserBId equals ub.UserId
                                 where ub.Username == name
                                 select new Contact
                                 {
                                    ChatId = dm.ChatId,
                                    UserAId = dm.UserAId,
                                    UserAName = ua.Username,
                                    UserBId = dm.UserBId,
                                    UserBName = dm.UserB.Username,
                                    LastMessage = _dbContext.Messages.OrderByDescending(dm => dm.DateCreated).FirstOrDefault(m => m.DirectMessageId == dm.ChatId)
                                 }).Where(c => c.UserAId == userId).Skip((int)((page - 1) * count)).Take((int)count).ToArrayAsync();
            }
        }
    }
}