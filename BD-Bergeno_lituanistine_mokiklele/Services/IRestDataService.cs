using BD_Bergeno_lituanistine_mokiklele.Platforms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Services
{
    public interface IRestDataService
    {
        Task<List<Post>> GetAllPostsAsync();

    }
}
