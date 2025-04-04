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
    public class CategoriaForm : Form
    {
        private DataGridView dataGridViewCategorias;
        private Button btnAdicionar;
        private Button btnEditar;
        private Button btnRemover;
        private string tokenJwt;

        public CategoriaForm(string token)
        {
            tokenJwt = token;

            this.Text = "Gerenciar Categorias";
            this.Size = new System.Drawing.Size(500, 400);

            dataGridViewCategorias = new DataGridView
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(440, 250),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dataGridViewCategorias);

            btnAdicionar = new Button
            {
                Text = "Adicionar",
                Location = new System.Drawing.Point(20, 300),

            };
            btnAdicionar.Click += BtnAdicionar_Click;
            this.Controls.Add(btnAdicionar);

            btnEditar = new Button
            {
                Text = "Editar",
                Location = new System.Drawing.Point(120, 300)
            };
            btnEditar.Click += BtnEditar_Click;
            this.Controls.Add(btnEditar);

            btnRemover = new Button
            {
                Text = "Remover",
                Location = new System.Drawing.Point(220, 300)
            };
            btnRemover.Click += BtnRemover_Click;
            this.Controls.Add(btnRemover);

            CarregarCategorias();
        }

        private async void CarregarCategorias()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenJwt);
                    var response = await httpClient.GetAsync("https://localhost:5001/api/Categoria");

                    if (response.IsSuccessStatusCode)
                    {
                        var categorias = await response.Content.ReadFromJsonAsync<List<CategoriaDto>>();
                        dataGridViewCategorias.DataSource = categorias;
                    }
                    else
                    {
                        MessageBox.Show("Erro ao carregar categorias.");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar à API.");
            }
        }

        private async void BtnAdicionar_Click(object sender, EventArgs e)
        {
            string nomeCategoria = Microsoft.VisualBasic.Interaction.InputBox("Digite o nome da nova categoria:", "Adicionar Categoria");

            if (!string.IsNullOrWhiteSpace(nomeCategoria))
            {
                var categoria = new { Nome = nomeCategoria };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

                    var response = await client.PostAsJsonAsync("https://localhost:5001/api/Categoria", categoria);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Categoria adicionada com sucesso.");
                        CarregarCategorias();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao adicionar categoria: " + await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        private async void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewCategorias.SelectedRows.Count > 0)
            {
                var row = dataGridViewCategorias.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["Id"].Value);
                string nomeAtual = row.Cells["Nome"].Value.ToString();

                string novoNome = Microsoft.VisualBasic.Interaction.InputBox("Edite o nome da categoria:", "Editar Categoria", nomeAtual);

                if (!string.IsNullOrWhiteSpace(novoNome))
                {
                    var categoriaAtualizada = new { Id = id, Nome = novoNome };

                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

                        var response = await client.PutAsJsonAsync($"https://localhost:5001/api/Categoria/{id}", categoriaAtualizada);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Categoria atualizada com sucesso.");
                            CarregarCategorias();
                        }
                        else
                        {
                            MessageBox.Show("Erro ao atualizar categoria: " + await response.Content.ReadAsStringAsync());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma categoria para editar.");
            }
        }

        private async void BtnRemover_Click(object sender, EventArgs e)
        {
            if (dataGridViewCategorias.SelectedRows.Count > 0)
            {
                var row = dataGridViewCategorias.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["Id"].Value);

                var confirm = MessageBox.Show("Deseja realmente remover esta categoria?", "Confirmar Remoção", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

                        var response = await client.DeleteAsync($"https://localhost:5001/api/Categoria/{id}");
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Categoria removida com sucesso.");
                            CarregarCategorias();
                        }
                        else
                        {
                            MessageBox.Show("Erro ao remover categoria: " + await response.Content.ReadAsStringAsync());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma categoria para remover.");
            }
        }

    }
}
