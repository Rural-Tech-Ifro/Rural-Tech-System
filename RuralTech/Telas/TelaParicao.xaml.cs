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
    /// Lógica interna para TelaParicao.xaml
    /// </summary>
    public partial class TelaParicao : Window
    {
        private Paricao _paricao = new Paricao();
        private ParicaoDAO _paricaoDAO = new ParicaoDAO();
        public bool Editar = false;

        public ObservableCollection<Paricao> ParicoesList { get; set; }
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

        public TelaParicao()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            ParicoesList = new ObservableCollection<Paricao>(); // Inicializa a lista como uma ObservableCollection
            CarregarParicoes();
            txt_usuario.Text = TelaLogin.usuarioLogado.Nome;



        }
        private void CarregarParicoes()
        {
            try
            {
                var paricoes = _paricaoDAO.GetParicoes(); // Obtém a lista de vacinas do banco
                ParicoesList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var paricao in paricoes)
                {
                    ParicoesList.Add(paricao); // Adiciona cada vacina à ObservableCollection
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
            _paricao = new Paricao();
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
                
                // Atualiza o objeto _vacina com os valores do formulário
                _paricao.DataParto = Convert.ToDateTime(txt_data.Text);
                _paricao.Sexo = combo_sexo.Text;
                _paricao.Tipo = txt_tipo.Text;
                _paricao.Lote = txt_lote.Text;
                _paricao.Detalhamento = txt_detalhamento.Text;
                _paricao.Situacao = txt_situacao.Text;


                if (Editar)
                {
                    _paricaoDAO.Update(_paricao);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _paricaoDAO.Insert(_paricao); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                TelaParicao tela = new TelaParicao();
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
            if (sender is FrameworkElement element && element.DataContext is Paricao paricaoSelecionado)
            {
                _paricao = paricaoSelecionado;
                PreencherCamposComDados(_paricao); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Paricao paricao)
        {
            txt_data.Text = paricao.DataParto.ToString();
            combo_sexo.Text = paricao.Sexo.ToString();
            txt_tipo.Text = paricao.Tipo.ToString();
            txt_lote.Text = paricao.Lote.ToString();
            txt_detalhamento.Text = paricao.Detalhamento.ToString();
            txt_situacao.Text = paricao.Situacao.ToString();
        }
        private void LimparCampos()
        {
            txt_data.Text = "";
            combo_sexo.Text = "";
            txt_tipo.Text = "";
            txt_lote.Text = "";
            txt_detalhamento.Text = "";
            txt_situacao.Text = "";
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Paricao paricaoSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _paricaoDAO.Delete(paricaoSelecionada);
                        ParicoesList.Remove(paricaoSelecionada);
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
                MessageBox.Show("Nenhuma parição selecionada.");
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
                CarregarParicoes();
            }
            else
            {
                var paricoesFiltradas = _paricaoDAO.GetParicoes()
                    .Where(v => v.Tipo != null && v.Tipo.ToLower().Contains(SearchText.ToLower()))
                    .ToList();

                ParicoesList.Clear();
                foreach (var paricao in paricoesFiltradas)
                {
                    ParicoesList.Add(paricao);
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
