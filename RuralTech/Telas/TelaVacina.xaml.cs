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
        public bool Editar = false;
        private bool isEditMode = false;
        private int editingVacinaId;
        private Vacina _vacina = new Vacina();
        private VacinaDAO _vacinaDAO = new VacinaDAO();
        public ObservableCollection<Vacina> VacinasList { get; set; }
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
            VacinasList = new ObservableCollection<Vacina>();
            CarregarVacinas();
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TelaAnimal tela = new TelaAnimal();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TelaMedicamento tela = new TelaMedicamento();
            tela.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TelaMedicamento tela = new TelaMedicamento();
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
            TelaPropriedade tela = new TelaPropriedade();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            TelaVacina tela = new TelaVacina();
            this.Close();
            tela.ShowDialog();
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
            if (!Editar) // Se não for edição, limpa o formulário para novo cadastro
            {
                _vacina = new Vacina();
            }

            PreencherCamposComDados(_vacina); // Carrega os dados no formulário
            PropertyPopup.IsOpen = true;
        }


        // Evento para salvar ou atualizar a vacina
        private void SaveProperty(object sender, RoutedEventArgs e)
        {
            try
            {
                // ... código para salvar os dados ...

                if (Editar)
                {
                    _vacinaDAO.Update(_vacina);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Reseta o modo de edição após atualizar
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


        // Método para abrir o modal para edição




        private void CloseModal(object sender, RoutedEventArgs e)
        {
            PropertyPopup.IsOpen = false;
        }

        private void PackIcon_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Vacina vacinaSelecionada)
            {
                _vacina = vacinaSelecionada;
                PreencherCamposComDados(_vacina); // Preenche o formulário com os dados para edição
                Editar = true; // Define o modo de edição
                PropertyPopup.IsOpen = true;
            }
        }




        private void PreencherCamposComDados(Vacina vacina)
        {
            if (vacina.Id == 0) // Verifica se é uma nova vacina
            {
                txt_nome.Text = string.Empty;
                txt_dias.Text = string.Empty;
                CBestado.Text = string.Empty;
                txt_inscricao.Text = string.Empty;
                txt_quantidade.Text = string.Empty;
                CBunidadeEntrada.Text = string.Empty;
                CBunidadeSaida.Text = string.Empty;
                txt_observacao.Text = string.Empty;
            }
            else // Caso contrário, preenche os dados da vacina para edição
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
        }


        private void DeleteVacina(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Vacina vacinaSelecionada)
            {
                // Verifica se a vacina selecionada é válida para exclusão
                if (vacinaSelecionada == null)
                {
                    MessageBox.Show("Nenhuma vacina selecionada para exclusão.");
                    return;
                }

                // Exibir uma mensagem de confirmação antes de excluir
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Exclui a vacina do banco de dados passando o objeto vacina
                        _vacinaDAO.Delete(vacinaSelecionada);

                        // Remove a vacina da lista em exibição
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