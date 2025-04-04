using WindowsFormsAPI.Forms;

namespace WindowsFormsAPI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            


            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(loginForm.TokenJwt))
                {
                    Application.Run(new MainForm(loginForm.TokenJwt));
                }
                else
                {
                    MessageBox.Show("Login cancelado ou inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
