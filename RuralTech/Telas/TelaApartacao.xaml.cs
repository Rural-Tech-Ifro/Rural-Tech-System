using RuralTech.Integracoes;
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
    /// Lógica interna para TelaApartacao.xaml
    /// </summary>
    public partial class TelaApartacao : Window
    {
        AnimalDAO animal = new AnimalDAO();
        private Apartacao _apartacao = new Apartacao();
        private ApartacaoDAO _apartacaoDAO = new ApartacaoDAO();
        public bool Editar = false;
        public ObservableCollection<Apartacao> ApartacoesList { get; set; }

        public TelaApartacao()
        {
            InitializeComponent();
            DataContext = this; 
            ApartacoesList = new ObservableCollection<Apartacao>();
            CarregarApartacoes();

            //COMBO BOX animal

            foreach (Animals str in animal.GetAnimal())
            {
                combo_animal.Items.Add(str.Brinco);
            }
        }
        private void CarregarApartacoes()
        {
            try
            {
                var apartacoes = _apartacaoDAO.GetApartacoes(); // Obtém a lista de vacinas do banco
                ApartacoesList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var apartacao in apartacoes)
                {
                    ApartacoesList.Add(apartacao); // Adiciona cada vacina à ObservableCollection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar apartações: {ex.Message}");
            }
        }

        private void OpenModal(object sender, RoutedEventArgs e)
        {
            Editar = false;
            LimparCampos();
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
                foreach (Animals str in animal.GetAnimal())
                {
                    if (str.Brinco == combo_animal.Text)
                    {
                        _apartacao.Animal = str.Id.ToString();

                    }
                }
                _apartacao.Situacao = combo_situacao.Text;
                _apartacao.Lote = combo_lote.Text;
                _apartacao.DataTransferencia = Convert.ToDateTime(txt_dataTransferencia.Text);
                _apartacao.Observacao = txt_observacao.Text;

                if (Editar)
                {
                    _apartacaoDAO.Update(_apartacao);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    TelaApartacao tela = new TelaApartacao();
                    this.Close();
                    tela.ShowDialog();
                }
                else
                {
                    _apartacaoDAO.Insert(_apartacao); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                    TelaApartacao tela = new TelaApartacao();
                    this.Close();
                    tela.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
            PropertyPopup.IsOpen = false;
        }


        private void OpenModalEdit(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Apartacao apartacaoSelecionada)
            {
                _apartacao = apartacaoSelecionada;
                combo_animal.Text = _apartacao.Animal;
                combo_situacao.Text = _apartacao.Situacao;
                combo_lote.Text = _apartacao.Lote;
                txt_dataTransferencia.Text = _apartacao.DataTransferencia.ToString();
                txt_observacao.Text = _apartacao.Observacao; 
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }

        private void DeleteVacina(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Apartacao apartacaoSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _apartacaoDAO.Delete(apartacaoSelecionada);
                        ApartacoesList.Remove(apartacaoSelecionada);
                        MessageBox.Show("Registro deletado com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao deletar registro: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Nenhuma vacina selecionada.");
            }
        }
        private void LimparCampos()
        {
            combo_animal.Text = null;
            combo_situacao.Text = null;
            combo_lote.Text = null;
            txt_dataTransferencia.Clear();
            txt_observacao.Clear();
        }

        private void Button_Compra(object sender, RoutedEventArgs e)
        {
            TelaCompra tela = new TelaCompra();
            tela.Show();
            this.Close();
        }

        private void Button_Despesa(object sender, RoutedEventArgs e)
        {
            TelaDespesa tela = new TelaDespesa();
            tela.Show();
            this.Close();
        }

        private void Button_Equipamento(object sender, RoutedEventArgs e)
        {
            TelaEquipamento tela = new TelaEquipamento();
            tela.Show();
            this.Close();
        }

        private void Button_Exame(object sender, RoutedEventArgs e)
        {
            TelaExame tela = new TelaExame();
            tela.Show();
            this.Close();
        }

        private void Button_Fornecedor(object sender, RoutedEventArgs e)
        {
            TelaFornecedor tela = new TelaFornecedor();
            tela.Show();
            this.Close();
        }

        private void Button_Funcionario(object sender, RoutedEventArgs e)
        {
            TelaFuncionario tela = new TelaFuncionario();
            tela.Show();
            this.Close();
        }

        private void Button_Inseminacao(object sender, RoutedEventArgs e)
        {
            TelaInseminacao tela = new TelaInseminacao();
            tela.Show();
            this.Close();
        }

        private void Button_Medicamento(object sender, RoutedEventArgs e)
        {
            TelaMedicamento tela = new TelaMedicamento();
            tela.Show();
            this.Close();
        }

        private void Button_Ordenha(object sender, RoutedEventArgs e)
        {
            TelaOrdenha tela = new TelaOrdenha();
            tela.Show();
            this.Close();
        }

        private void Button_Parto(object sender, RoutedEventArgs e)
        {
            TelaParicao tela = new TelaParicao();
            tela.Show();
            this.Close();
        }

        private void Button_Pasto(object sender, RoutedEventArgs e)
        {
            TelaPasto tela = new TelaPasto();
            tela.Show();
            this.Close();
        }

        private void Button_Patrimonio(object sender, RoutedEventArgs e)
        {
            TelaPatrimonio tela = new TelaPatrimonio();
            tela.Show();
            this.Close();
        }

        private void Button_Produto(object sender, RoutedEventArgs e)
        {
            TelaProduto tela = new TelaProduto();
            tela.Show();
            this.Close();
        }

        private void Button_Propriedade(object sender, RoutedEventArgs e)
        {
            TelaPropriedade tela = new TelaPropriedade();
            tela.Show();
            this.Close();
        }

        private void Button_Transporte(object sender, RoutedEventArgs e)
        {
            TelaTransportador tela = new TelaTransportador();
            tela.Show();
            this.Close();
        }

        private void Button_Vacina(object sender, RoutedEventArgs e)
        {
            TelaVacina tela = new TelaVacina();
            tela.Show();
            this.Close();
        }

        private void Button_Animal(object sender, RoutedEventArgs e)
        {
            TelaAnimal tela = new TelaAnimal();
            tela.Show();
            this.Close();
        }
    }
}
