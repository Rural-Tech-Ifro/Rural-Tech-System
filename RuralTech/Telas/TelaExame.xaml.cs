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
    /// Lógica interna para TelaExame.xaml
    /// </summary>
    public partial class TelaExame : Window
    {
        AnimalDAO animal = new AnimalDAO();
        private Exame _exame = new Exame();
        private ExameDAO _exameDAO = new ExameDAO();
        public bool Editar = false;

        public ObservableCollection<Exame> ExamesList { get; set; }

        public TelaExame()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            ExamesList = new ObservableCollection<Exame>(); // Inicializa a lista como uma ObservableCollection
            CarregarExames();
            //COMBO BOX
            
            foreach (Animals str in animal.GetAnimal())
            {
                combo_animal.Items.Add(str.Brinco);
            }

        }
        private void CarregarExames()
        {
            try
            {
                var exames = _exameDAO.GetExames(); // Obtém a lista de vacinas do banco
                ExamesList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var exame in exames)
                {
                    ExamesList.Add(exame); // Adiciona cada vacina à ObservableCollection
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
            _exame = new Exame();
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
                foreach (Animals str in animal.GetAnimal())
                {
                    if (str.Brinco == combo_animal.Text)
                    {
                        _exame.Animal = str.Id.ToString();

                    }
                }
                // Atualiza o objeto _vacina com os valores do formulário
                _exame.Tipo = combo_exame.Text;
                _exame.Resultado = txt_resultado.Text;
                _exame.Data = Convert.ToDateTime(txt_data_exame.Text);

                if (Editar)
                {
                    _exameDAO.Update(_exame);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _exameDAO.Insert(_exame); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                TelaExame tela = new TelaExame();
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
            if (sender is FrameworkElement element && element.DataContext is Exame exameSelecionado)
            {
                _exame = exameSelecionado;
                PreencherCamposComDados(_exame); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Exame exame)
        {
            combo_exame.Text = exame.Tipo;
            txt_resultado.Text = exame.Resultado;
            txt_data_exame.Text = exame.Data.ToString();
            combo_animal.Text = exame.Animal;


        }
        private void LimparCampos()
        {
            combo_exame.Text = "";
            txt_resultado.Text = "";
            txt_data_exame.Text = "";
            combo_animal.Text = "";
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Exame exameSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _exameDAO.Delete(exameSelecionada);
                        ExamesList.Remove(exameSelecionada);
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
                MessageBox.Show("Nenhum Exame selecionado.");
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
