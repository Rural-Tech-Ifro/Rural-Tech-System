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
    /// Lógica interna para Patrimonio.xaml
    /// </summary>
    public partial class TelaPatrimonio : Window
    {
        PropriedadeDAO propriedade = new PropriedadeDAO();

        private Patrimonio _patrimonio = new Patrimonio();
        private PatrimonioDAO _patrimonioDAO = new PatrimonioDAO();
        public bool Editar = false;

        public ObservableCollection<Patrimonio> PatrimoniosList { get; set; }
        public TelaPatrimonio()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            PatrimoniosList = new ObservableCollection<Patrimonio>(); // Inicializa a lista como uma ObservableCollection
            CarregarPatrimonios();

            //COMBO BOX PROPRIEDADE

            foreach (Propriedade str in propriedade.GetPropriedade())
            {
                combo_propriedade.Items.Add(str.NomePropriedade);
            }
        }

        private void CarregarPatrimonios()
        {
            try
            {
                var patrimonios = _patrimonioDAO.GetPatrimonio(); // Obtém a lista de vacinas do banco
                PatrimoniosList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var patrimonio in patrimonios)
                {
                    PatrimoniosList.Add(patrimonio); // Adiciona cada vacina à ObservableCollection
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
            _patrimonio = new Patrimonio();
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
                foreach (Propriedade str in propriedade.GetPropriedade())
                {
                    if (str.NomePropriedade == combo_propriedade.Text)
                    {
                        _patrimonio.Propriedade = str.Id.ToString();
                    }
                }
                _patrimonio.Nome = txt_nome.Text;
                _patrimonio.Valor = Convert.ToDouble(txt_valor.Text);
                _patrimonio.Descricao = txt_descricao.Text;
                _patrimonio.Tipo = txt_tipo.Text;

                if (Editar)
                {
                    _patrimonioDAO.Update(_patrimonio);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _patrimonioDAO.Insert(_patrimonio); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                TelaPatrimonio tela = new TelaPatrimonio();
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
            if (sender is FrameworkElement element && element.DataContext is Patrimonio patrimonioSelecionado)
            {
                _patrimonio = patrimonioSelecionado;
                PreencherCamposComDados(_patrimonio); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Patrimonio patrimonio)
        {
            combo_propriedade.Text = patrimonio.Propriedade;
            txt_nome.Text = patrimonio.Nome;
            txt_valor.Text = patrimonio.Valor.ToString();
            txt_descricao.Text = patrimonio.Descricao;
            txt_tipo.Text = patrimonio.Tipo;


        }
        private void LimparCampos()
        {
            combo_propriedade.Text = "";
            txt_nome.Text = "";
            txt_valor.Text = "";
            txt_descricao.Text = "";
            txt_tipo.Text = "";
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
