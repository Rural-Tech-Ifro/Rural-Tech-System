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
    /// Lógica interna para Pastos.xaml
    /// </summary>
    public partial class TelaPasto : Window
    {
        PropriedadeDAO propriedade = new PropriedadeDAO();

        private Pasto _pasto = new Pasto();
        private PastoDAO _pastoDAO = new PastoDAO();
        public bool Editar = false;

        public ObservableCollection<Pasto> PastosList { get; set; }
        public TelaPasto()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            PastosList = new ObservableCollection<Pasto>(); // Inicializa a lista como uma ObservableCollection
            CarregarPastos();

            //COMBO BOX PROPRIEDADE

            foreach (Propriedade str in propriedade.GetPropriedade())
            {
                combo_propriedade.Items.Add(str.NomePropriedade);
            }
        }
        private void CarregarPastos()
        {
            try
            {
                var pastos = _pastoDAO.GetPasto(); // Obtém a lista de vacinas do banco
                PastosList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var animal in pastos)
                {
                    PastosList.Add(animal); // Adiciona cada vacina à ObservableCollection
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
            _pasto = new Pasto();
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
                        _pasto.Propriedade = str.Id.ToString();

                    }
                }
                _pasto.Tamanho = Convert.ToInt32(txt_tamanho.Text);
                _pasto.Limite = Convert.ToInt32(txt_limite.Text);
                _pasto.Descricao = txt_descricao.Text;
                _pasto.TipoPastagem = txt_tipoPastagem.Text;

                if (Editar)
                {
                    _pastoDAO.Update(_pasto);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _pastoDAO.Insert(_pasto); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                TelaPasto tela = new TelaPasto();
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
            if (sender is FrameworkElement element && element.DataContext is Pasto pastoSelecionado)
            {
                _pasto = pastoSelecionado;
                PreencherCamposComDados(_pasto); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Pasto pasto)
        {
            combo_propriedade.Text = pasto.Propriedade;
            txt_tamanho.Text = pasto.Tamanho.ToString();
            txt_limite.Text = pasto.Limite.ToString();
            txt_descricao.Text = pasto.Descricao;
            txt_tipoPastagem.Text = pasto.TipoPastagem;


        }
        private void LimparCampos()
        {
            combo_propriedade.Text = "";
            txt_tamanho.Text = "";
            txt_limite.Text = "";
            txt_descricao.Text = "";
            txt_tipoPastagem.Text = "";
        }


        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Pasto pastoSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _pastoDAO.Delete(pastoSelecionada);
                        PastosList.Remove(pastoSelecionada);
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
                MessageBox.Show("Nenhum pasto selecionado.");
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
