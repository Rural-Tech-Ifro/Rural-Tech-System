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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;

namespace RuralTech.Telas
{
    /// <summary>
    /// Interação lógica para CadastrarEquipamentos.xam
    /// </summary>
    public partial class CadastrarEquipamentos : Page
    {
        private bool isMenuExpanded = false;
        private double posicaoAtual;
        public CadastrarEquipamentos()
        {
            InitializeComponent();
            posicaoAtual = appBar_Esquerdo.ActualWidth;

        }

        private void imagem_status_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();

            if (isMenuExpanded)
            {
                button1.Margin = new Thickness(0, 20, 0, 0);
                button2.Margin = new Thickness(0, 20, 0, 0);
                button3.Margin = new Thickness(0, 20, 0, 0);
                button4.Margin = new Thickness(0, 20, 0, 0);
                button5.Margin = new Thickness(0, 20, 0, 0);
                button6.Margin = new Thickness(0, 20, 0, 0);
                button7.Margin = new Thickness(0, 220, 0, 0);
                animation.From = posicaoAtual;
                animation.To = posicaoAtual + 65;
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            }
            else
            {
                button1.Margin = new Thickness(0, 20, 120, 0);
                button2.Margin = new Thickness(0, 20, 120, 0);
                button3.Margin = new Thickness(0, 20, 120, 0);
                button4.Margin = new Thickness(0, 20, 120, 0);
                button5.Margin = new Thickness(0, 20, 120, 0);
                button6.Margin = new Thickness(0, 20, 120, 0);
                button7.Margin = new Thickness(0, 220, 120, 0);
                animation.From = posicaoAtual;
                animation.To = 200;
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            }
            isMenuExpanded = !isMenuExpanded;
            appBar_Esquerdo.BeginAnimation(FrameworkElement.WidthProperty, animation);
        }
    }
}
