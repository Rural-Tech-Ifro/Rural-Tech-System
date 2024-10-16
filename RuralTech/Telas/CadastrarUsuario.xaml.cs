using DataBase.Configuracao;
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
        public CadastrarUsuario()
        {
            InitializeComponent();
        }
        public void InserirBancoDados()
        {
            try
            {
                Conexao conexao = new Conexao();

                var comando = conexao.Comando("INSERT INTO usuario (nome_usu, email_usu, senha_usu) VALUES (@nome, @email, @senha);");
                foreach (Usuario str in Program.usuarios)
                {
                    comando.Parameters.AddWithValue("@nome", str.Nome.Trim());
                    comando.Parameters.AddWithValue("@email", str.Email.Trim());
                    comando.Parameters.AddWithValue("@senha", str.Senha.Trim());

                }
                Program.usuarios.Clear();
                var resultado = comando.ExecuteNonQuery();

                if (resultado > 0)
                {
                    MessageBox.Show("Usuario Cadastrado com sucesso!");
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var usuario = txt_usuario.Text;
                var email = txt_email.Text;
                var senha = txt_senha.Password;
                var confirmarSenha = txt_confirmarSenha.Password;

                if (usuario != "" && email != "" && senha != "")
                {
                    if (senha == confirmarSenha)
                    {
                        Usuario conexao = new Usuario(usuario, email, senha);
                        Program.usuarios.Add(conexao);
                        InserirBancoDados();
                    }
                    else
                    {
                        MessageBox.Show("Senhas não se coincidem!");
                    }
                }
                else
                {
                    MessageBox.Show("Campos vazios!");
                }
            }
            catch
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Login tela = new Login();
            this.Close();
            tela.ShowDialog();
        }
    }
}
