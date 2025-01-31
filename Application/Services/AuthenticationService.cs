using Application.Dto.Authentication;
using Application.Interfaces;
using Application.Utility.ExceptionHandling.ExceptionTypes;
using Application.Utility.Jwt;
using Domain.Entities;
using Mapster;


namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private IUserService _userService;
        private JwtHelperService _jwt;
        private IInvitationService _invite;
        public AuthenticationService(IUserService userService, JwtHelperService jwt, IInvitationService invite)
        {
            _userService = userService;
            _jwt = jwt;
            _invite = invite;
        }



        public async Task<LoginResponse> Login(LoginRequest user)
        {
            var oUser = await _userService.GetByConditionAsync(x => x.Email == user.email && x.Password == user.password, x => x.Role,x=>x.Vendor);
            if (oUser.Count() > 0)
            {
                string token = _jwt.GenerateToken(oUser.FirstOrDefault().Email);
                if (!string.IsNullOrEmpty(token))
                {

                    Token oToken = new Token();
                    oToken.value = token;
                    oToken.expires = null;
                    return new LoginResponse(oUser.FirstOrDefault(), oToken);


                }
                throw new Exception("Erro while generating token");
            }
            throw new BusinessException("User not found");
        }


        public async Task<User> Register(RegistrationDto user)
        {
            var oUser = (await _userService.GetByConditionAsync(x => x.Email == user.email)).FirstOrDefault();
            if (oUser != null)
            {
                throw new Exception("User with same id exists");
            }
            oUser = new User();
            oUser = user.Adapt<User>();
            oUser.CreatedBy = 1;
            await _userService.AddAsync(oUser);
            oUser = (await _userService.GetByConditionAsync(x => x.Email == user.email)).FirstOrDefault();

            if (oUser != null)
            {
                var invitation = (await _invite.GetByConditionAsync(x => x.Code == Guid.Parse(user.invitationId))).FirstOrDefault();
                invitation.IsUsed = true;
                await _invite.UpdateAsync(invitation);
                return oUser;
            }
            throw new Exception("Oops! Something Went Wrong."); 

        }


        public async Task<Invitation> SendInvite(InvitationDto invitationDto)
        {
            
            var invitation = invitationDto.Adapt<Invitation>();
            var existingInvitation = (await _invite.GetByConditionAsync(x => x.Email == invitation.Email)).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (existingInvitation == null)
            {
                return await _invite.AddAsync(invitation);
            }
            else
            {
                if (!_invite.IsInvitationExpired(invitation.ExpiresAt))
                {
                    throw new BusinessException("The invitation has already been sent to this email address and is still valid.");
                }
                return await _invite.AddAsync(invitation);
            }

        }
        public async Task<Invitation> GetInvite(string code)
        {
            var invitation = (await _invite.GetByConditionAsync(x => x.Code == Guid.Parse(code),x=>x.Role)).FirstOrDefault();

            if (invitation == null)
            {
                throw new BusinessException("Invitation code is invalid");
            }

            if (_invite.IsInvitationExpired(invitation.ExpiresAt) || invitation.IsUsed)
            {
                throw new BusinessException("Invitation has been expired");
            }


            return invitation;

        }
    }
}
