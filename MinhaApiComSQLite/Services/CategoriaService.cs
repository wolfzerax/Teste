using AutoMapper;
using MinhaApiComSQLite.DTOs;
using MinhaApiComSQLite.Models;
using MinhaApiComSQLite.Repository.Interface;
using MinhaApiComSQLite.Services.Interface;

namespace MinhaApiComSQLite.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogger<CategoriaService> _logger;

        public CategoriaService(ICategoriaRepository categoriaRepository, ILogger<CategoriaService> logger)
        {
            _categoriaRepository = categoriaRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoriaDto>> GetAllAsync()
        {
            try
            {
                var categorias = await _categoriaRepository.GetAllAsync();
                return categorias.Select(c => new CategoriaDto
                {
                    Id = c.Id,
                    Nome = c.Nome
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todas as categorias.");
                throw;
            }
        }

        public async Task<CategoriaDto> GetByIdAsync(int id)
        {
            try
            {
                var categoria = await _categoriaRepository.GetByIdAsync(id);

                if (categoria == null)
                {
                    throw new KeyNotFoundException($"Categoria com ID {id} não encontrada.");
                }

                return new CategoriaDto
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                };
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Tentativa de acessar uma categoria inexistente com ID {Id}.", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar categoria com ID {Id}.", id);
                throw;
            }
        }


        public async Task<CategoriaDto> AddAsync(CategoriaDto categoriaDto)
        {
            try
            {
                var categoria = new Categoria
                {
                    Id = categoriaDto.Id,
                    Nome = categoriaDto.Nome
                };

                await _categoriaRepository.AddAsync(categoria);

                return new CategoriaDto
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar uma nova categoria.");
                throw;
            }
        }

        public async Task<CategoriaDto> UpdateAsync(CategoriaDto categoriaDto)
        {
            try
            {
                var categoria = new Categoria
                {
                    Nome = categoriaDto.Nome
                };

                await _categoriaRepository.UpdateAsync(categoria);

                return new CategoriaDto
                {
                    Nome = categoria.Nome
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar a categoria.");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _categoriaRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir categoria com ID {Id}.", id);
                throw;
            }
        }
    }

}
