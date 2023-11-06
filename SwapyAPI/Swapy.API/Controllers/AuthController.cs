using Castle.Core.Internal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.API.Validators;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.Common.Attributes;
using Swapy.Common.DTO.Auth.Requests;
using Swapy.Common.Exceptions;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) => _mediator = mediator;

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

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync(LoginCommandDTO dto)
        {
            try
            {
                var validator = new LoginValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"Login property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var command = new LoginCommand()
                {
                    EmailOrPhone = dto.EmailOrPhone,
                    Password = dto.Password
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthenticationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPost("Register/User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UserRegistrationAsync(UserRegistrationCommandDTO dto)
        {
            try
            {
                var validator = new UserRegisterValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"User property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var command = new UserRegistrationCommand()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Password = dto.Password
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (EmailOrPhoneTakenException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Invalid operation: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPost("Register/Shop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ShopRegistrationAsync(ShopRegistrationCommandDTO dto)
        {
            try
            {
                var validator = new ShopRegisterValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"Shop property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var command = new ShopRegistrationCommand()
                {
                    ShopName = dto.ShopName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Password = dto.Password
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (EmailOrPhoneTakenException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, "Invalid operation: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut("RefreshAccessToken")]
        [AuthorizeIgnore]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTokenAsync()
        {
            try
            {
                var accessToken = User.FindFirstValue(ClaimTypes.Hash);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId.IsNullOrEmpty()) throw new UnauthorizedAccessException("Unauthorized");
                var command = new UpdateUserTokenCommand()
                {
                    UserId = userId,
                    OldAccessToken = accessToken,
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (TokenExpiredException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                var refreshToken = User.FindFirstValue(ClaimTypes.Authentication);
                var command = new LogoutCommand()
                {
                    RefreshToken = refreshToken  
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex is System.ComponentModel.DataAnnotations.ValidationException)
                {
                    return BadRequest(ex.Message);
                }

                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckAsync([FromQuery] CheckCommandDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.Email) && string.IsNullOrEmpty(dto.PhoneNumber) && string.IsNullOrEmpty(dto.ShopName))
                {
                    var result = await _mediator.Send(new EmailCommand() { Email = dto.Email });
                    return Ok(result);
                }
                else if (string.IsNullOrEmpty(dto.Email) && !string.IsNullOrEmpty(dto.PhoneNumber) && string.IsNullOrEmpty(dto.ShopName))
                {
                    var result = await _mediator.Send(new PhoneNumberCommand() { PhoneNumber = dto.PhoneNumber });
                    return Ok(result);
                }
                else if (string.IsNullOrEmpty(dto.Email) && string.IsNullOrEmpty(dto.PhoneNumber) && !string.IsNullOrEmpty(dto.ShopName))
                {
                    var result = await _mediator.Send(new ShopNameCommand() { ShopName = dto.ShopName });
                    return Ok(result);
                }
                else throw new Exception("You can't send more than 1 parameter");
            }
            catch (Exception ex)
            {
                if (ex is System.ComponentModel.DataAnnotations.ValidationException)
                {
                    return BadRequest(ex.Message);
                }

                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPatch("ChangePassword")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommandDTO dto)
        {
            try
            {
                var validator = new ChangePasswordValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"Password property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new ChangePasswordCommand()
                {
                    UserId = userId,
                    NewPassword = dto.NewPassword,
                    OldPassword = dto.OldPassword
                };

                var result = await _mediator.Send(command);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPatch("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailDTO dto)
        {
            try
            {
                var validator = new ConfirmEmailValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"Confirm email property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var command = new ConfirmEmailCommand()
                {
                    UserId = dto.UserId,
                    Token = dto.Token,
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (TokenExpiredException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPost("ForgotPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommandDTO dto)
        {
            try
            {
                var validator = new ForgotPasswordValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"Forgot Password property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var result = await _mediator.Send(new ForgotPasswordCommand { Email = dto.Email });
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPatch("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommandDTO dto)
        {
            try
            {
                var validator = new ResetPasswordValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"Reset Password property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var command = new ResetPasswordCommand
                {
                    UserId = dto.UserId,
                    Password = dto.Password,
                    Token = dto.Token
                };

                var result = await _mediator.Send(command);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TokenExpiredException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
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
            return Ok("x3 GET, x4 POST, x3 PATCH, HEAD, OPTIONS");
        }
    }
}
