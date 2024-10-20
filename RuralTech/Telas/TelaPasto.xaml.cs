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
    /// Lógica interna para Pastos.xaml
    /// </summary>
    public partial class TelaPasto : Window
    {
        public TelaPasto()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TelaAnimal tela = new TelaAnimal();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TelaEquipamento tela = new TelaEquipamento();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TelaMedicamento tela = new TelaMedicamento();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            TelaPropriedade tela = new TelaPropriedade();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            TelaVacina tela = new TelaVacina();
            this.Close();
            tela.ShowDialog();
        }
    }
}
