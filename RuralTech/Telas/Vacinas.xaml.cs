using System;
using System.Collections.Generic;
using System.IO;
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
using RuralTech.Integracoes;

namespace RuralTech.Telas
{
    /// <summary>
    /// Lógica interna para Vacinas.xaml
    /// </summary>
    public partial class Vacinas : Window
    {
        private Vacina _vacina = new Vacina();
        public Vacinas()
        {
            InitializeComponent();
        }

        public Vacinas(Vacina vacina)
        {
            InitializeComponent();
            _vacina = vacina;
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
            _vacina.Nome = txt_nome.Text;
            // Inicializa uma variável para armazenar o número de dias
            int diasCarencia = _vacina.DiasCarencia;

            // Tenta converter o valor do TextBox para int
            bool conversaoBemSucedida = int.TryParse(txt_dias.Text.Trim(), out diasCarencia);

            if (!conversaoBemSucedida)
            {
                // Exibe uma mensagem de erro se a conversão falhar
                MessageBox.Show("Por favor, insira um número válido para os dias de carência.");
                return; // Interrompe a execução caso o valor não seja válido
            }

            // Agora você pode usar a variável 'diasCarencia' para inserir no banco de dados

            _vacina.Estado = CBestado.Text;
            _vacina.InscricaoEstadual = txt_inscricao.Text;
            // Inicializa uma variável para armazenar a quantidade
            int quantidade = _vacina.Quantidade;

            // Tenta converter o valor do TextBox para int
            bool conversaobemSucedida = int.TryParse(txt_quantidade.Text.Trim(), out quantidade);

            if (!conversaobemSucedida)
            {
                // Exibe uma mensagem de erro se a conversão falhar
                MessageBox.Show("Por favor, insira um número válido para a quantidade.");
                return; // Interrompe a execução caso o valor não seja válido
            }

            // Agora você pode usar a variável 'quantidade' para inserir no banco de dados

            _vacina.UnidadeEntrada = CBunidadeEntrada.Text;
            _vacina.UnidadeSaida = CBunidadeSaida.Text;
            _vacina.Observacao = txt_observacao.Text;

            try
            {
                var dao = new VacinaDAO();

                if (_vacina.Id <= 0)
                {
                    dao.Insert(_vacina);
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                else
                {
                    MessageBox.Show("Registro não cadastrado com sucesso.");
                }

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Fechar o modal após salvar
            PropertyPopup.IsOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Animal tela = new Animal();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Equipamentos tela = new Equipamentos();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Medicamentos tela = new Medicamentos();
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
            Propriedades tela = new Propriedades();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Vacinas tela = new Vacinas();
            this.Close();
            tela.ShowDialog();
        }

        private void ImageDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

    }
}
