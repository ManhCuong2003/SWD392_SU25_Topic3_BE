using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Config;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtOptions)
        {
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtOptions.Value;
        }

        #region Login
        public async Task<LoginResponse?> LoginAsync(LoginRequest dto)
        {
            // Lấy user theo email
            // LoginAsync
            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                // User not found
                return null;  // hoặc throw với thông báo rõ ràng
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!passwordValid)
            {
                // Password không đúng
                return null;  // hoặc throw
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                // Thêm claim role nếu cần
                //new Claim(ClaimTypes.Role, user.Role ?? "User"),
            };

            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = now,
                IssuedAt = now,
                Expires = now.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return new LoginResponse
            {
                Token = jwt,
                FullName = user.FullName,
                Role = user.Role ?? "User",
                ExpiresIn = _jwtSettings.ExpiryMinutes * 60
            };
        }
        #endregion

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest dto)
        {
            // Kiểm tra email đã tồn tại chưa
            // LoginAsync
            var normalizedEmail = dto.Email?.Trim().ToLower();
            var user = await _unitOfWork.Users.GetByEmailAsync(normalizedEmail);

            var existingUser = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Email đã được sử dụng.");

            var newUser = new User
            {
                FullName = dto.FullName,
                Email = dto.Email?.Trim().ToLower(),
                Phone = dto.Phone,
                Gender = dto.Gender,
                Address = dto.Address,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow,
                Role = dto.Role,
            };

            // Thêm người dùng mới vào cơ sở dữ liệu
            await _unitOfWork.Users.CreateAsync(newUser);
            await _unitOfWork.SaveAsync();

            // Trả về thông tin người dùng mới
            return new RegisterResponse
            {
                UserId = newUser.UserId,
                FullName = newUser.FullName,
                Email = newUser.Email,
                Role = "User" // Mặc định là "Passenger"
            };
        }

    }
}