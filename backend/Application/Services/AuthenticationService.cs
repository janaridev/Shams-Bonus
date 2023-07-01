using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using backend.Application.Dtos;
using backend.Application.IServices;
using backend.Domain.Entities;
using backend.Domain.Exceptions.NotFound;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace backend.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    protected ApiResponse _response;

    private User _user;

    public AuthenticationService(IMapper mapper, UserManager<User> userManager,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _response = new();
    }

    public async Task<ApiResponse> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        _user = await _userManager.FindByNameAsync(userForRegistration.UserName);
        if (_user != null)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.Conflict;
            _response.ErrorMessages.Add("Данный номер телефона уже занят.");
            Console.WriteLine("Username already exists");

            return _response;
        }

        var isValidPassword = ValidPassword(userForRegistration.Password);
        if (!isValidPassword)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add("В пароле должна быть одна цифра");

            return _response;
        }

        var user = _mapper.Map<User>(userForRegistration);
        var isMatched = ConfirmPassword(userForRegistration.Password, userForRegistration.ConfirmPassword);
        if (!isMatched)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add("Пароли не совпадают");
            Console.WriteLine("Passwords are not match");

            return _response;
        }

        var result = await _userManager.CreateAsync(user, userForRegistration.Password);
        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, "User");

        _response.StatusCode = HttpStatusCode.Created;
        return _response;
    }

    public async Task<ApiResponse> ValidateUser(UserForAuthenticationDto userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.Username);

        var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
        if (!result)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add("Неправильный пароль или номер телефона.");
            Console.WriteLine($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password");

            return _response;
        }

        var userDetails = new UserDetailsDto
        {
            FirstName = _user.FirstName,
            LastName = _user.LastName,
            UserName = _user.UserName, //phone number
        };

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = new ResultDto(userDetails, await CreateToken());
        return _response;
    }

    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
        Console.WriteLine(key);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName),
            new Claim("userId", _user.Id)
        };

        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
        List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
        );

        return tokenOptions;
    }

    private bool ConfirmPassword(string password, string confirmPassword)
    {
        return password == confirmPassword ? true : false;
    }

    private bool ValidPassword(string password)
    {
        return password.Any(char.IsDigit);
    }
}