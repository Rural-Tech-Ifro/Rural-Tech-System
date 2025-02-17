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
        public bool Editar = false;

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
            // Limpa o objeto e os campos quando adicionando um novo registro
            _funcionario = new Funcionario();
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

                if (Editar)
                {
                    _funcionarioDAO.Update(_funcionario);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _funcionarioDAO.Insert(_funcionario); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }

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

        private void OpenModalEdit(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Funcionario funcionarioSelecionado)
            {
                _funcionario = funcionarioSelecionado;
                PreencherCamposComDados(_funcionario); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Funcionario funcionario)
        {
            txt_nome.Text = funcionario.Nome;
            txt_email.Text = funcionario.Email;
            txt_telefone.Text = funcionario.Telefone;
            txt_numero.Text = funcionario.Numero;
            txt_salario.Text = funcionario.Salario.ToString();
            txt_celular.Text = funcionario.Celular;
            txt_logradouro.Text = funcionario.Logradouro;
            txt_pais.Text = funcionario.Pais;
            txt_estado.Text = funcionario.Estado;
            txt_cidade.Text = funcionario.Cidade;
            txt_cep.Text = funcionario.CEP;
            txt_cpf.Text = funcionario.CPF;
            txt_cpf.Text = funcionario.CPF;
            txt_dataNascimento.Text = funcionario.DataNascimento.ToString();
            txt_dataPagamento.Text = funcionario.DataPagamento.ToString();
            txt_dataAdmissao.Text = funcionario.DataAdmissao.ToString();


        }
        private void LimparCampos()
        {
            txt_nome.Text = "";
            txt_email.Text = "";
            txt_telefone.Text = "";
            txt_numero.Text = "";
            txt_salario.Text = "";
            txt_celular.Text = "";
            txt_logradouro.Text = "";
            txt_pais.Text = "";
            txt_estado.Text = "";
            txt_cidade.Text = "";
            txt_cep.Text = "";
            txt_cpf.Text = "";
            txt_cpf.Text = "";
            txt_dataNascimento.Text = "";
            txt_dataPagamento.Text = "";
            txt_dataAdmissao.Text = "";

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Funcionario funcionarioSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _funcionarioDAO.Delete(funcionarioSelecionada);
                        FuncionariosList.Remove(funcionarioSelecionada);
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
                MessageBox.Show("Nenhum Funcionario selecionado.");
            }
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