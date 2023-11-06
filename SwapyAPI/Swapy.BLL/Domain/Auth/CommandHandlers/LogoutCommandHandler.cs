using MediatR;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly IUserTokenRepository _userTokenRepository;

        public LogoutCommandHandler(IUserTokenRepository userTokenRepository) => _userTokenRepository = userTokenRepository;

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userTokenRepository.DeleteByIdAsync(request.RefreshToken);
            }
            catch (Exception) { return Unit.Value; }
            return Unit.Value;
        }
    }
}
