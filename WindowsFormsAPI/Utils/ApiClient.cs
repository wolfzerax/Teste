using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAPI.Utils
{
    public static class ApiClient
    {
        public static readonly HttpClient Instance = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/") // ou sua URL
        };
    }
}
