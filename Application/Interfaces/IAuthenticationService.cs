using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Authentication;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(LoginRequest user);
        Task<User> Register(RegistrationDto user);
        Task<Invitation> SendInvite(InvitationDto invitation);
        Task<Invitation> GetInvite(string code);
    }
}
