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
    /// Lógica interna para TelaTransportador.xaml
    /// </summary>
    public partial class TelaTransportador : Window
    {
        private Transportador _transportador = new Transportador();
        private TransportadorDAO _transportadorDAO = new TransportadorDAO();
        public bool Editar = false;

        public ObservableCollection<Transportador> TransportadoresList { get; set; }

        public TelaTransportador()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            TransportadoresList = new ObservableCollection<Transportador>(); // Inicializa a lista como uma ObservableCollection
            CarregarExames();
        }
        private void CarregarExames()
        {
            try
            {
                var transportadores = _transportadorDAO.GetTransportadores(); // Obtém a lista de vacinas do banco
                TransportadoresList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var transportador in transportadores)
                {
                    TransportadoresList.Add(transportador); // Adiciona cada vacina à ObservableCollection
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
            _transportador = new Transportador();
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
                _transportador.Cpf = txt_cpf.Text;
                _transportador.Cnpj = txt_cnpj.Text;
                _transportador.Nome = txt_nome.Text;
                _transportador.InscricaoEstadual = txt_inscricao_estadual.Text;
                _transportador.Estado = combo_estadol.Text;
                _transportador.Cidade = txt_cidade.Text;
                _transportador.Bairro = txt_bairro.Text;
                _transportador.Rua = txt_rua.Text;
                _transportador.Numero = txt_numero.Text;


                if (Editar)
                {
                    _transportadorDAO.Update(_transportador);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _transportadorDAO.Insert(_transportador); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                TelaTransportador tela = new TelaTransportador();
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
            if (sender is FrameworkElement element && element.DataContext is Transportador transporteSelecionado)
            {
                _transportador = transporteSelecionado;
                PreencherCamposComDados(_transportador); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Transportador transporte)
        {
            txt_cpf.Text = transporte.Cpf;
            txt_cnpj.Text = transporte.Cnpj;
            txt_nome.Text = transporte.Nome;
            txt_inscricao_estadual.Text = transporte.InscricaoEstadual;
            combo_estadol.Text = transporte.Estado;
            txt_cidade.Text = transporte.Cidade;
            txt_bairro.Text = transporte.Bairro;
            txt_rua.Text = transporte.Rua;
            txt_numero.Text = transporte.Numero;
        }
        private void LimparCampos()
        {
            txt_cpf.Text = "";
            txt_cnpj.Text = "";
            txt_nome.Text = "";
            txt_inscricao_estadual.Text = "";
            combo_estadol.Text = "";
            txt_cidade.Text = "";
            txt_bairro.Text = "";
            txt_rua.Text = "";
            txt_numero.Text = "";
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
