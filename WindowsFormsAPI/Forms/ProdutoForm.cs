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
    public class ProdutoForm : Form
    {
        private DataGridView dgvProdutos;
        private Button btnAdicionar;
        private Button btnEditar;
        private Button btnRemover;

        private string _token;

        public ProdutoForm(string token)
        {
            _token = token;

            this.Text = "Gerenciar Produtos";
            this.Size = new System.Drawing.Size(600, 400);

            dgvProdutos = new DataGridView
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(540, 250),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            this.Controls.Add(dgvProdutos);

            btnAdicionar = new Button
            {
                Text = "Adicionar",
                Location = new System.Drawing.Point(20, 290),
                Size = new System.Drawing.Size(100, 30)
            };
            btnAdicionar.Click += BtnAdicionar_Click;
            this.Controls.Add(btnAdicionar);

            btnEditar = new Button
            {
                Text = "Editar",
                Location = new System.Drawing.Point(130, 290),
                Size = new System.Drawing.Size(100, 30)
            };
            btnEditar.Click += BtnEditar_Click;
            this.Controls.Add(btnEditar);

            btnRemover = new Button
            {
                Text = "Remover",
                Location = new System.Drawing.Point(240, 290),
                Size = new System.Drawing.Size(100, 30)
            };
            btnRemover.Click += BtnRemover_Click;
            this.Controls.Add(btnRemover);

            CarregarProdutos();
        }

        private async void CarregarProdutos()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await client.GetAsync("https://localhost:5001/api/Produto");

            if (response.IsSuccessStatusCode)
            {
                var produtos = await response.Content.ReadFromJsonAsync<List<ProdutoDto>>();
                dgvProdutos.DataSource = produtos;
            }
            else
            {
                MessageBox.Show("Erro ao carregar produtos.");
            }
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            var form = new ProdutoCadastroForm(_token);
            form.ShowDialog();
            CarregarProdutos();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.CurrentRow == null)
            {
                MessageBox.Show("Selecione um produto para editar.");
                return;
            }

            var produto = (ProdutoDto)dgvProdutos.CurrentRow.DataBoundItem;
            var form = new ProdutoCadastroForm(_token, produto.Id);
            form.ShowDialog();
            CarregarProdutos();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // ProdutoForm
            // 
            ClientSize = new Size(522, 261);
            Name = "ProdutoForm";
            Load += ProdutoForm_Load;
            ResumeLayout(false);
        }

        private async void BtnRemover_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.CurrentRow == null)
            {
                MessageBox.Show("Selecione um produto para remover.");
                return;
            }

            var produto = (ProdutoDto)dgvProdutos.CurrentRow.DataBoundItem;

            var confirmar = MessageBox.Show($"Deseja realmente remover o produto '{produto.Nome}'?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirmar != DialogResult.Yes)
                return;

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await client.DeleteAsync($"https://localhost:5001/api/Produto/{produto.Id}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Produto removido com sucesso.");
                CarregarProdutos();
            }
            else
            {
                MessageBox.Show("Erro ao remover produto.");
            }
        }

        private void ProdutoForm_Load(object sender, EventArgs e)
        {

        }
    }

}
