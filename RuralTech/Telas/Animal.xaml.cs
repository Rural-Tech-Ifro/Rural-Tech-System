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
using System.Text.RegularExpressions;


namespace RuralTech.Telas
{
    /// <summary>
    /// Lógica interna para Animal.xaml
    /// </summary>
    public partial class Animal : Window
    {
        public Animal()
        {
            InitializeComponent();
        }

        private void OpenModal(object sender, RoutedEventArgs e)
        {
            PropertyPopup.IsOpen = true;
        }

        private void CloseModal(object sender, RoutedEventArgs e)
        {
            PropertyPopup.IsOpen = false;
        }

        private void SaveProperty(object sender, RoutedEventArgs e)
        {
            string brinco = txt_brinco.Text;
            string raca = combo_raca.Text;
            string classificacao = txt_classificacao.Text;
            string sexo = combo_sex.Text;
            string cep = txt_cep.Text;
            string endereco = txt_endereco.Text;
            string numero = txt_numero.Text;
            string bairro = txt_bairro.Text;
            string complemento = txt_complemento.Text;
            PropertyPopup.IsOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Equipamentos tela = new Equipamentos();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Medicamentos tela = new Medicamentos();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Propriedades tela = new Propriedades();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Pastos tela = new Pastos();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Vacinas tela = new Vacinas();
            this.Close();
            tela.ShowDialog();
        }
    }
}
