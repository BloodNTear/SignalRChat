using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class MessageRepository : IMessageRepository
    {
        private SignalRDBContext _context;
        private DbSet<Message> _dbSet;
        public MessageRepository(SignalRDBContext context) {
            _context = context;
            _dbSet = _context.messages;
        }
        public bool SendMessage(string message, Account from, ChatGroup to)
        {
            Message newMess = new Message();
            newMess.MessageID = Guid.NewGuid();
            newMess.MessageContent = message;
            newMess.MessageTime = DateTime.Now;
            newMess.FromAccount = from;
            newMess.ToChatGroup = to;

            _dbSet.Add(newMess);

            return _context.SaveChanges() > 0;
        }
    }
}
