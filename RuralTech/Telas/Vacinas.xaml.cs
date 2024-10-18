using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using RuralTech.Integracoes;

namespace RuralTech.Telas
{
    /// <summary>
    /// Lógica interna para Vacinas.xaml
    /// </summary>
    public partial class Vacinas : Window
    {
        private Vacina _vacina = new Vacina(); // Objeto vacina
        private VacinaDAO _vacinaDAO = new VacinaDAO(); // Objeto responsável por acessar o banco de dados
        public List<Vacina> VacinasList { get; set; } // Propriedade pública que será ligada ao ItemsControl

        // Construtor padrão
        public Vacinas()
        {
            InitializeComponent();
            DataContext = this; // Define o DataContext para a própria janela
            CarregarVacinas(); // Chama o método para carregar as vacinas do banco de dados ao iniciar a janela
        }

        // Método para carregar vacinas da base de dados
        private void CarregarVacinas()
        {
            try
            {
                VacinasList = null;
                VacinasList = _vacinaDAO.GetVacinas(); // Preenche a lista de vacinas
                OnPropertyChanged(nameof(VacinasList)); // Notifica o binding para atualizar a interface
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar vacinas: {ex.Message}");
            }
        }

        // Método que chama a atualização de propriedades para o binding (caso esteja implementado INotifyPropertyChanged)
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Preenche os campos de edição com os dados da vacina
        private void PreencherCamposVacina()
        {
            txt_nome.Text = _vacina.Nome;
            txt_dias.Text = _vacina.DiasCarencia.ToString();
            CBestado.Text = _vacina.Estado;
            txt_inscricao.Text = _vacina.InscricaoEstadual;
            txt_quantidade.Text = _vacina.Quantidade.ToString();
            CBunidadeEntrada.Text = _vacina.UnidadeEntrada;
            CBunidadeSaida.Text = _vacina.UnidadeSaida;
            txt_observacao.Text = _vacina.Observacao;
        }

        // Abre o modal para editar ou inserir dados
        private void OpenModal(object sender, RoutedEventArgs e)
        {
            PropertyPopup.IsOpen = true;
        }

        // Fecha o modal
        private void CloseModal(object sender, RoutedEventArgs e)
        {
            PropertyPopup.IsOpen = false;
        }

        // Salva as alterações (inserção ou edição) ao clicar no botão de salvar
        private void SaveProperty(object sender, RoutedEventArgs e)
        {
            try
            {
                // Atualiza o objeto _vacina com os valores do formulário
                _vacina.Nome = txt_nome.Text;

                // Converte Dias de Carência
                if (!int.TryParse(txt_dias.Text.Trim(), out int diasCarencia))
                {
                    MessageBox.Show("Por favor, insira um número válido para os dias de carência.");
                    return;
                }
                _vacina.DiasCarencia = diasCarencia;

                _vacina.Estado = CBestado.Text;
                _vacina.InscricaoEstadual = txt_inscricao.Text;

                // Converte a Quantidade
                if (!int.TryParse(txt_quantidade.Text.Trim(), out int quantidade))
                {
                    MessageBox.Show("Por favor, insira um número válido para a quantidade.");
                    return;
                }
                _vacina.Quantidade = quantidade;

                _vacina.UnidadeEntrada = CBunidadeEntrada.Text;
                _vacina.UnidadeSaida = CBunidadeSaida.Text;
                _vacina.Observacao = txt_observacao.Text;

                // Insere ou atualiza a vacina no banco de dados
                if (_vacina.Id <= 0)
                {
                    _vacinaDAO.Insert(_vacina);
                    MessageBox.Show("Registro cadastrado com sucesso.");
                }
                else
                {
                    MessageBox.Show("Registro não cadastrado com sucesso.");
                }


                // Fecha o modal e recarrega a lista de vacinas
                PropertyPopup.IsOpen = false;
                CarregarVacinas();

            }


            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
        }

        // Manipula o arrasto de imagens para dentro da janela
        private void ImageDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        // Exemplos de navegação entre outras telas
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Animal tela = new Animal();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Equipamentos tela = new Equipamentos();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Medicamentos tela = new Medicamentos();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Pastos tela = new Pastos();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {       
            Propriedades tela = new Propriedades();
            this.Close();
            tela.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Vacinas tela = new Vacinas();
            this.Close();
            tela.ShowDialog();
        }
    }
}
