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
    /// Lógica interna para FornecedorCliente.xaml
    /// </summary>
    public partial class TelaFornecedor : Window
    {
        private Fornecedor _fornecedor = new Fornecedor();
        private FornecedorDAO _fornecedorDAO = new FornecedorDAO();
        public bool Editar = false;
        public ObservableCollection<Fornecedor> FornecedoresList { get; set; }
        public TelaFornecedor()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            FornecedoresList = new ObservableCollection<Fornecedor>(); // Inicializa a lista como uma ObservableCollection
            CarregarFornecedores();
        }
        private void CarregarFornecedores()
        {
            try
            {
                var equipamentos = _fornecedorDAO.GetFornecedor(); // Obtém a lista de vacinas do banco
                FornecedoresList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var equipamento in equipamentos)
                {
                    FornecedoresList.Add(equipamento); // Adiciona cada vacina à ObservableCollection
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
            _fornecedor = new Fornecedor();
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
                _fornecedor.Nome = txt_nome.Text;
                _fornecedor.Celular = txt_celular.Text;
                _fornecedor.Telefone = txt_telefone.Text;
                _fornecedor.CPFCNPJ = txt_cpfCnpj.Text;
                _fornecedor.Pais = txt_pais.Text;
                _fornecedor.Estado = txt_estado.Text;
                _fornecedor.Cidade = txt_cidade.Text;
                _fornecedor.CEP = txt_cep.Text;
                _fornecedor.Numero = txt_numero.Text;
                _fornecedor.Logradouro = txt_logradouro.Text;
                _fornecedor.Email = txt_email.Text;

                if (Editar)
                {
                    _fornecedorDAO.Update(_fornecedor);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _fornecedorDAO.Insert(_fornecedor); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                TelaFornecedor tela = new TelaFornecedor();
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
            if (sender is FrameworkElement element && element.DataContext is Fornecedor fornecedorSelecionado)
            {
                _fornecedor = fornecedorSelecionado;
                PreencherCamposComDados(_fornecedor); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Fornecedor fornecedor)
        {
            txt_nome.Text = fornecedor.Nome;
            txt_celular.Text = fornecedor.Celular;
            txt_telefone.Text = fornecedor.Telefone;
            txt_cpfCnpj.Text = fornecedor.CPFCNPJ;
            txt_pais.Text = fornecedor.Pais;
            txt_estado.Text = fornecedor.Estado;
            txt_cidade.Text = fornecedor.Cidade;
            txt_cep.Text = fornecedor.CEP;
            txt_numero.Text = fornecedor.Numero;
            txt_logradouro.Text = fornecedor.Logradouro;
            txt_email.Text = fornecedor.Email;

        }
        private void LimparCampos()
        {
            txt_nome.Text = "";
            txt_celular.Text = "";
            txt_telefone.Text = "";
            txt_cpfCnpj.Text = "";
            txt_pais.Text = "";
            txt_estado.Text = "";
            txt_cidade.Text = "";
            txt_cep.Text = "";
            txt_numero.Text = "";
            txt_logradouro.Text = "";
            txt_email.Text = "";
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
