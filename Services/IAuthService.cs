using CookbookClient.DTO;
using CookbookClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        string GenerateJwtToken(User user);
    }
}
