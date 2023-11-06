﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Autos.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.CommandHandlers
{
    public class AddAutoAttributeCommandHandler : IRequestHandler<AddAutoAttributeCommand, AutoAttribute>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly INotificationService _notificationService;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IAutoAttributeRepository _autoAttributeRepository;
        private readonly UserManager<User> _userManager;

        public AddAutoAttributeCommandHandler(UserManager<User> userManager, IImageService imageService, IProductRepository productRepository, INotificationService notificationService, ISubcategoryRepository subcategoryRepository, IAutoAttributeRepository autoAttributeRepository)
        {
            _userManager = userManager;
            _imageService = imageService;
            _productRepository = productRepository;
            _notificationService = notificationService;
            _subcategoryRepository = subcategoryRepository;
            _autoAttributeRepository = autoAttributeRepository;
        }

        public async Task<AutoAttribute> Handle(AddAutoAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory");

            Product product = new Product(request.Title, request.Description, request.Price, request.UserId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            AutoAttribute autoAttribute = new AutoAttribute(request.Miliage, request.EngineCapacity, request.ReleaseYear, request.IsNew, request.FuelTypeId, request.AutoColorId, request.TransmissionTypeId, request.AutoModelId, product.Id);
            product.AutoAttributeId = autoAttribute.Id;
            await _autoAttributeRepository.CreateAsync(autoAttribute);

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

            return autoAttribute;
        }
    }
}
