using MediatR;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.DTO.Chats.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Chats.QueryHandlers
{
    public class GetTemporaryChatQueryHandler : IRequestHandler<GetTemporaryChatQuery, DetailChatResponseDTO>
    {
        private readonly IProductRepository _productRepository;

        public GetTemporaryChatQueryHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<DetailChatResponseDTO> Handle(GetTemporaryChatQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetDetailByIdAsync(request.ProductId);

            if(request.UserId == product.UserId) throw new ArgumentException("You cannot create a chat for your product");


            return new DetailChatResponseDTO()
            {
                ChatId = null,
                Messages = null,
                Title = product.Title,
                Image = product.Images.FirstOrDefault()?.Image == null ? "default-product-image.png" : product.Images.FirstOrDefault()?.Image
            };
        }
    }
}
