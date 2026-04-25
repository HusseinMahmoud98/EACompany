using Company.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Services.Interfaces.Auth
{
    public interface IAuthManager
    {
        public Task<UserDto> LoginAsync(LoginDto loginDto);
        public Task<UserDto> RegisterAsync(RegisterDto registerDto);

    }
}
