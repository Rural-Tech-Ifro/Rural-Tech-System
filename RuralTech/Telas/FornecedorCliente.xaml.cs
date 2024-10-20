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
    public partial class FornecedorCliente : Window
    {
        private Fornecedor _fornecedor = new Fornecedor();
        private FornecedorDAO _fornecedorDAO = new FornecedorDAO();
        public ObservableCollection<Fornecedor> FornecedoresList { get; set; }
        public FornecedorCliente()
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

                _fornecedorDAO.Insert(_fornecedor); // Insere no banco
                MessageBox.Show("Registro cadastrado com sucesso.");
                FornecedorCliente tela = new FornecedorCliente();
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
