using AutoMapper;
using DLEventSphere.Abstract;
using DLEventSphere.DTO_s;
using DLEventSphere.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLEventSphere
{
    public class BlUser
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public BlUser(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // ===================== GET ALL USERS =====================
        public async Task<ServiceResponse<List<UserResponseDto>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<UserResponseDto>>();

            try
            {
                var users = await _repo.GetAllUsersAsync();
                response.Data = _mapper.Map<List<UserResponseDto>>(users);
                response.Message = "Users retrieved successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // ===================== REGISTER USER =====================
        public async Task<ServiceResponse<UserResponseDto>> AddUser(
            UserRegistration userRegistration,
            bool isAdmin)
        {
            var response = new ServiceResponse<UserResponseDto>();

            try
            {
                var existing = await _repo.GetUserByEmail(userRegistration.Email);
                if (existing != null)
                    throw new InvalidOperationException("Email already exists");

                var user = _mapper.Map<User>(userRegistration);
                user.CreatedAt = DateTime.UtcNow;

                // ⚠️ TEMPORARY: plain password (hash later)
                user.Password = userRegistration.Password;

                user.role = isAdmin ? userRegistration.role : Role.Attendee;

                await _repo.AddUserAsync(user);

                response.Data = _mapper.Map<UserResponseDto>(user);
                response.Message = "Registration successful!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // ===================== AUTHENTICATE USER =====================
        public async Task<ServiceResponse<UserResponseDto>> AuthenticateUser(AuthDetails auth)
        {
            var response = new ServiceResponse<UserResponseDto>();

            try
            {
                var user = await _repo.GetUserByEmail(auth.Email);
                if (user == null)
                    throw new InvalidOperationException("Invalid email or password");

                // ⚠️ TEMPORARY: plain password check
                if (user.Password != auth.Password)
                    throw new InvalidOperationException("Invalid email or password");

                response.Data = _mapper.Map<UserResponseDto>(user);
                response.Message = "Login successful!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // ===================== CHANGE PASSWORD =====================
        public async Task<ServiceResponse<bool>> ChangePasswordAsync(
            long userId,
            ChangePassword dto)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var user = await _repo.GetUserByIdAsync(userId)
                    ?? throw new InvalidOperationException("User not found");

                if (dto.NewPassword != dto.ConfirmPassword)
                    throw new InvalidOperationException(
                        "New password and confirm password do not match");

                // ⚠️ TEMPORARY: plain password check
                if (user.Password != dto.CurrentPassword)
                    throw new InvalidOperationException("Current password is incorrect");

                user.Password = dto.NewPassword;
                await _repo.UpdateUserAsync(user);

                response.Data = true;
                response.Message = "Password changed successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
