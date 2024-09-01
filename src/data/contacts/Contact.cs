
using repo;

namespace data.contacts
{
    public class Contact
    {
        public required Guid ChatId { get; set; }

        public required Guid UserAId { get; set; }
        public string? UserAName {get; set;}

        public required Guid UserBId { get; set; }
        public string? UserBName {get; set;}
        public Message? LastMessage {get; set;}
    }
}