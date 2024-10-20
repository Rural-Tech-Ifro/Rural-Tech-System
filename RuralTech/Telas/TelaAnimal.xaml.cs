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
using RuralTech.Integracoes;
using System.Collections.ObjectModel;


namespace RuralTech.Telas
{
    /// <summary>
    /// Lógica interna para Animal.xaml
    /// </summary>
    public partial class TelaAnimal : Window
    {
        private Animals _animal = new Animals();
        private AnimalDAO _animalDAO = new AnimalDAO();
        public ObservableCollection<Animals> AnimaisList { get; set; }



        public TelaAnimal()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            AnimaisList = new ObservableCollection<Animals>(); // Inicializa a lista como uma ObservableCollection
            CarregarVacinas();
        }
        private void CarregarVacinas()
        {
            try
            {
                var animais = _animalDAO.GetAnimal(); // Obtém a lista de vacinas do banco
                AnimaisList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var animal in animais)
                {
                    AnimaisList.Add(animal); // Adiciona cada vacina à ObservableCollection
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
                // Atualiza o objeto _vacina com os valores do formulário
                _animal.Brinco = txt_brinco.Text;
                _animal.Raca = combo_raca.Text;
                _animal.Classificacao = txt_classificacao.Text;
                _animal.Sexo = combo_sex.Text;
                _animal.Origem = combo_origem.Text;

                _animalDAO.Insert(_animal); // Insere no banco
                MessageBox.Show("Registro cadastrado com sucesso.");
                TelaAnimal tela = new TelaAnimal();
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
            TelaEquipamento tela = new TelaEquipamento();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TelaMedicamento tela = new TelaMedicamento();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TelaPropriedade tela = new TelaPropriedade();
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
