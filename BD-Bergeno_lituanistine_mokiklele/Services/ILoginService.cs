using BD_Bergeno_lituanistine_mokiklele.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Services {
    public interface ILoginService {
        Task<LoginResponse> Authenticate(LoginRequest loginRequest);
        Task<LoginResponse> AuthenticateSimpleWay(LoginRequest loginRequest);
        //Task<List<UserListResponse>> GetAllUsers();
    }
}
