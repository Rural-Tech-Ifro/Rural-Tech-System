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
    /// Lógica interna para TelaInseminacao.xaml
    /// </summary>
    public partial class TelaInseminacao : Window
    {
        AnimalDAO animal = new AnimalDAO();
        FuncionarioDAO funcionario = new FuncionarioDAO();
        private Inseminacao _inseminacao = new Inseminacao();
        private InseminacaoDAO _inseminacaoDAO = new InseminacaoDAO();
        public ObservableCollection<Inseminacao> InseminacoesList { get; set; }

        public TelaInseminacao()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            InseminacoesList = new ObservableCollection<Inseminacao>(); // Inicializa a lista como uma ObservableCollection
            CarregarExames();
        }
        private void CarregarExames()
        {
            try
            {
                var inseminacoes = _inseminacaoDAO.GetInseminacoes(); // Obtém a lista de vacinas do banco
                InseminacoesList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var inseminacao in inseminacoes)
                {
                    InseminacoesList.Add(inseminacao); // Adiciona cada vacina à ObservableCollection
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
                foreach (Animals str in animal.GetAnimal())
                {
                    if (str.Brinco == combo_animal.Text)
                    {
                        _inseminacao.Animal = str.Id.ToString();

                    }
                }
                
                foreach (Funcionario str in funcionario.GetFuncionario())
                {
                    if (str.Nome == txt_funcionario.Text)
                    {
                        _inseminacao.Animal = str.Id.ToString();

                    }
                }
                // Atualiza o objeto _vacina com os valores do formulário
                _inseminacao.Tipo = txt_tipo.Text;
                _inseminacao.Observacao = txt_observacao.Text;
                _inseminacao.Data = Convert.ToDateTime(txt_data_inseminacao.Text);
                

                _inseminacaoDAO.Insert(_inseminacao); // Insere no banco
                MessageBox.Show("Registro cadastrado com sucesso.");
                TelaInseminacao tela = new TelaInseminacao();
                this.Close();
                tela.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
            PropertyPopup.IsOpen = false;
        }

        private void AutoSuggestBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
