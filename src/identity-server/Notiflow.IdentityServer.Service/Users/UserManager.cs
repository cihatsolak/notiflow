using Mapster;
using Notiflow.IdentityServer.Service.Models;

namespace Notiflow.IdentityServer.Service.Users
{
    public sealed class UserManager : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserManager> _logger;

        public UserManager(ApplicationDbContext context, ILogger<UserManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseData<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking().SingleAsync(p => p.Id == id, cancellationToken);
            if (user is null)
            {
                _logger.LogInformation("");
                return ResponseData<UserResponse>.Fail(-1);
            }

            return ResponseData<UserResponse>.Success(user.Adapt<UserResponse>());
        }

        public async Task<ResponseData<int>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            bool isExists = _context.Users.Any(p => p.Username.Equals(request.Username) || p.Email.Equals(request.Email));
            if (isExists)
            {
                _logger.LogInformation("");
                return ResponseData<int>.Fail(-1);
            }

            var user = request.Adapt<User>();
            user.TenantId = 1;

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ResponseData<int>.Success(1);
        }

        public async Task<ResponseData<int>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
            if (user is null)
            {
                _logger.LogInformation("");
                return ResponseData<int>.Fail(-1);
            }

            request.Adapt(user);
            await _context.SaveChangesAsync(cancellationToken);

            return ResponseData<int>.Success(1);
        }

        public async Task<ResponseData<int>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            int numberOfRowsDeleted = await _context.Users.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);
            if (numberOfRowsDeleted != 1)
            {
                _logger.LogInformation("");
                return ResponseData<int>.Fail(-1);
            }

            return ResponseData<int>.Success(1);
        }
    }
}
