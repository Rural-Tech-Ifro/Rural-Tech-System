using RuralTech.Database;
using RuralTech.Integracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RuralTech.Telas
{
    /// <summary>
    /// Lógica interna para CadastrarUsuario.xaml
    /// </summary>
    public partial class CadastrarUsuario : Window
    {
        private Usuario _usuario = new Usuario(); // Objeto vacina
        private UsuarioDAO _usuarioDAO = new UsuarioDAO(); // Objeto responsável por acessar o banco de dados
        public CadastrarUsuario()
        {
            InitializeComponent();
        }
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TelaLogin tela = new TelaLogin();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Atualiza o objeto _vacina com os valores do formulário
                _usuario.Nome = txt_usuario.Text;
                _usuario.Senha = txt_senha.Password;
                _usuario.Email = txt_email.Text;

                _usuarioDAO.Insert(_usuario); // Insere no banco
                MessageBox.Show("Usuario cadastrado com sucesso.");

                TelaLogin tela = new TelaLogin();
                this.Close();
                tela.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
        }
    }
}
