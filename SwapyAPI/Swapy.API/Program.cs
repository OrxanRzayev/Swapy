using Swapy.DAL;
using Swapy.BLL.Services;
using Swapy.DAL.Interfaces;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using Swapy.BLL.Domain.Auth.CommandHandlers;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Domain.Chats.Commands;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.BLL.Domain.Products.CommandHandlers;
using Swapy.BLL.Domain.Users.CommandHandlers;
using Swapy.BLL.Domain.Users.QueryHandlers;
using Swapy.BLL.Domain.Chats.CommandHandlers;
using Swapy.BLL.Domain.Products.QueryHandlers;
using Swapy.BLL.Domain.Shops.QueryHandlers;
using Swapy.BLL.Domain.Chats.QueryHandlers;
using Swapy.BLL.Domain.Shops.CommandHandlers;
using Microsoft.OpenApi.Models;
using Swapy.API.Middlewares;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.DTO.Auth.Responses;
using System.Text.Json.Serialization;
using Swapy.Common.DTO.Shops.Responses;
using Swapy.Common.DTO.Users.Responses;
using Swapy.Common.DTO.Chats.Responses;
using Swapy.BLL.Domain.Categories.QueryHandlers;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO.Animals.Responses;
using Swapy.Common.DTO.Autos.Responses;
using Swapy.Common.DTO.Clothes.Responses;
using Swapy.Common.DTO.Electronics.Responses;
using Swapy.Common.DTO.Items.Responses;
using Swapy.Common.DTO.RealEstates.Responses;
using Swapy.Common.DTO.TVs.Responses;
using Swapy.BLL.Domain.Animals.Commands;
using Swapy.BLL.Domain.Autos.Commands;
using Swapy.BLL.Domain.Animals.CommandHandlers;
using Swapy.BLL.Domain.Electronics.Commands;
using Swapy.BLL.Domain.Clothes.Commands;
using Swapy.BLL.Domain.Items.CommandHandlers;
using Swapy.BLL.Domain.Items.Commands;
using Swapy.BLL.Domain.Autos.CommandHandlers;
using Swapy.BLL.Domain.Clothes.CommandHandlers;
using Swapy.BLL.Domain.Electronics.CommandHandlers;
using Swapy.BLL.Domain.RealEstates.CommandHandlers;
using Swapy.BLL.Domain.RealEstates.Commands;
using Swapy.BLL.Domain.TVs.Commands;
using Swapy.BLL.Domain.TVs.CommandHandlers;
using Swapy.BLL.Domain.Animals.Queries;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.BLL.Domain.Animals.QueryHandlers;
using Swapy.BLL.Domain.Autos.QueryHandlers;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.BLL.Domain.Clothes.QueryHandlers;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.BLL.Domain.Items.Queries;
using Swapy.BLL.Domain.Items.QueryHandlers;
using Swapy.BLL.Domain.RealEstates.Queries;
using Swapy.BLL.Domain.RealEstates.QueryHandlers;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.BLL.Domain.TVs.QueryHandlers;
using Swapy.BLL.Domain.Electronics.QueryHandlers;
using FluentValidation.AspNetCore;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.Enums;
using Microsoft.AspNetCore.Http.Features;
using Swapy.BLL.Hubs;

