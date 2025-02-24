using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.ComponentModel;
using RuralTech.Integracoes;
using System.Windows.Controls;

namespace RuralTech.Telas
{
    public partial class TelaMedicamento : Window, INotifyPropertyChanged
    {
        public bool Editar = false;
        private Medicamento _medicamento = new Medicamento(); // Objeto medicamento
        private MedicamentoDAO _medicamentoDAO = new MedicamentoDAO(); // Objeto responsável por acessar o banco de dados
        public ObservableCollection<Medicamento> MedicamentosList { get; set; }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FiltrarMedicamentos();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TelaMedicamento()
        {
            InitializeComponent();
            DataContext = this;
            MedicamentosList = new ObservableCollection<Medicamento>();
            CarregarMedicamentos();
            txt_usuario.Text = TelaLogin.usuarioLogado.Nome;

        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FiltrarMedicamentos()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                CarregarMedicamentos();
            }
            else
            {
                var medicamentosFiltrados = _medicamentoDAO.GetMedicamentos()
                    .Where(v => v.Nome != null && v.Nome.ToLower().Contains(SearchText.ToLower()))
                    .ToList();

                MedicamentosList.Clear();
                foreach (var medicamento in medicamentosFiltrados)
                {
                    MedicamentosList.Add(medicamento);
                }
            }
        }

        private void AutoSuggestBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarMedicamentos();
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

        private void CarregarMedicamentos()
        {
            try
            {
                var medicamentos = _medicamentoDAO.GetMedicamentos();
                MedicamentosList.Clear();
                foreach (var medicamento in medicamentos)
                {
                    MedicamentosList.Add(medicamento);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os Medicamentos: {ex.Message}");
            }
        }

        private void OpenModal(object sender, RoutedEventArgs e)
        {
            // Limpa o objeto e os campos quando adicionando um novo registro
            _medicamento = new Medicamento();
            Editar = false; // Indica que é um novo registro
            LimparCampos();
            PropertyPopup.IsOpen = true;
        }

        private void SaveProperty(object sender, RoutedEventArgs e)
        {
            try
            {
                // Preenche o objeto _medicamento com os valores do formulário
                _medicamento.Nome = txt_nome.Text;

                if (!int.TryParse(txt_dias.Text.Trim(), out int diasCarencia))
                {
                    MessageBox.Show("Por favor, insira um número válido para os dias de carência.");
                    return;
                }
                _medicamento.DiasCarencia = diasCarencia;
                _medicamento.Estado = CBestado.Text;
                _medicamento.InscricaoEstadual = txt_inscricao.Text;

                if (!int.TryParse(txt_quantidade.Text.Trim(), out int quantidade))
                {
                    MessageBox.Show("Por favor, insira um número válido para a quantidade.");
                    return;
                }
                _medicamento.Quantidade = quantidade;
                _medicamento.UnidadeEntrada = CBunidadeEntrada.Text;
                _medicamento.UnidadeSaida = CBunidadeSaida.Text;
                _medicamento.Observacao = txt_observacao.Text;

                if (Editar)
                {
                    _medicamentoDAO.Update(_medicamento);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _medicamento.Id = _medicamentoDAO.Insert(_medicamento);
                    MedicamentosList.Add(_medicamento);
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }

                CarregarMedicamentos();
                PropertyPopup.IsOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
        }

        private void CloseModal(object sender, RoutedEventArgs e)
        {
            PropertyPopup.IsOpen = false;
        }

        private void OpenModalEdit(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Medicamento medicamentoSelecionado)
            {
                _medicamento = medicamentoSelecionado;
                PreencherCamposComDados(_medicamento); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void LimparCampos()
        {
            txt_nome.Text = "";
            txt_dias.Text = "";
            CBestado.Text = "";
            txt_inscricao.Text = "";
            txt_quantidade.Text = "";
            CBunidadeEntrada.Text = "";
            CBunidadeSaida.Text = "";
            txt_observacao.Text = "";
        }

        private void PreencherCamposComDados(Medicamento medicamento)
        {
            txt_nome.Text = medicamento.Nome;
            txt_dias.Text = medicamento.DiasCarencia.ToString();
            CBestado.Text = medicamento.Estado;
            txt_inscricao.Text = medicamento.InscricaoEstadual;
            txt_quantidade.Text = medicamento.Quantidade.ToString();
            CBunidadeEntrada.Text = medicamento.UnidadeEntrada;
            CBunidadeSaida.Text = medicamento.UnidadeSaida;
            txt_observacao.Text = medicamento.Observacao;
        }

        private void DeleteMedicamento(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Medicamento medicamentoSelecionado)
            {
                if (medicamentoSelecionado == null)
                {
                    MessageBox.Show("Nenhum medicamento selecionado para exclusão.");
                    return;
                }

                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _medicamentoDAO.Delete(medicamentoSelecionado);
                        MedicamentosList.Remove(medicamentoSelecionado);
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
                MessageBox.Show("Nenhum medicamento selecionado.");
            }
        }
    }
}
