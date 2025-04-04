using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WindowsFormsAPI.Forms
{
    public class LoginForm : Form
    {
        private TextBox txtUsuario;
        private TextBox txtSenha;
        private Button btnLogin;
        private Label lblUsuario;
        private Label lblSenha;
        private string tokenJwt;

        public string TokenJwt => tokenJwt; // Propriedade pública para obter o token

        public LoginForm()
        {
            this.Text = "Login";
            this.Size = new System.Drawing.Size(300, 200);

            lblUsuario = new Label { Text = "Usuário:", Location = new System.Drawing.Point(20, 20) };
            txtUsuario = new TextBox { Location = new System.Drawing.Point(120, 20), Width = 150 };

            lblSenha = new Label { Text = "Senha:", Location = new System.Drawing.Point(20, 60) };
            txtSenha = new TextBox { Location = new System.Drawing.Point(120, 60), Width = 150, PasswordChar = '*' };

            btnLogin = new Button { Text = "Login", Location = new System.Drawing.Point(100, 100) };
            btnLogin.Click += BtnLogin_Click;

            this.Controls.Add(lblUsuario);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(lblSenha);
            this.Controls.Add(txtSenha);
            this.Controls.Add(btnLogin);
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Usuário e senha são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var loginData = new { Username = usuario, Password = senha };
                    var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("https://localhost:5001/api/Auth/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                        tokenJwt = result.Token;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credenciais inválidas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar à API.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // DTO para resposta da API
    public class LoginResponseDto
    {
        public string Token { get; set; }
    }


}

