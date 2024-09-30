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
            string propertyName = txt_propriedade.Text;
            string ownerName = txt_proprietario.Text;
            string size = txt_tamanho.Text;
            string phone = txt_telefone.Text;
            string zipCode = txt_cep.Text;
            string address = txt_endereco.Text;
            string number = txt_numero.Text;
            string neighborhood = txt_bairro.Text;
            string complement = txt_complemento.Text;
            // Lógica para salvar as informações da propriedade

            // Fechar o modal após salvar
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
    }

}
