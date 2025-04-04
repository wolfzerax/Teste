using MinhaApiComSQLite.DTOs;

namespace MinhaApiComSQLite.Services.Interface
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDto>> GetAllAsync();
        Task<CategoriaDto> GetByIdAsync(int id);
        Task<CategoriaDto> AddAsync(CategoriaDto categoriaDto);
        Task<CategoriaDto> UpdateAsync(CategoriaDto categoriaDto);
        Task DeleteAsync(int id);
    }
}
