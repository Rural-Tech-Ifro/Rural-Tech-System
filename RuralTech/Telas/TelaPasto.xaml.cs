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
    /// Lógica interna para Pastos.xaml
    /// </summary>
    public partial class TelaPasto : Window
    {
        PropriedadeDAO propriedade = new PropriedadeDAO();

        private Pasto _pasto = new Pasto();
        private PastoDAO _pastoDAO = new PastoDAO();
        public ObservableCollection<Pasto> PastosList { get; set; }
        public TelaPasto()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            PastosList = new ObservableCollection<Pasto>(); // Inicializa a lista como uma ObservableCollection
            CarregarPastos();

            //COMBO BOX PROPRIEDADE

            foreach (Propriedade str in propriedade.GetPropriedade())
            {
                combo_propriedade.Items.Add(str.NomePropriedade);
            }
        }
        private void CarregarPastos()
        {
            try
            {
                var pastos = _pastoDAO.GetPasto(); // Obtém a lista de vacinas do banco
                PastosList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var animal in pastos)
                {
                    PastosList.Add(animal); // Adiciona cada vacina à ObservableCollection
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
                foreach (Propriedade str in propriedade.GetPropriedade())
                {
                    if (str.NomePropriedade == combo_propriedade.Text)
                    {
                        _pasto.Propriedade = str.Id.ToString();

                    }
                }
                _pasto.Tamanho = Convert.ToInt32(txt_tamanho.Text);
                _pasto.Limite = Convert.ToInt32(txt_limite.Text);
                _pasto.Descricao = txt_descricao.Text;
                _pasto.TipoPastagem = txt_tipoPastagem.Text;

                _pastoDAO.Insert(_pasto); // Insere no banco
                MessageBox.Show("Registro cadastrado com sucesso.");
                TelaPasto tela = new TelaPasto();
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

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}
