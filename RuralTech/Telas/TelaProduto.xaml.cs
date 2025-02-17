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
    /// Lógica interna para Produtos.xaml
    /// </summary>
    public partial class TelaProduto : Window
    {
        private Produto _produto = new Produto();
        private ProdutoDAO _produtoDAO = new ProdutoDAO();
        public bool Editar = false;
        public ObservableCollection<Produto> ProdutosList { get; set; }
        public TelaProduto()
        {
            InitializeComponent();
            DataContext = this;
            ProdutosList = new ObservableCollection<Produto>();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            try
            {
                var produtos = _produtoDAO.GetProduto(); // Obtém a lista de vacinas do banco
                ProdutosList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var produto in produtos)
                {
                    ProdutosList.Add(produto); // Adiciona cada vacina à ObservableCollection
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
            _produto = new Produto();
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
                _produto.Nome = txt_nome.Text;
                _produto.PrecoCusto = Convert.ToDouble(txt_preco_custo.Text);
                _produto.Quantidade = Convert.ToInt32(txt_quantidade.Text);
                _produto.DataVencimento = Convert.ToDateTime(txt_data_vencimento.Text);
                _produto.UnidadeEntrada = txt_unidade_entrada.Text;
                _produto.PrecoVenda = Convert.ToDouble(txt_preco_venda.Text);




                if (Editar)
                {
                    _produtoDAO.Update(_produto);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _produtoDAO.Insert(_produto); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }

                TelaProduto tela = new TelaProduto();
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
            if (sender is FrameworkElement element && element.DataContext is Produto produtoSelecionado)
            {
                _produto = produtoSelecionado;
                PreencherCamposComDados(_produto); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Produto produto)
        {
            txt_nome.Text = produto.Nome;
            txt_preco_custo.Text = produto.PrecoCusto.ToString();
            txt_quantidade.Text = produto.Quantidade.ToString();
            txt_data_vencimento.Text = produto.DataVencimento.ToString();
            txt_unidade_entrada.Text = produto.UnidadeEntrada;
            txt_unidade_saida.Text = produto.UnidadeSaida;
            txt_preco_venda.Text = produto.PrecoVenda.ToString();


        }
        private void LimparCampos()
        {
            txt_nome.Text = "";
            txt_preco_custo.Text = "";
            txt_quantidade.Text = "";
            txt_data_vencimento.Text = "";
            txt_unidade_entrada.Text = "";
            txt_unidade_saida.Text = "";
            txt_preco_venda.Text = "";
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Produto produtoSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _produtoDAO.Delete(produtoSelecionada);
                        ProdutosList.Remove(produtoSelecionada);
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
                MessageBox.Show("Nenhum produto selecionado.");
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
