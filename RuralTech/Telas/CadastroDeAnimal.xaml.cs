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
using System.Windows.Threading;

namespace RuralTech.Telas
{
    /// <summary>
    /// Lógica interna para CadastroDeAnimal.xaml
    /// </summary>
    public partial class CadastroDeAnimal : Window
    {
        private DispatcherTimer timer;
        private double targetPosition;
        public CadastroDeAnimal()
        {
            InitializeComponent();
        }

        private void PackIcon_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (appBar_Esquerdo.Margin.Left < targetPosition)
            {
                appBar_Esquerdo.Margin = new Thickness(appBar_Esquerdo.Margin.Left + 5, 0, 0, 0);
            }
            else
            {
                timer.Stop(); // Para o movimento ao alcançar a posição desejada
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
