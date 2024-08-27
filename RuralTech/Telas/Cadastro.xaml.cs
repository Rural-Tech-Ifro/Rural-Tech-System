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
    /// Lógica interna para Cadastro.xaml
    /// </summary>
    public partial class Cadastro : Window
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var usuario = txt_usuario.Text;
            var email = txt_email.Text;
            var senha = txt_senha.Password;
               
            if (usuario != "" && email != "" && senha != "")
            {
                MessageBox.Show("Teste Cadastro: " + usuario + "\n" + email + " \n" + senha);

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
