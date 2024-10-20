using System;
using System.Collections.ObjectModel; // Para usar ObservableCollection
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using RuralTech.Integracoes;

namespace RuralTech.Telas
{
    public partial class TelaVacina : Window
    {
        private Vacina _vacina = new Vacina(); // Objeto vacina
        private VacinaDAO _vacinaDAO = new VacinaDAO(); // Objeto responsável por acessar o banco de dados
        public ObservableCollection<Vacina> VacinasList { get; set; } // Usamos ObservableCollection para ligar ao ItemsControl

        public TelaVacina()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            VacinasList = new ObservableCollection<Vacina>(); // Inicializa a lista como uma ObservableCollection
            CarregarVacinas(); // Chama o método para carregar as vacinas do banco de dados ao iniciar a janela
        }

        // Método para carregar vacinas da base de dados
        private void CarregarVacinas()
        {
            try
            {
                var vacinas = _vacinaDAO.GetVacinas(); // Obtém a lista de vacinas do banco
                VacinasList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var vacina in vacinas)
                {
                    VacinasList.Add(vacina); // Adiciona cada vacina à ObservableCollection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar vacinas: {ex.Message}");
            }
        }

        // Salva as alterações (inserção ou edição) ao clicar no botão de salvar
        private void SaveProperty(object sender, RoutedEventArgs e)
        {
            try
            {
                // Atualiza o objeto _vacina com os valores do formulário
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

                if (_vacina.Id <= 0) // Se for um novo registro
                {
                    _vacina.Id = _vacinaDAO.Insert(_vacina); // Pega o Id fornecido do Banco e insere no banco
                    VacinasList.Add(_vacina); // Adiciona na coleção para atualização instantânea
                    MessageBox.Show("Registro cadastrado com sucesso. " + _vacina.Id);
                }
                else
                {
                    // Lógica para edição (caso implementada no futuro)
                    MessageBox.Show("Registro não cadastrado com sucesso.");
                }

                // Fecha o modal
                PropertyPopup.IsOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
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
        private void OpenModal(object sender, RoutedEventArgs e)
        {
            PropertyPopup.IsOpen = true;
        }

        // Fecha o modal
        private void CloseModal(object sender, RoutedEventArgs e)
        {
            PropertyPopup.IsOpen = false;
        }
    }
}
