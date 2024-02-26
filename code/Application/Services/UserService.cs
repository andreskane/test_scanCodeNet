using Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IConfiguration _configuration;

    public UserService(ILogger<UserService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

  


}
