using RuralTech.Integracoes;
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
    /// Lógica interna para Funcionario.xaml
    /// </summary>
    public partial class TelaFuncionario : Window
    {
        private Funcionario _funcionario = new Funcionario();
        private FuncionarioDAO _funcionarioDAO = new FuncionarioDAO();
        public ObservableCollection<Funcionario> FuncionariosList { get; set; }
        public TelaFuncionario()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            FuncionariosList = new ObservableCollection<Funcionario>(); // Inicializa a lista como uma ObservableCollection
            CarregarFuncionarios();
        }

        private void CarregarFuncionarios()
        {
            try
            {
                var funcionarios = _funcionarioDAO.GetFuncionario(); // Obtém a lista de vacinas do banco
                FuncionariosList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var funcionario in funcionarios)
                {
                    FuncionariosList.Add(funcionario); // Adiciona cada vacina à ObservableCollection
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
                _funcionario.Nome = txt_nome.Text;
                _funcionario.Email = txt_email.Text;
                _funcionario.Celular = txt_celular.Text;
                _funcionario.Telefone = txt_telefone.Text;
                _funcionario.CPF = txt_cpf.Text;
                _funcionario.DataNascimento = Convert.ToDateTime(txt_dataNascimento.Text);
                _funcionario.DataAdmissao = Convert.ToDateTime(txt_dataAdmissao.Text);
                _funcionario.DataPagamento = Convert.ToDateTime(txt_dataPagamento.Text);

                _funcionario.Logradouro = txt_logradouro.Text;
                _funcionario.Numero = txt_numero.Text;
                _funcionario.Estado = txt_estado.Text;
                _funcionario.Cidade = txt_cidade.Text;
                _funcionario.Pais = txt_pais.Text;
                _funcionario.CEP = txt_cep.Text;

                _funcionarioDAO.Insert(_funcionario); // Insere no banco
                MessageBox.Show("Registro cadastrado com sucesso.");

                TelaFuncionario tela = new TelaFuncionario();
                this.Close();
                tela.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
            PropertyPopup.IsOpen = false;
        }

        /*private void PackIcon_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Funcionario funcionarioSelecionado)
            {
                _funcionario = funcionarioSelecionado;
                PreencherCamposComDados(_funcionario); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }



        private void PreencherCamposComDados(Medicamento medicamento)
        {
            txt_nome.Text = medicamento.Nome;
            txt_email.Text = medicamento.DiasCarencia.ToString();
            .Text = medicamento.Estado;
            txt_inscricao.Text = medicamento.InscricaoEstadual;
            txt_quantidade.Text = medicamento.Quantidade.ToString();
            CBunidadeEntrada.Text = medicamento.UnidadeEntrada;
            CBunidadeSaida.Text = medicamento.UnidadeSaida;
            txt_observacao.Text = medicamento.Observacao;
        }

        private void DeleteMedicamento(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Medicamento medicamentoSelecionado)
            {
                // Verifica se a medicamento selecionada é válida para exclusão
                if (medicamentoSelecionado == null)
                {
                    MessageBox.Show("Nenhum medicamento selecionada para exclusão.");
                    return;
                }

                // Exibir uma mensagem de confirmação antes de excluir
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Exclui a medicamento do banco de dados passando o objeto medicamento
                        _medicamentoDAO.Delete(medicamentoSelecionado);

                        // Remove a medicamento da lista em exibição
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
        }*/
    }
}