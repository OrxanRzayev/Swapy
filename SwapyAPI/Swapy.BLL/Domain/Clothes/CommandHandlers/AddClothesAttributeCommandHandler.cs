using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Clothes.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;
using System.Runtime.InteropServices;

namespace Swapy.BLL.Domain.Clothes.CommandHandlers
{
    public class AddClothesAttributeCommandHandler : IRequestHandler<AddClothesAttributeCommand, ClothesAttribute>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly INotificationService _notificationService;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IClothesAttributeRepository _clothesAttributeRepository;
        private readonly UserManager<User> _userManager;

        public AddClothesAttributeCommandHandler(UserManager<User> userManager, IImageService imageService, IProductRepository productRepository, INotificationService notificationService, ISubcategoryRepository subcategoryRepository, IClothesAttributeRepository clothesAttributeRepository)
        {
            _userManager = userManager;
            _imageService = imageService;
            _productRepository = productRepository;
            _notificationService = notificationService;
            _subcategoryRepository = subcategoryRepository;
            _clothesAttributeRepository = clothesAttributeRepository;
        }

        public async Task<ClothesAttribute> Handle(AddClothesAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory");

            Product product = new Product(request.Title, request.Description, request.Price, request.UserId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            ClothesAttribute clothesAttribute = new ClothesAttribute(request.IsNew, request.ClothesSeasonId, request.ClothesSizeId, request.ClothesBrandViewId, product.Id);
            product.ClothesAttributeId = clothesAttribute.Id;
            await _clothesAttributeRepository.CreateAsync(clothesAttribute);

            if (request.Files.Count > 0) await _imageService.UploadProductImagesAsync(request.Files, product.Id);

            var model = new NotificationModel()
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                CityId = request.CityId,
                Price = request.Price,
                CurrencyId = request.CurrencyId,
                ProductId = product.Id
            };

            await _notificationService.Notificate(model);

            var user = await _userManager.FindByIdAsync(request.UserId);
            user.ProductsCount++;
            await _userManager.UpdateAsync(user);

            return clothesAttribute;
        }
    }
}
