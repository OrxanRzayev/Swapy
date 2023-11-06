using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.API.Validators;
using Swapy.BLL.Domain.TVs.Commands;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.DTO.TVs.Requests.Commands;
using Swapy.Common.DTO.TVs.Requests.Queries;
using Swapy.Common.Exceptions;
using System.Security.Claims;
using System.Text;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/Products/[controller]")]
    [Produces("application/json")]
    public class TVsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TVsController(IMediator mediator) => _mediator = mediator;

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
        /// TVs
        /// </summary>
        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTVAsync([FromForm] AddTVAttributeCommandDTO dto)
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


                var tvValidator = new AddTVAttributeValidator();
                var tvValidatorResult = tvValidator.Validate(dto);

                if (!tvValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in tvValidatorResult.Errors)
                    {
                        builder.Append($"Animal Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
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
                var command = new AddTVAttributeCommand()
                {
                    UserId = userId,
                    IsNew = dto.IsNew,
                    Price = dto.Price,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    Files = formFiles,
                    IsSmart = dto.IsSmart,
                    TVTypeId = dto.TvTypeId,
                    TVBrandId = dto.TvBrandId,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    Description = dto.Description,
                    SubcategoryId = dto.SubcategoryId,
                    ScreenDiagonalId = dto.ScreenDiagonalId,
                    ScreenResolutionId = dto.ScreenResolutionId,
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/tvs/{result.Id}");
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
        public async Task<IActionResult> UpdateTVAsync([FromForm] UpdateTVAttributeCommandDTO dto)
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


                var tvValidator = new UpdateTVAttributeValidator();
                var tvValidatorResult = tvValidator.Validate(dto);

                if (!tvValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in tvValidatorResult.Errors)
                    {
                        builder.Append($"TV Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
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
                var command = new UpdateTVAttributeCommand()
                {
                    UserId = userId,
                    IsNew = dto.IsNew,
                    Price = dto.Price,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    IsSmart = dto.IsSmart,
                    TVTypeId = dto.TvTypeId,
                    ProductId = dto.ProductId,
                    TVBrandId = dto.TvBrandId,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    Description = dto.Description,
                    SubcategoryId = dto.SubcategoryId,
                    ScreenDiagonalId = dto.ScreenDiagonalId,
                    ScreenResolutionId = dto.ScreenResolutionId,
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
        public async Task<IActionResult> GetAllTVsAsync([FromQuery] GetAllTVAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllTVAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    IsNew = dto.IsNew,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    IsSmart = dto.IsSmart,
                    PageSize = dto.PageSize,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    TVTypesId = dto.TvTypesId,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    TVBrandsId = dto.TvBrandsId,
                    OtherUserId = dto.OtherUserId,
                    ReverseSort = dto.ReverseSort,
                    SortByPrice = dto.SortByPrice,
                    SubcategoryId = dto.SubcategoryId,
                    ScreenDiagonalsId = dto.ScreenDiagonalsId,
                    ScreenResolutionsId = dto.ScreenResolutionsId,
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
        public async Task<IActionResult> GetByIdTVAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdTVAttributeQuery() {
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
        /// TVs Attributes
        /// </summary>
        [HttpGet("Brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTVBrandsAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllTVBrandsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("ScreenResolutions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllScreenResolutionsAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllScreenResolutionsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("ScreenDiagonals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllScreenDiagonalsAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllScreenDiagonalsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTVTypesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllTVTypesQuery());
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
            return Ok("x7 GET, POST, PUT, DELETE, HEAD, OPTIONS");
        }
    }
}
