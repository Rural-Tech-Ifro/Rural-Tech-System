using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using RuralTech.Integracoes;

namespace RuralTech.Telas
{
    public partial class TelaVacina : Window
    {
        public bool Editar = false;
        private bool isEditMode = false;
        private int editingVacinaId; // ID da vacina sendo editada
        private Vacina _vacina = new Vacina(); // Objeto vacina
        private VacinaDAO _vacinaDAO = new VacinaDAO(); // Objeto responsável por acessar o banco de dados
        public ObservableCollection<Vacina> VacinasList { get; set; }

        public TelaVacina()
        {
            InitializeComponent();
            DataContext = this;
            VacinasList = new ObservableCollection<Vacina>();
            CarregarVacinas();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TelaAnimal tela = new TelaAnimal();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TelaEquipamento tela = new TelaEquipamento();
            this.Close();
            tela.ShowDialog();
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
            // Se não for uma vacina selecionada, inicializa para novo cadastro
            if (_vacina.Id == 0)
            {
                _vacina = new Vacina();
            }

            PreencherCamposComDados(_vacina);
            PropertyPopup.IsOpen = true;
        }

        // Evento para salvar ou atualizar a vacina
        private void SaveProperty(object sender, RoutedEventArgs e)
        {
            try
            {
                // Preenche o objeto _vacina com os valores do formulário
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

                // Verifica se é uma atualização (Id > 0) ou um novo registro
                if (Editar == true)
                {
                    _vacinaDAO.Update(_vacina);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false;
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