using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using RuralTech.Integracoes;
using System.Collections.ObjectModel;


namespace RuralTech.Telas
{
    /// <summary>
    /// Lógica interna para Animal.xaml
    /// </summary>
    public partial class TelaAnimal : Window
    {
        private PropriedadeDAO propriedade = new PropriedadeDAO();

        private Animals _animal = new Animals();
        private AnimalDAO _animalDAO = new AnimalDAO();
        public bool Editar = false;

        public ObservableCollection<Animals> AnimaisList { get; set; }

        public TelaAnimal()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            AnimaisList = new ObservableCollection<Animals>(); // Inicializa a lista como uma ObservableCollection
            CarregarVacinas();

            //COMBO BOX

            foreach (Propriedade str in propriedade.GetPropriedade())
            {
                combo_propriedade.Items.Add(str.NomePropriedade);
            }


        }
        private void CarregarVacinas()
        {
            try
            {
                var animais = _animalDAO.GetAnimal(); // Obtém a lista de vacinas do banco
                AnimaisList.Clear(); // Limpa a coleção atual para evitar duplicatas
                foreach (var animal in animais)
                {
                    AnimaisList.Add(animal); // Adiciona cada vacina à ObservableCollection
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
            _animal = new Animals();
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
                _animal.Brinco = txt_brinco.Text;
                _animal.Raca = combo_raca.Text;
                _animal.Classificacao = txt_classificacao.Text;
                _animal.Sexo = combo_sex.Text;
                _animal.Origem = combo_origem.Text;
                if (Editar)
                {
                    _animalDAO.Update(_animal);
                    MessageBox.Show("Registro atualizado com sucesso.");
                    Editar = false; // Volta o estado para novo registro
                }
                else
                {
                    _animalDAO.Insert(_animal); // Insere no banco
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
               
                TelaAnimal tela = new TelaAnimal();
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
            if (sender is FrameworkElement element && element.DataContext is Animals animalSelecionado)
            {
                _animal = animalSelecionado;
                PreencherCamposComDados(_animal); // Preenche o formulário com os dados para edição
                Editar = true;
                PropertyPopup.IsOpen = true;
            }
        }


        private void PreencherCamposComDados(Animals animal)
        {
            txt_brinco.Text = animal.Brinco;
            combo_sex.Text = animal.Sexo;
            combo_raca.Text = animal.Raca;
            txt_classificacao.Text = animal.Classificacao;
            combo_origem.Text = animal.Origem;
           
        }
        private void LimparCampos()
        {
            txt_brinco.Text = "";
            combo_sex.Text = "";
            combo_raca.Text = "";
            txt_classificacao.Text = "";
            combo_origem.Text = "";
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Animals animalSelecionada)
            {
                var resultado = MessageBox.Show("Tem certeza de que deseja excluir este registro?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        _animalDAO.Delete(animalSelecionada);
                        AnimaisList.Remove(animalSelecionada);
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
                MessageBox.Show("Nenhum animal selecionada.");
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
