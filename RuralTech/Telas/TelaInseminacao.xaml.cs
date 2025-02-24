using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public bool Editar = false;

        public ObservableCollection<Inseminacao> InseminacoesList { get; set; }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                Filtrar();
            }
        }

        public TelaInseminacao()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            InseminacoesList = new ObservableCollection<Inseminacao>(); // Inicializa a lista como uma ObservableCollection
            CarregarInseminacoes();
            txt_usuario.Text = TelaLogin.usuarioLogado.Nome;




            //COMBO BOX
            foreach (Funcionario str in funcionario.GetFuncionario())
            {
                combo_funcionario.Items.Add(str.Nome);
            }
            foreach (Animals str in animal.GetAnimal())
            {
                combo_animal.Items.Add(str.Brinco);
            }
        }
        private void CarregarInseminacoes()
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
            // Limpa o objeto e os campos quando adicionando um novo registro
            _inseminacao = new Inseminacao();
            Editar = false; // Indica que é um novo registro
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
                        _inseminacao.Animal = str.Id.ToString();

                    }
                }
                
                foreach (Funcionario str in funcionario.GetFuncionario())
                {
                    if (str.Nome == combo_funcionario.Text)
                    {
                        _inseminacao.Funcionario = str.Id.ToString();

                    }
                }
                // Atualiza o objeto _vacina com os valores do formulário
                _inseminacao.Tipo = txt_tipo.Text;
                _inseminacao.Observacao = txt_observacao.Text;
                _inseminacao.Data = Convert.ToDateTime(txt_data_inseminacao.Text);


                if (Editar)
                {
                    _inseminacaoDAO.Update(_inseminacao);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _inseminacaoDAO.Insert(_inseminacao); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }

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

        private void OpenModalEdit(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Inseminacao inseminacaoSelecionado)
            {
                _inseminacao = inseminacaoSelecionado;
                PreencherCamposComDados(_inseminacao); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Inseminacao inseminacao)
        {
            txt_tipo.Text = inseminacao.Tipo;
            txt_observacao.Text = inseminacao.Observacao;
            txt_data_inseminacao.Text = inseminacao.Data.ToString();
            combo_animal.Text = inseminacao.Animal;
            combo_funcionario.Text = inseminacao.Funcionario;


        }
        private void LimparCampos()
        {
            txt_tipo.Text = "";
            txt_observacao.Text = "";
            txt_data_inseminacao.Text = "";
            combo_animal.Text = "";
            combo_funcionario.Text = "";
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Inseminacao inseminacaoSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _inseminacaoDAO.Delete(inseminacaoSelecionada);
                        InseminacoesList.Remove(inseminacaoSelecionada);
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
                MessageBox.Show("Nenhuma inseminação selecionada.");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Filtrar()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                CarregarInseminacoes();
            }
            else
            {
                var inseminacoesFiltradas = _inseminacaoDAO.GetInseminacoes()
                    .Where(v => v.Animal != null && v.Animal.ToLower().Contains(SearchText.ToLower()))
                    .ToList();

                InseminacoesList.Clear();
                foreach (var inseminacao in inseminacoesFiltradas)
                {
                    InseminacoesList.Add(inseminacao);
                }
            }
        }

        private void AutoSuggestBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtrar();
        }
        private void Button_Compra(object sender, RoutedEventArgs e)
        {
            TelaCompra tela = new TelaCompra();
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
