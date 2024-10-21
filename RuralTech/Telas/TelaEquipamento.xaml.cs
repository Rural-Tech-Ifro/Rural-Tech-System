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
    /// Lógica interna para Equipamentos.xaml
    /// </summary>
    public partial class TelaEquipamento : Window
    {
        PropriedadeDAO propriedade = new PropriedadeDAO();

        private Equipamento _equipamento = new Equipamento();
        private EquipamentoDAO _equipamentoDAO = new EquipamentoDAO();
        public ObservableCollection<Equipamento> EquipamentosList { get; set; }
        public TelaEquipamento()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            EquipamentosList = new ObservableCollection<Equipamento>(); // Inicializa a lista como uma ObservableCollection
            CarregarEquipamentos();

            //COMBO BOX PROPRIEDADE

            foreach (Propriedade str in propriedade.GetPropriedade())
            {
                combo_propriedade.Items.Add(str.NomePropriedade);
            }
        }

        private void CarregarEquipamentos()
        {
            try
            {
                var equipamentos = _equipamentoDAO.GetEquipamento(); // Obtém a lista de vacinas do banco
                EquipamentosList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var equipamento in equipamentos)
                {
                    EquipamentosList.Add(equipamento); // Adiciona cada vacina à ObservableCollection
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
                        _equipamento.Propriedade = str.Id.ToString();

                    }
                }
                _equipamento.Nome = txt_nome.Text;
                _equipamento.Valor = Convert.ToDouble(txt_valor.Text);
                _equipamento.Descricao = txt_descricao.Text;
                _equipamento.Tipo = txt_tipo.Text;

                _equipamentoDAO.Insert(_equipamento); // Insere no banco
                MessageBox.Show("Registro cadastrado com sucesso.");
                TelaEquipamento tela = new TelaEquipamento();
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
