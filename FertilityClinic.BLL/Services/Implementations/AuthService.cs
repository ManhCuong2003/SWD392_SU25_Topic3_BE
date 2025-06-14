using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Config;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
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
            // Check if the user exists and validate the password
            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new Exception(message: "Invalid email or password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName ?? ""),
            new Claim(ClaimTypes.Role, user.Role ?? "User"),
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            var responseOjb = new LoginResponse
            {
                Token = jwt,
                //Role = user.Role ?? "User",
                //FullName = user.FullName,
                //ExpiresIn = _jwtSettings.ExpiryMinutes * 60
            };

            return (responseOjb);
        }


        #endregion

        #region Register
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest dto)
        {
            //var permission = dto.Role ?? "User";

            //if (dto.Role != null && dto.Role != "User" && dto.Role != "Admin" && dto.Role != "Doctor" && dto.Role != "Manager" /*&& dto.Role != "Patient"*/)
            // permission = "User";

            string role = "User";
            // Kiểm tra email đã tồn tại chưa
            // LoginAsync
            var normalizedEmail = dto.Email?.Trim().ToLower();
            var user = await _unitOfWork.Users.GetByEmailAsync(normalizedEmail);

            if (await _unitOfWork.Users.IsEmailExistsAsync(dto.Email, 0))
                throw new Exception("Email đã được sử dụng.");

            var newUser = new User
            {
                
                FullName = dto.FullName,
                Email = normalizedEmail,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = null,      
                Gender = null, 
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                HealthInsuranceId = null,
                Address = null,      
                CreatedAt = DateTime.UtcNow,
                Role = "User",
                NationalId = null,     
                PartnerId = null // Mặc định là null nếu không có thông tin partner

            };

            // Thêm người dùng mới vào cơ sở dữ liệu
            await _unitOfWork.Users.CreateAsync(newUser);
            await _unitOfWork.SaveAsync();

            // Trả về thông tin người dùng mới
            return new RegisterResponse
            {
                UserId = newUser.UserId,
                FullName = newUser.FullName,
                DateOfBirth = newUser.DateOfBirth,
                Gender = newUser.Gender,
                Email = newUser.Email,
                Phone = newUser.Phone,
                Role = newUser.Role // Mặc định là "User"
                
                
            };
        }
        #endregion
        public async Task<LoginResponse> LoginWithGoogleAsync(string email, string fullName)
        {
            // 1. Tìm user theo email
            var user = await _unitOfWork.Users.GetByEmailAsync(email);

            // 2. Nếu chưa có thì tạo mới user với giá trị mặc định cho các trường NOT NULL
            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    FullName = fullName ?? "Google User",
                    Phone = null, // Giá trị mặc định nếu NOT NULL
                    DateOfBirth = DateOnly.MinValue, // Hoặc một ngày mặc định nếu NOT NULL
                    Address = null,
                    Gender = null,
                    Password = null,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Role = "User",
                    NationalId = null,     // Thêm giá trị mặc định cho NationalId
                    HealthInsuranceId = null,
                    PartnerId = 0
                };
                await _unitOfWork.Users.CreateAsync(user);
                await _unitOfWork.SaveAsync();
            }

            // 3. Tạo JWT token cho user (giữ nguyên như cũ)
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.FullName ?? ""),
        new Claim(ClaimTypes.Role, user.Role ?? "User"),
    };

            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = now,
                IssuedAt = now,
                //Expires = now.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            // 4. Trả về LoginResponse
            return new LoginResponse
            {
                Token = jwt,
                //Role = user.Role ?? "User",
                //FullName = user.FullName,
                //ExpiresIn = _jwtSettings.ExpiryMinutes * 60
            };
        }
    }
}