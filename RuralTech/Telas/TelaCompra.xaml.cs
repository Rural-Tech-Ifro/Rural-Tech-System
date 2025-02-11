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
    /// Lógica interna para TelaCompra.xaml
    /// </summary>
    public partial class TelaCompra : Window
    {
        FuncionarioDAO funcionario = new FuncionarioDAO();
        FornecedorDAO fornecedor = new FornecedorDAO();
        ProdutoDAO produto = new ProdutoDAO();

        private Compra _compra = new Compra();
        private CompraDAO _compraDAO = new CompraDAO();
        public bool Editar = false;
        public ObservableCollection<Compra> ComprasList { get; set; }

        public TelaCompra()
        {
            InitializeComponent();
            DataContext = this;
            ComprasList = new ObservableCollection<Compra>();
            CarregarCompras();

            //COMBO BOX animal

            foreach (Funcionario str in funcionario.GetFuncionario())
            {
                combo_funcionario.Items.Add(str.Nome);
            }
            foreach (Fornecedor str in fornecedor.GetFornecedor())
            {
                combo_fornecedor.Items.Add(str.Nome);
            }
            foreach (Produto str in produto.GetProduto())
            {
                combo_produto.Items.Add(str.Nome);
            }
        }

        private void CarregarCompras()
        {
            try
            {
                var compras = _compraDAO.GetCompras(); // Obtém a lista de vacinas do banco
                ComprasList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var compra in compras)
                {
                    ComprasList.Add(compra); // Adiciona cada vacina à ObservableCollection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar apartações: {ex.Message}");
            }
        }


        private void OpenModal(object sender, RoutedEventArgs e)
        {
            // Limpa o objeto e os campos quando adicionando um novo registro
            _compra = new Compra();
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
                foreach (Funcionario str in funcionario.GetFuncionario())
                {
                    if (str.Nome == combo_funcionario.Text)
                    {
                        _compra.Funcionario = str.Id.ToString();


                    }
                }
                foreach (Fornecedor str in fornecedor.GetFornecedor())
                {
                    if (str.Nome == combo_fornecedor.Text)
                    {
                        _compra.Fornecedor = str.Id.ToString();


                    }
                }
                foreach (Produto str in produto.GetProduto())
                {
                    if (str.Nome == combo_produto.Text)
                    {
                        _compra.Produto= str.Id.ToString();
                    }
                }

                // Atualiza o objeto _vacina com os valores do formulário
                _compra.Codigo = txt_codigo.Text;
                _compra.FormaPagamento = combo_formaPagamento.Text;
                _compra.DataCompra = Convert.ToDateTime(txt_dataCompra.Text);
                _compra.DataPagamento = Convert.ToDateTime(txt_dataPagamento.Text);
                _compra.QuantidadeParcelas = Convert.ToInt32(txt_quantidadeParcela.Text);

                if (Editar)
                {
                    _compraDAO.Update(_compra);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _compraDAO.Insert(_compra); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }

                TelaCompra tela = new TelaCompra();
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
            if (sender is FrameworkElement element && element.DataContext is Compra compraSelecionado)
            {
                _compra = compraSelecionado;
                PreencherCamposComDados(_compra); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Compra compra)
        {
            // txt_atributo1.Text = nome.Atributo1;
            //combo_atributo2.Text = nome.Atributo2;

            combo_funcionario.Text = compra.Funcionario;
            combo_fornecedor.Text = compra.Fornecedor;
            combo_produto.Text = compra.Produto;
            combo_formaPagamento.Text = compra.FormaPagamento;
            txt_codigo.Text = compra.Codigo;
            txt_dataCompra.Text = compra.DataCompra.ToString();
            txt_dataPagamento.Text = compra.DataPagamento.ToString();
            txt_quantidadeParcela.Text = compra.QuantidadeParcelas.ToString();


        }

        private void DeleteCompra(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Compra compraSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _compraDAO.Delete(compraSelecionada);
                        ComprasList.Remove(compraSelecionada);
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
        private void LimparCampos()
        {
            combo_funcionario.Text = "";
            combo_fornecedor.Text = "";
            combo_produto.Text = "";
            combo_formaPagamento.Text = "";
            txt_codigo.Clear();
            txt_dataCompra.Clear();
            txt_dataPagamento.Clear();
            txt_quantidadeParcela.Clear();
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
