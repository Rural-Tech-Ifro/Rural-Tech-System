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
    /// Lógica interna para Propriedades.xaml
    /// </summary>
    public partial class TelaPropriedade : Window
    {
        private Propriedade _propriedade = new Propriedade();
        private PropriedadeDAO _propriedadeDAO = new PropriedadeDAO();
        public bool Editar = false;

        public ObservableCollection<Propriedade> PropriedadesList { get; set; }
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
        public TelaPropriedade()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            PropriedadesList = new ObservableCollection<Propriedade>(); // Inicializa a lista como uma ObservableCollection
            CarregarPropriedades();
            txt_usuario.Text = TelaLogin.usuarioLogado.Nome;

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
            // Limpa o objeto e os campos quando adicionando um novo registro
            _propriedade = new Propriedade();
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
                _propriedade.NomePropriedade = txt_propriedade.Text;
                _propriedade.NomeProprietario = txt_proprietario.Text;
                _propriedade.Tamanho = Convert.ToInt32(txt_tamanho.Text);
                _propriedade.CEP = txt_cep.Text;
                _propriedade.Logradouro = txt_logradouro.Text;
                _propriedade.Bairro = txt_bairro.Text;
                _propriedade.Complemento = txt_complemento.Text;

                if (Editar)
                {
                    _propriedadeDAO.Update(_propriedade);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _propriedadeDAO.Insert(_propriedade); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
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
        private void OpenModalEdit(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Propriedade propriedadeSelecionado)
            {
                _propriedade = propriedadeSelecionado;
                PreencherCamposComDados(_propriedade); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Propriedade propriedade)
        {
            txt_propriedade.Text = propriedade.NomePropriedade;
            txt_proprietario.Text = propriedade.NomeProprietario;
            txt_tamanho.Text = propriedade.Tamanho.ToString();
            txt_cep.Text = propriedade.CEP;
            txt_logradouro.Text = propriedade.Logradouro;
            txt_bairro.Text = propriedade.Bairro;
            txt_complemento.Text = propriedade.Complemento;

            

        }
        private void LimparCampos()
        {
            txt_propriedade.Text = "";
            txt_proprietario.Text = "";
            txt_tamanho.Text = "";
            txt_cep.Text = "";
            txt_logradouro.Text = "";
            txt_bairro.Text = "";
            txt_complemento.Text = "";
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Propriedade propriedadeSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _propriedadeDAO.Delete(propriedadeSelecionada);
                        PropriedadesList.Remove(propriedadeSelecionada);
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
                MessageBox.Show("Nenhuma propriedade selecionada.");
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
                CarregarPropriedades();
            }
            else
            {
                var propriedadesFiltradas = _propriedadeDAO.GetPropriedade()
                    .Where(v => v.NomePropriedade != null && v.NomePropriedade.ToLower().Contains(SearchText.ToLower()))
                    .ToList();

                PropriedadesList.Clear();
                foreach (var propriedade in propriedadesFiltradas)
                {
                    PropriedadesList.Add(propriedade);
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