namespace Swapy.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Services Setup
            /// </summary>
            builder.Services.AddControllers()
                            .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            builder.Services.AddEndpointsApiExplorer();

            //Swagger Registration
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Swapy Api", Description = "'Swapy' REST Api", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });


            //Form Data Registration
            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });


            //SignalR Registration
            builder.Services.AddSignalR();


            /// <summary>
            /// Database Registration
            /// </summary>
            builder.Services.AddDbContext<SwapyDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("AzureSQL"));
            });


            /// <summary>
            /// JSON Registration
            /// </summary>
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });


            /// <summary>
            /// Repository Registration
            /// </summary>
            builder.Services.AddScoped<IAnimalAttributeRepository, AnimalAttributeRepository>();
            builder.Services.AddScoped<IAnimalBreedRepository, AnimalBreedRepository>();
            builder.Services.AddScoped<IAutoAttributeRepository, AutoAttributeRepository>();
            builder.Services.AddScoped<IAutoBrandRepository, AutoBrandRepository>();
            builder.Services.AddScoped<IAutoModelRepository, AutoModelRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IChatRepository, ChatRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<IClothesAttributeRepository, ClothesAttributeRepository>();
            builder.Services.AddScoped<IClothesBrandRepository, ClothesBrandRepository>();
            builder.Services.AddScoped<IClothesBrandViewRepository, ClothesBrandViewRepository>();
            builder.Services.AddScoped<IClothesSeasonRepository, ClothesSeasonRepository>();
            builder.Services.AddScoped<IClothesSizeRepository, ClothesSizeRepository>();
            builder.Services.AddScoped<IClothesViewRepository, ClothesViewRepository>();
            builder.Services.AddScoped<IColorRepository, ColorRepository>();
            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            builder.Services.AddScoped<IElectronicAttributeRepository, ElectronicAttributeRepository>();
            builder.Services.AddScoped<IElectronicBrandRepository, ElectronicBrandRepository>();
            builder.Services.AddScoped<IElectronicBrandTypeRepository, ElectronicBrandTypeRepository>();
            builder.Services.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();
            builder.Services.AddScoped<IFuelTypeRepository, FuelTypeRepository>();
            builder.Services.AddScoped<IGenderRepository, GenderRepository>();
            builder.Services.AddScoped<IItemAttributeRepository, ItemAttributeRepository>();
            builder.Services.AddScoped<ILikeRepository, LikeRepository>();
            builder.Services.AddScoped<IMemoryModelRepository, MemoryModelRepository>();
            builder.Services.AddScoped<IMemoryRepository, MemoryRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IModelColorRepository, ModelColorRepository>();
            builder.Services.AddScoped<IModelRepository, ModelRepository>();
            builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IRealEstateAttributeRepository, RealEstateAttributeRepository>();
            builder.Services.AddScoped<IScreenDiagonalRepository, ScreenDiagonalRepository>();
            builder.Services.AddScoped<IScreenResolutionRepository, ScreenResolutionRepository>();
            builder.Services.AddScoped<IShopAttributeRepository, ShopAttributeRepository>();
            builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<ITransmissionTypeRepository, TransmissionTypeRepository>();
            builder.Services.AddScoped<ITVAttributeRepository, TVAttributeRepository>();
            builder.Services.AddScoped<ITVBrandRepository, TVBrandRepository>();
            builder.Services.AddScoped<ITVTypeRepository, TVTypeRepository>();
            builder.Services.AddScoped<IUserLikeRepository, UserLikeRepository>();
            builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
            builder.Services.AddScoped<IUserTokenRepository, UserTokenRepository>();


            /// <summary>
            /// Service Registration
            /// </summary>
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IUserTokenService, UserTokenService>();
            builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<ICurrencyConverterService, CurrencyConverterService>();
            builder.Services.AddScoped<IKeyVaultService, KeyVaultService>(provider => new KeyVaultService(builder.Configuration.GetValue<string>("KeyVaultUrl")));


            /// <summary>
            /// CQRS registration
            /// </summary>
            builder.Services.AddTransient<IRequestHandler<AddAnimalAttributeCommand, AnimalAttribute>, AddAnimalAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddAutoAttributeCommand, AutoAttribute>, AddAutoAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddClothesAttributeCommand, ClothesAttribute>, AddClothesAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddElectronicAttributeCommand, ElectronicAttribute>, AddElectronicAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddFavoriteProductCommand, FavoriteProduct>, AddFavoriteProductCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddItemAttributeCommand, ItemAttribute>, AddItemAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddLikeCommand, Like>, AddLikeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddRealEstateAttributeCommand, RealEstateAttribute>, AddRealEstateAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddSubscriptionCommand, Subscription>, AddSubscriptionCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddTVAttributeCommand, TVAttribute>, AddTVAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ChangePasswordCommand, Unit>, ChangePasswordCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ConfirmEmailCommand, Unit>, ConfirmEmailCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<EmailCommand, bool>, CheckEmailCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ForgotPasswordCommand, Unit>, ForgotPasswordCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<CheckLikeQuery, bool>, CheckLikeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<PhoneNumberCommand, bool>, CheckPhoneNumberCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ShopNameCommand, bool>, CheckShopNameCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<CheckSubscriptionQuery, bool>, CheckSubscriptionQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<CreateChatCommand, Chat>, CreateChatCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAnimalAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>, GetAllAnimalAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAnimalBreedsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllAnimalBreedsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAnimalTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllAnimalTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAutoAttributesQuery, AutoAttributesResponseDTO>, GetAllAutoAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAutoBrandsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllAutoBrandsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAutoModelsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllAutoModelsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAutoTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllAutoTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllBuyerChatsQuery, ChatsResponseDTO>, GetAllBuyerChatsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryTreeResponseDTO>>, GetAllCategoriesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllCitiesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllCitiesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>, GetAllClothesAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesBrandsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllClothesBrandsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesSeasonsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllClothesSeasonsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesSizesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllClothesSizesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllClothesTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesViewsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllClothesViewsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllColorsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllColorsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllColorsByModelQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllColorsQueryByModelHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllCurrenciesQuery, IEnumerable<CurrencyResponseDTO>>, GetAllCurrenciesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllElectronicAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>, GetAllElectronicAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllElectronicBrandsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllElectronicBrandsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllElectronicTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllElectronicTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllFavoriteProductsQuery, ProductsResponseDTO<ProductResponseDTO>>, GetAllFavoriteProductsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllFuelTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllFuelTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllGendersQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllGendersQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllItemAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>, GetAllItemAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllItemTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllItemTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllMemoriesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllMemoriesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllModelsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllModelsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllProductsQuery, ProductsResponseDTO<ProductResponseDTO>>, GetAllProductsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllRealEstateAttributesQuery, RealEstateAttributesResponseDTO>, GetAllRealEstateAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllRealEstateTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllRealEstateTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllScreenDiagonalsQuery, IEnumerable<SpecificationResponseDTO<int>>>, GetAllScreenDiagonalsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllScreenResolutionsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllScreenResolutionsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllSellerChatsQuery, ChatsResponseDTO>, GetAllSellerChatsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllShopsQuery, ShopsResponseDTO>, GetAllShopsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllSubcategoriesByCategoryQuery, IEnumerable<CategoryTreeResponseDTO>>, GetAllSubcategoriesByCategoryQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllSubcategoriesBySubcategoryQuery, IEnumerable<CategoryTreeResponseDTO>>, GetAllSubcategoriesBySubcategoryQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllTransmissionTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllTransmissionTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllTVAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>, GetAllTVAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllTVBrandsQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllTVBrandsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllTVTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetAllTVTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdAnimalAttributeQuery, AnimalAttributeResponseDTO>, GetByIdAnimalAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdAutoAttributeQuery, AutoAttributeResponseDTO>, GetByIdAutoAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdClothesAttributeQuery, ClothesAttributeResponseDTO>, GetByIdClothesAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdElectronicAttributeQuery, ElectronicAttributeResponseDTO>, GetByIdElectronicAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdItemAttributeQuery, ItemAttributeResponseDTO>, GetByIdItemAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdRealEstateAttributeQuery, RealEstateAttributeResponseDTO>, GetByIdRealEstateAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdShopQuery, ShopDetailResponseDTO>, GetByIdShopQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdTVAttributeQuery, TVAttributeResponseDTO>, GetByIdTVAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdUserQuery, UserResponseDTO>, GetByIdUserQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetClothesBrandViewIdQuery, string>, GetClothesBrandViewIdQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetDetailChatQuery, DetailChatResponseDTO>, GetDetailChatQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetDetailChatByProductIdQuery, DetailChatResponseDTO>, GetDetailChatByProductIdQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetModelColorIdQuery, string>, GetModelColorIdQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetMemoryModelIdQuery, string>, GetMemoryModelIdQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetShopDataQuery, ShopDataResponseDTO>, GetShopDataQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetSiblingsQuery, IEnumerable<CategoryTreeResponseDTO>>, GetSiblingsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetSimilarProductsByProductIdQuery, ProductsResponseDTO<ProductResponseDTO>>, GetSimilarProductsByProductIdQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetSubcategoryPathQuery, IEnumerable<SpecificationResponseDTO<string>>>, GetSubcategoryPathQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetUserDataQuery, UserDataResponseDTO>, GetUserDataQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetUserSubscriptionsQuery, IEnumerable<Subscription>>, GetUserSubscriptionsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetProductCategoryTypeQuery, SpecificationResponseDTO<CategoryType>>, GetProductCategoryTypeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetProductSubcategoryQuery, ProductSubcategoryResponseDTO>, GetProductSubcategoryQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetTemporaryChatQuery, DetailChatResponseDTO>, GetTemporaryChatQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<IncrementProductViewsCommand, Unit>, IncrementProductViewsCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<LoginCommand, AuthResponseDTO>, LoginCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<LogoutCommand, Unit>, LogoutCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<PreviewUploadImageCommand, ImageResponseDTO>, PreviewUploadImageCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ReadChatCommand, bool>, ReadChatCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveFavoriteProductCommand, Unit>, RemoveFavoriteProductCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveLikeCommand, Unit>, RemoveLikeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveProductCommand, Unit>, RemoveProductCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveSubscriptionCommand, Unit>, RemoveSubscriptionCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveUserCommand, Unit>, RemoveUserCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ResetPasswordCommand, Unit>, ResetPasswordCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<SendMessageCommand, Message>, SendMessageCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ShopRegistrationCommand, Unit>, ShopRegistrationCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<SwitchProductEnablingCommand, Unit>, SwitchProductEnablingCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<SendMessageToRemoveCommand, Unit>, SendMessageToRemoveCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ToggleSubscriptionStatusCommand, Unit>, ToggleSubscriptionStatusCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateAnimalAttributeCommand, Unit>, UpdateAnimalAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateAutoAttributeCommand, Unit>, UpdateAutoAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateClothesAttributeCommand, Unit>, UpdateClothesAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateElectronicAttributeCommand, Unit>, UpdateElectronicAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateItemAttributeCommand, Unit>, UpdateItemAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateRealEstateAttributeCommand, Unit>, UpdateRealEstateAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateShopCommand, Unit>, UpdateShopCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateTVAttributeCommand, Unit>, UpdateTVAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateUserCommand, Unit>, UpdateUserCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateMessagesStatusCommand, Unit>, UpdateMessagesStatusCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateUserTokenCommand, AuthResponseDTO>, UpdateUserTokenCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UploadBannerCommand, Unit>, UploadBannerCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UploadImageCommand, Unit>, UploadImageCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UploadLogoCommand, Unit>, UploadLogoCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UserRegistrationCommand, Unit>, UserRegistrationCommandHandler>();


            /// <summary>
            /// Claims Principal Registration
            /// </summary>
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped(provider => 
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                return httpContextAccessor.HttpContext?.User;
            });


            /// <summary>
            /// CORS(Cross - Origin Resource Sharing)
            /// </summary>
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("Default", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });


            /// <summary>
            /// Register Identity Service
            /// </summary>
            builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SwapyDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<DataProtectorTokenProvider<User>>("UserTokenProvider");

            //Token life time
            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(1);
            });


            /// <summary>
            /// Configurations for JWToken
            /// </summary>
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Basic";
                options.DefaultChallengeScheme = "Basic";
            }).AddScheme<BasicAuthenticationOptions, SwapyAuthenticationMiddleware>("Basic", null);


            /// <summary>
            /// Configurations for MediatR
            /// </summary>
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


            /// <summary>
            /// Application Setup
            /// </summary>
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("Default");

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}