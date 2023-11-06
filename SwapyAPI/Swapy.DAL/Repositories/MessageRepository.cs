using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SwapyDbContext _context;

        public MessageRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Message item)
        {
            await _context.Messages.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Message item)
        {
            _context.Messages.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Message item)
        {
            _context.Messages.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Message> GetByIdAsync(string id)
        {
            var item = await _context.Messages.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }
    }
}
