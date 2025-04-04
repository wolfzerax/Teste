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
    public class ProdutoService
    {
        public async Task<List<ProdutoDto>> GetAllAsync()
        {
            var response = await ApiClient.Instance.GetFromJsonAsync<List<ProdutoDto>>("api/produtos");
            return response ?? new List<ProdutoDto>();
        }

        // Você pode adicionar AddAsync, DeleteAsync etc aqui
    }
}
