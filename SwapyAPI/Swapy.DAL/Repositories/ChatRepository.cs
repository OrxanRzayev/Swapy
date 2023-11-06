using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly SwapyDbContext _context;

        public ChatRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Chat item)
        {
            await _context.Chats.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chat item)
        {
            _context.Chats.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Chat item)
        {
            _context.Chats.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Chat> GetByIdAsync(string id)
        {
            var item = await _context.Chats.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Chat>> GetAllAsync()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetAllSellerChatsAsync(string userId)
        { 
            return await _context.Products.Where(p => p.UserId.Equals(userId))
                                          .SelectMany(p => p.Chats)
                                          .Include(c => c.Product)
                                            .ThenInclude(p => p.Images)
                                          .Include(c => c.Buyer)
                                            .ThenInclude(b => b.ShopAttribute)
                                          .Select(c => new Chat
                                          {
                                              Id = c.Id,
                                              Buyer = c.Buyer,
                                              Product = c.Product,
                                              IsReaded = c.IsReaded,
                                              Messages = c.Messages.OrderByDescending(m => m.DateTime).Take(1).ToList(),
                                          })
                                          .ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetAllBuyerChatsAsync(string userId)
        {
            var result = await _context.Chats.Where(c => c.BuyerId.Equals(userId))
                                            .Include(c => c.Product)
                                                .ThenInclude(p => p.User)
                                            .Include(c => c.Product)
                                                .ThenInclude(p => p.Images)
                                            .Select(c => new Chat
                                            {
                                                Id = c.Id,
                                                Buyer = c.Buyer,
                                                Product = c.Product,
                                                IsReaded = c.IsReaded,
                                                Messages = c.Messages.OrderByDescending(m => m.DateTime).Take(1).ToList(),
                                            })
                                            .ToListAsync();

            return result;
        }

        public async Task<Chat> GetByIdDetailAsync(string id)
        {
            var item = await _context.Chats.Where(c => c.Id.Equals(id))
                                           .Include(c => c.Product)
                                                .ThenInclude(p => p.User)
                                           .Include(c => c.Buyer)
                                            .ThenInclude(b => b.ShopAttribute)
                                           .Include(c => c.Product)
                                                .ThenInclude(p => p.Images)
                                           .Include(c => c.Messages)
                                                .ThenInclude(m => m.Sender)
                                           .FirstOrDefaultAsync();


            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            item.Messages = item?.Messages?.OrderBy(m => m.DateTime).ToList();
            return item;
        }

        public async Task<Chat> GetByIdDetailByProductIdAsync(string productId, string userId)
        {
            var item = await _context.Chats.Include(c => c.Product)
                                                .ThenInclude(p => p.User)
                                           .Include(c => c.Buyer)
                                           .Include(c => c.Product)
                                                .ThenInclude(p => p.Images)
                                           .Include(c => c.Messages)
                                                .ThenInclude(m => m.Sender)
                                           .Where(c => c.BuyerId.Equals(userId))
                                           .FirstOrDefaultAsync(c => c.ProductId.Equals(productId));

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with product with {productId} id not found");
            item.Messages = item?.Messages?.OrderBy(m => m.DateTime).ToList();
            return item;
        }

        public async Task<Chat> CheckChatExists(string userId, string productId)
        {
            return await _context.Chats.Where(c => c.ProductId.Equals(productId) && c.BuyerId.Equals(userId)).FirstOrDefaultAsync();
        }

        public async Task<User> GetChatRecepientIdAsync(string chatId, string senderId)
        {
            var chat = await _context.Chats.Where(c => c.Id.Equals(chatId))
                                           .Include(c => c.Buyer)
                                           .Include(c => c.Product)
                                            .ThenInclude(p => p.User)
                                                .ThenInclude(u => u.ShopAttribute)
                                           .FirstOrDefaultAsync();

            return chat.BuyerId == senderId ? chat.Product.User : chat.Buyer;
        }

        public async Task<bool> TryReadMessage(string userId, string chatId)
        {
            var chat = await _context.Chats.Where(c => c.Id.Equals(chatId))
                                           .Include(c => c.Product)
                                           .Select(c => new Chat
                                           {
                                               Id = c.Id,
                                               BuyerId = c.BuyerId,
                                               Product = c.Product,
                                               Messages = c.Messages.OrderByDescending(m => m.DateTime).Take(1).ToList(),
                                           })
                                           .FirstOrDefaultAsync();

            if(chat == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {chatId} id not found");

            if(!chat.BuyerId.Equals(userId) && !chat.Product.UserId.Equals(userId)) throw new NoAccessException($"Invalid UserId");

            return chat.Messages.Count > 0 ? !chat.Messages.FirstOrDefault().SenderId.Equals(userId) : false;
        }

        public async Task UpdateChatState(string chatId, bool value)
        {
            var chat = await GetByIdAsync(chatId);
            chat.IsReaded = value;
            await UpdateAsync(chat);
        }

        public async Task<IEnumerable<Chat>> GetAllChatsAsync(string userId)
        {
            var chats = await _context.Products.Where(p => p.UserId.Equals(userId))
                                          .SelectMany(p => p.Chats)
                                          .Select(c => new Chat
                                          {
                                              Id = c.Id,
                                              IsReaded = c.IsReaded,
                                          })
                                          .ToListAsync();

            chats.AddRange(await _context.Chats.Where(c => c.BuyerId.Equals(userId))
                                            .Select(c => new Chat
                                            {
                                                Id = c.Id,
                                                IsReaded = c.IsReaded,
                                            })
                                            .ToListAsync());

            return chats;
        }

        public async Task DeleteChatsByProductId(string productId)
        {
            var chats = await _context.Chats.Where(c => c.ProductId.Equals(productId)).ToListAsync();

            foreach (var chat in chats)
            {
                await DeleteAsync(chat);
            }
        }
    }
}