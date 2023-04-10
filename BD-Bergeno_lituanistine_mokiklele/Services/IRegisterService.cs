using BD_Bergeno_lituanistine_mokiklele.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Services {
    public interface IRegisterService {

        Task<RegisterResponse> RegisterNewUser(RegisterRequest request);
        //Task<UserBasicInfo> UpdateUserBasicInfo(RegisterRequest request);
        //Task<LoginResponse> Authenticate(LoginRequest loginRequest);
    }
}
