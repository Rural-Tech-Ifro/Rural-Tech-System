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
    /// Lógica interna para Equipamentos.xaml
    /// </summary>
    public partial class TelaEquipamento : Window
    {
        PropriedadeDAO propriedade = new PropriedadeDAO();

        private Equipamento _equipamento = new Equipamento();
        private EquipamentoDAO _equipamentoDAO = new EquipamentoDAO();
        public bool Editar = false;

        public ObservableCollection<Equipamento> EquipamentosList { get; set; }
        public TelaEquipamento()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            EquipamentosList = new ObservableCollection<Equipamento>(); // Inicializa a lista como uma ObservableCollection
            CarregarEquipamentos();

            //COMBO BOX PROPRIEDADE

            foreach (Propriedade str in propriedade.GetPropriedade())
            {
                combo_propriedade.Items.Add(str.NomePropriedade);
            }
        }

        private void CarregarEquipamentos()
        {
            try
            {
                var equipamentos = _equipamentoDAO.GetEquipamento(); // Obtém a lista de vacinas do banco
                EquipamentosList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var equipamento in equipamentos)
                {
                    EquipamentosList.Add(equipamento); // Adiciona cada vacina à ObservableCollection
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
            _equipamento = new Equipamento();
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
                        _equipamento.Propriedade = str.Id.ToString();

                    }
                }
                _equipamento.Nome = txt_nome.Text;
                _equipamento.Valor = Convert.ToDouble(txt_valor.Text);
                _equipamento.Descricao = txt_descricao.Text;
                _equipamento.Tipo = txt_tipo.Text;

                if (Editar)
                {
                    _equipamentoDAO.Update(_equipamento);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _equipamentoDAO.Insert(_equipamento); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                TelaEquipamento tela = new TelaEquipamento();
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
            if (sender is FrameworkElement element && element.DataContext is Equipamento equipamentoSelecionado)
            {
                _equipamento = equipamentoSelecionado;
                PreencherCamposComDados(_equipamento); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Equipamento equipamento)
        {
            combo_propriedade.Text = equipamento.Propriedade;
            txt_nome.Text = equipamento.Nome;
            txt_tipo.Text = equipamento.Tipo;
            txt_valor.Text = equipamento.Valor.ToString();
            txt_descricao.Text = equipamento.Descricao;


        }
        private void LimparCampos()
        {
            combo_propriedade.Text = "";
            txt_nome.Text = "";
            txt_tipo.Text = "";
            txt_valor.Text = "";
            txt_descricao.Text = "";
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Equipamento equipamentoSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _equipamentoDAO.Delete(equipamentoSelecionada);
                        EquipamentosList.Remove(equipamentoSelecionada);
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
                MessageBox.Show("Nenhum equipamento selecionada.");
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
