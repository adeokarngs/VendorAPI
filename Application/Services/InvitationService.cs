using Application.Interfaces;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services
{
    public class InvitationService : BaseService<Invitation>, IInvitationService
    {
        public InvitationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public bool IsInvitationExpired(DateTime invitationExpiryDate)
        {
            return DateTime.UtcNow > invitationExpiryDate;
        }
    }
}
