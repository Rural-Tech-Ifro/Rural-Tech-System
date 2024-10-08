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
using System.Windows.Shapes;

namespace RuralTech.Telas
{
    /// <summary>
    /// Lógica interna para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var usuario = txt_usuario.Text;
            var senha = txt_senha.Password;

            Animal login = new Animal();
            this.Close();
            login.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CadastrarUsuario tela = new CadastrarUsuario();
            this.Close();
            tela.ShowDialog();
        }
    }
}
