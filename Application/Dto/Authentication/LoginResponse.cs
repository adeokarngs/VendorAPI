using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dto.Authentication
{
    public class LoginResponse
    {
        public LoginResponse() { }
        public LoginResponse(User pUser, Token pToken)
        {
            user = pUser;
            token = pToken;
        }

        public User user { get; set; }
        public Token token { get; set; }

    }
}
