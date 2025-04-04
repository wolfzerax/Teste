using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WindowsFormsAPI.Forms
{
    public class EstatisticasForm : Form
    {
        private Label lblMediaPreco;

        public EstatisticasForm(string tokenJwt)
        {
            this.Text = "Estatísticas de Produtos";
            this.Size = new System.Drawing.Size(300, 150);

            lblMediaPreco = new Label
            {
                Text = "Carregando média de preço...",
                AutoSize = true,
                Location = new System.Drawing.Point(20, 40)
            };
            this.Controls.Add(lblMediaPreco);

            LoadMediaPreco(tokenJwt);
        }

        private async void LoadMediaPreco(string tokenJwt)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:5001/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJwt);

                    var response = await client.GetAsync("Relatorios");
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonDocument.Parse(json);

                    decimal mediaPreco = result.RootElement.GetProperty("mediaPrecos").GetDecimal();

                    lblMediaPreco.Text = $"Média dos preços dos produtos: R$ {mediaPreco:F2}";
                }
            }
            catch (Exception ex)
            {
                lblMediaPreco.Text = $"Erro ao carregar estatísticas: {ex.Message}";
            }
        }
    }
}
