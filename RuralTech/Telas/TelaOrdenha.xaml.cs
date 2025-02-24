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
    /// Lógica interna para Ordenha.xaml
    /// </summary>
    public partial class TelaOrdenha : Window
    {
        AnimalDAO animal = new AnimalDAO();
        FuncionarioDAO funcionario = new FuncionarioDAO();
        private Ordenha _ordenha = new Ordenha();
        private OrdenhaDAO _ordenhaDAO = new OrdenhaDAO();
        public bool Editar = false;

        public ObservableCollection<Ordenha> OrdenhasList { get; set; }
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

        public TelaOrdenha()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            OrdenhasList = new ObservableCollection<Ordenha>(); // Inicializa a lista como uma ObservableCollection
            CarregarOrdenhas();
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
        private void CarregarOrdenhas()
        {
            try
            {
                var ordenhas = _ordenhaDAO.GetOrdenhas(); // Obtém a lista de vacinas do banco
                OrdenhasList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var ordenha in ordenhas)
                {
                    OrdenhasList.Add(ordenha); // Adiciona cada vacina à ObservableCollection
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
            _ordenha = new Ordenha();
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
                        _ordenha.Animal = str.Id.ToString();

                    }
                }
                
                
                foreach (Funcionario str in funcionario.GetFuncionario())
                {
                    if (str.Nome == combo_funcionario.Text)
                    {
                        _ordenha.Funcionario = str.Id.ToString();

                    }
                }
                // Atualiza o objeto _vacina com os valores do formulário
                _ordenha.TotalLitros = Convert.ToInt32(txt_total_litros.Text);


                if (Editar)
                {
                    _ordenhaDAO.Update(_ordenha);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _ordenhaDAO.Insert(_ordenha); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                TelaOrdenha tela = new TelaOrdenha();
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
            if (sender is FrameworkElement element && element.DataContext is Ordenha ordenhaSelecionado)
            {
                _ordenha = ordenhaSelecionado;
                PreencherCamposComDados(_ordenha); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Ordenha ordenha)
        {
            txt_total_litros.Text = ordenha.TotalLitros.ToString();
            combo_funcionario.Text = ordenha.Funcionario;
            combo_animal.Text = ordenha.Animal;


        }
        private void LimparCampos()
        {
            txt_total_litros.Text = "";
            combo_funcionario.Text = "";
            combo_animal.Text = "";
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Ordenha ordenhaSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _ordenhaDAO.Delete(ordenhaSelecionada);
                        OrdenhasList.Remove(ordenhaSelecionada);
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
                MessageBox.Show("Nenhuma ordenha selecionada.");
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
                CarregarOrdenhas();
            }
            else
            {
                var ordenhasFiltradas = _ordenhaDAO.GetOrdenhas()
                    .Where(v => v.Animal != null && v.Animal.ToLower().Contains(SearchText.ToLower()))
                    .ToList();

                OrdenhasList.Clear();
                foreach (var ordenha in ordenhasFiltradas)
                {
                    OrdenhasList.Add(ordenha);
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
