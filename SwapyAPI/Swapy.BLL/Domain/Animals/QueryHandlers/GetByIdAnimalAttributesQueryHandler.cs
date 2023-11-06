using MediatR;
using Swapy.BLL.Domain.Animals.Queries;
using Swapy.Common.DTO.Animals.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Animals.QueryHandlers
{
    public class GetByIdAnimalAttributesQueryHandler : IRequestHandler<GetByIdAnimalAttributeQuery, AnimalAttributeResponseDTO>
    {
        private readonly IAnimalAttributeRepository _animalAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdAnimalAttributesQueryHandler(IAnimalAttributeRepository animalAttributeRepository, ISubcategoryRepository subcategoryRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _animalAttributeRepository = animalAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<AnimalAttributeResponseDTO> Handle(GetByIdAnimalAttributeQuery request, CancellationToken cancellationToken)
        {
            var animalAttribute = await _animalAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(animalAttribute.Product.SubcategoryId)).ToList();
            categories.Insert(0, new SpecificationResponseDTO<string>(animalAttribute.Product.CategoryId, animalAttribute.Product.Category.Name));

            AnimalAttributeResponseDTO result = new AnimalAttributeResponseDTO()
            {
                Id = animalAttribute.Id,
                City = animalAttribute.Product.City.Name,
                CityId = animalAttribute.Product.CityId,
                Currency = animalAttribute.Product.Currency.Name,
                CurrencyId = animalAttribute.Product.CurrencyId,
                CurrencySymbol = animalAttribute.Product.Currency.Symbol,
                UserId = animalAttribute.Product.UserId,
                FirstName = animalAttribute.Product.User.FirstName,
                LastName = animalAttribute.Product.User.LastName,
                PhoneNumber = animalAttribute.Product.User.PhoneNumber,
                ShopId = animalAttribute.Product.User.ShopAttributeId ?? string.Empty,
                Shop = animalAttribute.Product.User.ShopAttribute?.ShopName ?? string.Empty,
                UserType = animalAttribute.Product.User.Type,
                ProductId = animalAttribute.ProductId,
                Title = animalAttribute.Product.Title,
                Views = animalAttribute.Product.Views,
                Price = animalAttribute.Product.Price,
                Description = animalAttribute.Product?.Description,
                DateTime = animalAttribute.Product.DateTime,
                IsFavorite = request.UserId == null ? false : await _favoriteProductRepository.CheckProductOnFavorite(animalAttribute.ProductId, request.UserId),
                Categories = categories,
                IsDisable = animalAttribute.Product.IsDisable,
                Images = animalAttribute.Product.Images.Select(i => i.Image).ToList(),
                BreedId = animalAttribute.AnimalBreedId,
                Breed = animalAttribute.AnimalBreed.Name
            };
            return result;
        }
    }
}
