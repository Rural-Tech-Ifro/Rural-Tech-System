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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RuralTech.Telas
{
    /// <summary>
    /// Lógica interna para Login.xaml
    /// </summary>
    public partial class TelaLogin : Window
    {
        private UsuarioDAO _usuarioDAO = new UsuarioDAO(); // Objeto responsável por acessar o banco de dados
        public TelaLogin()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool validacao = false;
            var usuario = txt_usuario.Text;
            var senha = txt_senha.Password;

            foreach(Usuario str in _usuarioDAO.GetUsuario())
            {
                if (str.Nome == usuario && str.Senha == senha)
                {
                    MessageBox.Show("Logado com sucesso!");
                    validacao = true;
                    TelaAnimal login = new TelaAnimal();
                    this.Close();
                    login.ShowDialog();
                    break;
                }
            }

            if (validacao == false)
            {
                MessageBox.Show("Usuario não cadastrado!");

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CadastrarUsuario tela = new CadastrarUsuario();
            this.Close();
            tela.ShowDialog();
        }
    }
}
