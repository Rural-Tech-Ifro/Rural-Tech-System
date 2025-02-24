using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.ComponentModel;
using RuralTech.Integracoes;
using System.Windows.Controls;

namespace RuralTech.Telas
{
    public partial class TelaVacina : Window, INotifyPropertyChanged
    {
        private bool Editar = false;
        private Vacina _vacina = new Vacina();
        private VacinaDAO _vacinaDAO = new VacinaDAO();
        public ObservableCollection<Vacina> VacinasList { get; set; } = new ObservableCollection<Vacina>();
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FiltrarVacinas();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TelaVacina()
        {
            InitializeComponent();
            DataContext = this;
            CarregarVacinas();
            txt_usuario.Text = TelaLogin.usuarioLogado.Nome;

        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FiltrarVacinas()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                CarregarVacinas();
            }
            else
            {
                var vacinasFiltradas = _vacinaDAO.GetVacinas()
                    .Where(v => v.Nome != null && v.Nome.ToLower().Contains(SearchText.ToLower()))
                    .ToList();

                VacinasList.Clear();
                foreach (var vacina in vacinasFiltradas)
                {
                    VacinasList.Add(vacina);
                }
            }
        }

        private void AutoSuggestBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarVacinas();
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

        private void CarregarVacinas()
        {
            try
            {
                var vacinas = _vacinaDAO.GetVacinas();
                VacinasList.Clear();
                foreach (var vacina in vacinas)
                {
                    VacinasList.Add(vacina);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar vacinas: {ex.Message}");
            }
        }

        private void OpenModal(object sender, RoutedEventArgs e)
        {
            _vacina = new Vacina();
            Editar = false;
            LimparCampos();
            PropertyPopup.IsOpen = true;
        }

        private void SaveProperty(object sender, RoutedEventArgs e)
        {
            try
            {
                _vacina.Nome = txt_nome.Text;

                if (!int.TryParse(txt_dias.Text.Trim(), out int diasCarencia))
                {
                    MessageBox.Show("Por favor, insira um número válido para os dias de carência.");
                    return;
                }
                _vacina.DiasCarencia = diasCarencia;
                _vacina.Estado = CBestado.Text;
                _vacina.InscricaoEstadual = txt_inscricao.Text;

                if (!int.TryParse(txt_quantidade.Text.Trim(), out int quantidade))
                {
                    MessageBox.Show("Por favor, insira um número válido para a quantidade.");
                    return;
                }
                _vacina.Quantidade = quantidade;
                _vacina.UnidadeEntrada = CBunidadeEntrada.Text;
                _vacina.UnidadeSaida = CBunidadeSaida.Text;
                _vacina.Observacao = txt_observacao.Text;

                if (Editar)
                {
                    _vacinaDAO.Update(_vacina);
                    MessageBox.Show("Registro atualizado com sucesso.");
                }
                else
                {
                    _vacina.Id = _vacinaDAO.Insert(_vacina);
                    VacinasList.Add(_vacina);
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }

                CarregarVacinas();
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
            if (sender is FrameworkElement element && element.DataContext is Vacina vacinaSelecionada)
            {
                _vacina = vacinaSelecionada;
                PreencherCamposComDados(_vacina); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }



        private void PreencherCamposComDados(Vacina vacina)
        {
            txt_nome.Text = vacina.Nome;
            txt_dias.Text = vacina.DiasCarencia.ToString();
            CBestado.Text = vacina.Estado;
            txt_inscricao.Text = vacina.InscricaoEstadual;
            txt_quantidade.Text = vacina.Quantidade.ToString();
            CBunidadeEntrada.Text = vacina.UnidadeEntrada;
            CBunidadeSaida.Text = vacina.UnidadeSaida;
            txt_observacao.Text = vacina.Observacao;
        }

        private void LimparCampos()
        {
            txt_nome.Clear();
            txt_dias.Clear();
            CBestado.SelectedIndex = -1;
            txt_inscricao.Clear();
            txt_quantidade.Clear();
            CBunidadeEntrada.SelectedIndex = -1;
            CBunidadeSaida.SelectedIndex = -1;
            txt_observacao.Clear();
        }

        private void DeleteVacina(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Vacina vacinaSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _vacinaDAO.Delete(vacinaSelecionada);
                        VacinasList.Remove(vacinaSelecionada);
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
    }
}
