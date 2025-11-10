using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Auth;

namespace BookStoreApp.API.Services.Contracts
{
    public interface ITokenService
    {
        Task<AuthResponseDto> GenerateTokenAsync(ApiUser user);
    }
}
