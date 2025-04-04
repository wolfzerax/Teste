using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAPI.Forms
{
    public class MainForm : Form
    {
        private Button btnProdutos;
        private Button btnCategorias;
        private Button btnEstatisticas;
        private string tokenJwt;

        public MainForm(string token)
        {
            tokenJwt = token;

            this.Text = "Menu Principal";
            this.Size = new System.Drawing.Size(300, 250);

            btnProdutos = new Button
            {
                Text = "Gerenciar Produtos",
                Location = new System.Drawing.Point(50, 30),
                Size = new System.Drawing.Size(200, 40)
            };
            btnProdutos.Click += (s, e) => new ProdutoForm(tokenJwt).ShowDialog();
            this.Controls.Add(btnProdutos);

            btnCategorias = new Button
            {
                Text = "Gerenciar Categorias",
                Location = new System.Drawing.Point(50, 90),
                Size = new System.Drawing.Size(200, 40)
            };
            btnCategorias.Click += (s, e) => new CategoriaForm(tokenJwt).ShowDialog();
            this.Controls.Add(btnCategorias);

            btnEstatisticas = new Button
            {
                Text = "Ver Estatísticas",
                Location = new System.Drawing.Point(50, 150),
                Size = new System.Drawing.Size(200, 40)
            };
            btnEstatisticas.Click += (s, e) => new EstatisticasForm(tokenJwt).ShowDialog();
            this.Controls.Add(btnEstatisticas);

        }
    }
}
