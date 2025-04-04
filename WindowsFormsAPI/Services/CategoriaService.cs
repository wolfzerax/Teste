using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsAPI.Models;
using WindowsFormsAPI.Utils;

namespace WindowsFormsAPI.Services
{
    public class CategoriaService
    {
        public async Task<List<CategoriaDto>> GetAllAsync()
        {
            var response = await ApiClient.Instance.GetFromJsonAsync<List<CategoriaDto>>("api/categorias");
            return response ?? new List<CategoriaDto>();
        }
    }
}
