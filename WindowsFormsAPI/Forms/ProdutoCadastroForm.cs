using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsAPI.Models;

namespace WindowsFormsAPI.Forms
{
    public class ProdutoCadastroForm : Form
    {
        private TextBox txtNome;
        private NumericUpDown nudPreco;
        private ComboBox cbCategorias;
        private Button btnSalvar;

        private string _token;
        private int? _produtoId = null;

        public ProdutoCadastroForm(string token, int? produtoId = null)
        {
            _token = token;
            _produtoId = produtoId;

            this.Text = produtoId == null ? "Adicionar Produto" : "Editar Produto";
            this.Size = new System.Drawing.Size(400, 300);

            Label lblNome = new Label { Text = "Nome:", Location = new System.Drawing.Point(20, 20) };
            txtNome = new TextBox { Location = new System.Drawing.Point(120, 20), Width = 250 };

            Label lblPreco = new Label { Text = "Preço:", Location = new System.Drawing.Point(20, 60) };
            nudPreco = new NumericUpDown
            {
                Location = new System.Drawing.Point(120, 60),
                Width = 100,
                DecimalPlaces = 2,
                Minimum = 0.01M,
                Maximum = 1000000
            };

            Label lblCategoria = new Label { Text = "Categoria:", Location = new System.Drawing.Point(20, 100) };
            cbCategorias = new ComboBox
            {
                Location = new System.Drawing.Point(120, 100),
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            btnSalvar = new Button
            {
                Text = "Salvar",
                Location = new System.Drawing.Point(150, 160),
                Width = 100
            };
            btnSalvar.Click += BtnSalvar_Click;

            this.Controls.Add(lblNome);
            this.Controls.Add(txtNome);
            this.Controls.Add(lblPreco);
            this.Controls.Add(nudPreco);
            this.Controls.Add(lblCategoria);
            this.Controls.Add(cbCategorias);
            this.Controls.Add(btnSalvar);

            CarregarCategorias();

            if (produtoId.HasValue)
            {
                CarregarProduto(produtoId.Value);
            }
        }

        private async void CarregarCategorias()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await client.GetAsync("https://localhost:5001/api/Categoria");

            if (response.IsSuccessStatusCode)
            {
                var categorias = await response.Content.ReadFromJsonAsync<List<CategoriaDto>>();
                cbCategorias.DataSource = categorias;
                cbCategorias.DisplayMember = "Nome";
                cbCategorias.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("Erro ao carregar categorias.");
            }
        }

        private async void CarregarProduto(int id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await client.GetAsync($"https://localhost:5001/api/Produto/{id}");

            if (response.IsSuccessStatusCode)
            {
                var produto = await response.Content.ReadFromJsonAsync<ProdutoDto>();
                txtNome.Text = produto.Nome;
                nudPreco.Value = produto.Preco;
                cbCategorias.SelectedValue = produto.CategoriaId;
            }
            else
            {
                MessageBox.Show("Erro ao carregar dados do produto.");
            }
        }

        private async void BtnSalvar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            decimal preco = nudPreco.Value;
            int categoriaId = (int)cbCategorias.SelectedValue;

            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("O nome do produto é obrigatório.");
                return;
            }

            if (preco <= 0)
            {
                MessageBox.Show("O preço deve ser maior que zero.");
                return;
            }

            var produto = new
            {
                Id = _produtoId,
                Nome = nome,
                Preco = preco,
                CategoriaId = categoriaId
            };

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            HttpResponseMessage response;
            if (_produtoId == null)
            {
                var produtoParaEnvio = new
                {
                    Nome = produto.Nome,
                    Preco = produto.Preco,
                    CategoriaId = produto.CategoriaId
                };
                response = await client.PostAsJsonAsync("https://localhost:5001/api/Produto", produtoParaEnvio);
            }
               
            else
                response = await client.PutAsJsonAsync($"https://localhost:5001/api/Produto/{_produtoId}", produto);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Produto salvo com sucesso.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao salvar produto: " + await response.Content.ReadAsStringAsync());
            }
        }
    }
}
