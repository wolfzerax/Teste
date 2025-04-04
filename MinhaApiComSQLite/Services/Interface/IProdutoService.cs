using MinhaApiComSQLite.DTOs;

namespace MinhaApiComSQLite.Services.Interface
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDto>> GetAllAsync();
        Task<ProdutoDto> GetByIdAsync(int id);
        Task<ProdutoDto> AddAsync(ProdutoDto produtoDto);
        Task<ProdutoDto> UpdateAsync(ProdutoDto produtoDto);
        Task DeleteAsync(int id);
        Task<RelatorioEstatisticasDto> ObterRelatorioAsync();

    }
}
