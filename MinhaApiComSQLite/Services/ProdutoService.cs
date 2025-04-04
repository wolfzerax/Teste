using AutoMapper;
using MinhaApiComSQLite.DTOs;
using MinhaApiComSQLite.Models;
using MinhaApiComSQLite.Repository.Interface;
using MinhaApiComSQLite.Services.Interface;

namespace MinhaApiComSQLite.Services
{

    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogger<ProdutoService> _logger;

        public ProdutoService(
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository,
            ILogger<ProdutoService> logger)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllAsync();
                return produtos.Select(p => new ProdutoDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    CategoriaId = p.CategoriaId
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os produtos.");
                throw;
            }
        }

        public async Task<ProdutoDto> GetByIdAsync(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetByIdAsync(id);

                if (produto == null)
                    throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");

                return new ProdutoDto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco,
                    CategoriaId = produto.CategoriaId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produto por ID.");
                throw;
            }
        }

        public async Task<ProdutoDto> AddAsync(ProdutoDto produtoDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(produtoDto.Nome))
                    throw new ArgumentException("O nome do produto é obrigatório.");

                if (produtoDto.Preco <= 0)
                    throw new ArgumentException("O preço do produto deve ser maior que zero.");

                var categoria = await _categoriaRepository.GetByIdAsync(produtoDto.CategoriaId);
                if (categoria == null)
                    throw new ArgumentException("A categoria informada não existe.");

                var produto = new Produto
                {
                    Nome = produtoDto.Nome,
                    Preco = produtoDto.Preco,
                    CategoriaId = produtoDto.CategoriaId
                };

                await _produtoRepository.AddAsync(produto);

                return new ProdutoDto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco,
                    CategoriaId = produto.CategoriaId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar produto.");
                throw;
            }
        }

        public async Task<ProdutoDto> UpdateAsync(ProdutoDto produtoDto)
        {
            try
            {
                var produtoExistente = await _produtoRepository.GetByIdAsync(produtoDto.Id);
                if (produtoExistente == null)
                    throw new KeyNotFoundException($"Produto com ID {produtoDto.Id} não encontrado.");

                // Validação (mesmas do AddAsync)
                if (string.IsNullOrWhiteSpace(produtoDto.Nome))
                    throw new ArgumentException("O nome do produto é obrigatório.");

                if (produtoDto.Preco <= 0)
                    throw new ArgumentException("O preço do produto deve ser maior que zero.");

                var categoria = await _categoriaRepository.GetByIdAsync(produtoDto.CategoriaId);
                if (categoria == null)
                    throw new ArgumentException("A categoria informada não existe.");

                // Atualiza dados
                produtoExistente.Nome = produtoDto.Nome;
                produtoExistente.Preco = produtoDto.Preco;
                produtoExistente.CategoriaId = produtoDto.CategoriaId;

                await _produtoRepository.UpdateAsync(produtoExistente);

                return new ProdutoDto
                {
                    Id = produtoExistente.Id,
                    Nome = produtoExistente.Nome,
                    Preco = produtoExistente.Preco,
                    CategoriaId = produtoExistente.CategoriaId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar produto.");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetByIdAsync(id);
                if (produto == null)
                    throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");

                await _produtoRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir produto.");
                throw;
            }
        }

        public async Task<RelatorioEstatisticasDto> ObterRelatorioAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();

            var media = produtos.Any() ? produtos.Average(p => p.Preco) : 0;

            return new RelatorioEstatisticasDto
            {
                MediaPrecos = media,
            };
        }

    }

}
