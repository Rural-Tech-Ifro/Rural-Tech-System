using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica interna para Propriedades.xaml
    /// </summary>
    public partial class TelaPropriedade : Window
    {
        private Propriedade _propriedade = new Propriedade();
        private PropriedadeDAO _propriedadeDAO = new PropriedadeDAO();
        public ObservableCollection<Propriedade> PropriedadesList { get; set; }
        public TelaPropriedade()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            PropriedadesList = new ObservableCollection<Propriedade>(); // Inicializa a lista como uma ObservableCollection
            CarregarPropriedades();
        }
        private void CarregarPropriedades()
        {
            try
            {
                var propriedades = _propriedadeDAO.GetPropriedade(); // Obtém a lista de vacinas do banco
                PropriedadesList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var propriedade in propriedades)
                {
                    PropriedadesList.Add(propriedade); // Adiciona cada vacina à ObservableCollection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar vacinas: {ex.Message}");
            }
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
            try
            {
                _propriedade.NomePropriedade = txt_propriedade.Text;
                _propriedade.NomeProprietario = txt_proprietario.Text;
                _propriedade.Tamanho = Convert.ToInt32(txt_tamanho.Text);
                _propriedade.CEP = txt_cep.Text;
                _propriedade.Logradouro = txt_logradouro.Text;
                _propriedade.Bairro = txt_bairro.Text;
                _propriedade.Complemento = txt_complemento.Text;

                _propriedadeDAO.Insert(_propriedade); // Insere no banco
                MessageBox.Show("Registro cadastrado com sucesso.");
                TelaPropriedade tela = new TelaPropriedade();
                this.Close();
                tela.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
            PropertyPopup.IsOpen = false;
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
            TelaPasto tela = new TelaPasto();
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
