using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.API.Validators;
using Swapy.BLL.Domain.RealEstates.Commands;
using Swapy.BLL.Domain.RealEstates.Queries;
using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.DTO.RealEstates.Requests.Commands;
using Swapy.Common.DTO.RealEstates.Requests.Queries;
using Swapy.Common.Exceptions;
using System.Security.Claims;
using System.Text;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/Products/[controller]")]
    [Produces("application/json")]
    public class RealEstatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RealEstatesController(IMediator mediator) => _mediator = mediator;

        [HttpGet("Ping")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PingAsync()
        {
            try
            {
                return Ok("Ping");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Real Estates
        /// </summary>
        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRealEstatesAsync([FromForm] AddRealEstateAttributeCommandDTO dto)
        {
            try
            {
                IFormFileCollection formFiles = HttpContext.Request.Form.Files;
                var productValidator = new AddProductValidator();
                var productValidatorResult = productValidator.Validate(dto);

                if (!productValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in productValidatorResult.Errors)
                    {
                        builder.Append($"Product Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var realEstateValidator = new AddRealEstateAttributeValidator();
                var realEstateValidatorResult = realEstateValidator.Validate(dto);

                if (!realEstateValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in realEstateValidatorResult.Errors)
                    {
                        builder.Append($"Real Estate Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var imageValidator = new AddImageUploadValidator();
                var imageValidatorResult = imageValidator.Validate(formFiles);
                if (!imageValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in imageValidatorResult.Errors)
                    {
                        builder.Append($"Product image upload property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddRealEstateAttributeCommand()
                {
                    UserId = userId,
                    Area = dto.Area,
                    Price = dto.Price,
                    Rooms = dto.Rooms,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    Files = formFiles,
                    IsRent = dto.IsRent,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    Description = dto.Description,
                    SubcategoryId = dto.SubcategoryId,
                    RealEstateTypeId = dto.RealEstateTypeId,
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/real-estates/{result.Id}");
                return Created(locationUri, result.ProductId);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRealEstateAsync([FromForm] UpdateRealEstateAttributeCommandDTO dto)
        {
            try
            {
                IFormFileCollection formFiles = HttpContext.Request.Form.Files;
                var productValidator = new UpdateProductValidator();
                var productValidatorResult = productValidator.Validate(dto);

                if (!productValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in productValidatorResult.Errors)
                    {
                        builder.Append($"Product Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var realEstateValidator = new UpdateRealEstateAttributeValidator();
                var realEstateValidatorResult = realEstateValidator.Validate(dto);

                if (!realEstateValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in realEstateValidatorResult.Errors)
                    {
                        builder.Append($"Real Estate Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var imageValidator = new UpdateImageUploadValidator();
                var imageValidatorResult = imageValidator.Validate(formFiles);
                if (!imageValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in imageValidatorResult.Errors)
                    {
                        builder.Append($"Product image upload property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateRealEstateAttributeCommand()
                {
                    UserId = userId,
                    Area = dto.Area,
                    Price = dto.Price,
                    Title = dto.Title,
                    Rooms = dto.Rooms,
                    CityId = dto.CityId,
                    IsRent = dto.IsRent,
                    ProductId = dto.ProductId,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    Description = dto.Description,
                    SubcategoryId = dto.SubcategoryId,
                    RealEstateTypeId = dto.RealEstateTypeId,
                    OldPaths = dto.OldPaths,
                    NewFiles = formFiles
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NoAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRealEstatesAsync([FromQuery] GetAllRealEstateAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllRealEstateAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    IsRent = dto.IsRent,
                    AreaMax = dto.AreaMax,
                    AreaMin = dto.AreaMin,
                    PageSize = dto.PageSize,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    RoomsMin = dto.RoomsMin,
                    RoomsMax = dto.RoomsMax,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    OtherUserId = dto.OtherUserId,
                    ReverseSort = dto.ReverseSort,
                    SortByPrice = dto.SortByPrice,
                    SubcategoryId = dto.SubcategoryId,
                    RealEstateTypesId = dto.RealEstateTypesId,
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("{ProductId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdRealEstateAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdRealEstateAttributeQuery() {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    ProductId = dto.ProductId,
                });
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Real Estates
        /// </summary>
        [HttpGet("Types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRealEstateTypesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllRealEstateTypesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> HeadAsync()
        {
            return Ok();
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> OptionsAsync()
        {
            return Ok("x4 GET, POST, PUT, DELETE, HEAD, OPTIONS");
        }
    }
}
