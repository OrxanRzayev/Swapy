using MediatR;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.BLL.Interfaces;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class UploadLogoCommandHandler : IRequestHandler<UploadLogoCommand, Unit>
    {
        private readonly IImageService _imageService;

        public UploadLogoCommandHandler(IImageService imageService) => _imageService = imageService;

        public async Task<Unit> Handle(UploadLogoCommand request, CancellationToken cancellationToken)
        {
            await _imageService.UploadLogoAsync(request.Logo, request.UserId);
            return Unit.Value;
        }
    }
}
