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

                _patrimonioDAO.Insert(_patrimonio); // Insere no banco
                MessageBox.Show("Registro cadastrado com sucesso.");
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
    }
}
