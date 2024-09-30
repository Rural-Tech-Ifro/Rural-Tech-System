﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuralTech.Telas
{
    /// <summary>
    /// Interação lógica para CadastrarEquipamentos.xaml
    /// </summary>
    public partial class CadastrarEquipamentos : Page
    {
        public CadastrarEquipamentos()
        {
            InitializeComponent();
        }

        private void imagem_status_Click(object sender, RoutedEventArgs e)
        {

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
            string propertyName = txt_propriedade.Text;
            string ownerName = txt_proprietario.Text;
            string size = txt_tamanho.Text;
            string phone = txt_telefone.Text;
           
            // Lógica para salvar as informações da propriedade

            // Fechar o modal após salvar
            PropertyPopup.IsOpen = false;
        }

    }
}
