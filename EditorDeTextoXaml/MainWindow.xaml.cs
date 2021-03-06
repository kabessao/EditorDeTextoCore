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
using EditorDeTextoCore;
using Microsoft.Win32;
using System.Windows.Threading;

namespace EditorDeTextoXaml
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            Relogio();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            timer.Tick += (s, e) => Relogio();

            txtRelogio.MouseDoubleClick += (s, e) => EscreverDataHora();


            Title = Titulo;
            txtTexto.TextChanged += (s, e) => TextoModificado();
            txtTexto.Focus();




            Sair.Click += (s, e) => this.Close();
            menuDesfazer.Click += (s, e) => txtTexto.Undo();
            menuColar.Click += (s, e) => txtTexto.Paste();
            menuCopiar.Click += (s, e) => txtTexto.Copy();
            menuRecortar.Click += (s, e) => txtTexto.Cut();
            MenuProcurar.Click += (s, e) => new Procurar(Getter).Show();
            menuHoraData.Click += (s, e) => EscreverDataHora();
            menuSelecionar.Click += (s, e) => txtTexto.SelectAll();
            MenuSobre.Click += (s, e) => new Sobre().Show();


            AtivarMenus(false);

            txtTexto.SelectionChanged += (s, e) =>
            {
                AtivarMenus(txtTexto.SelectedText.Length > 0);
            };


        }

        private void EscreverDataHora()
        {
            if (!String.IsNullOrEmpty(txtTexto.Text))
                txtTexto.Text += "\n" + $"<{DateTime.Now.ToString()}>\n";
            else
                txtTexto.Text = $"<{DateTime.Now.ToString()}>\n";

            txtTexto.Select(txtTexto.Text.Length, 0);
        }

        public void Relogio()
        {
            txtRelogio.Content = DateTime.Now.ToString();
        }



        public TextBox Getter() => txtTexto;


        public string Titulo { get { return "Notepad + ou -  "; } set { Title = Titulo + value; } }

        static public Arquivo ArqAberto = new Arquivo();


        private void AtivarMenus(bool ativar)
        {
            menuCopiar.IsEnabled = ativar;
            menuRecortar.IsEnabled = ativar;
            menuDeletar.IsEnabled = ativar;
        }

        private void TextoModificado()
        {
            if (!txtTexto.Text.Equals(ArqAberto.Texto))
                Titulo = "*" + ArqAberto.Nome;
            else
                Titulo = ArqAberto.Nome;

            if (ArqAberto.Texto == "" && txtTexto.Text == "") Titulo = "";


        }

        private void Salvar(object sender, RoutedEventArgs e)
        {
            if (!Arquivo.Existe(ArqAberto))
            {
                ArqAberto = Salvar();

            }
            ArqAberto.Texto = txtTexto.Text;
            ArqAberto.Salvar();
            TextoModificado();
        }

        private static Arquivo Salvar()
        {
            var arquivo = new SaveFileDialog
            {
                Filter = "Arquivos de texto|*.txt",
                Title = "Salvar como"
            };
            arquivo.ShowDialog();

            var _arq = new Arquivo()
            {
                Nome = arquivo.SafeFileName,
                DiretorioCompleto = arquivo.FileName
            };
            return _arq;
        }

        private void SalvarComo(object sender, RoutedEventArgs e)
        {
            ArqAberto = Salvar();
        }

        private void Fechando(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = Confirmação();

        }

        private bool Confirmação()
        {
            if (!ArqAberto.Texto.Equals(txtTexto.Text))
                switch (MessageBox.Show("Deseja salvar?", "Arquivo não salvo!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning))
                {
                    case MessageBoxResult.Yes:
                        Salvar(null, null);
                        return false;
                    case MessageBoxResult.Cancel:
                        return true;
                    default:
                        return false;
                }
            return false;
        }

        private void Abrir(object sender, RoutedEventArgs e)
        {
            if (!Confirmação())
            {

                OpenFileDialog file = new OpenFileDialog()
                {
                    Filter = "Arquivos de texto|*.txt",
                    Title = "Abrir arquivo"
                };

                file.ShowDialog();

                ArqAberto.Abrir(file.FileName, file.SafeFileName);

                Titulo = ArqAberto.Nome;
                txtTexto.Text = ArqAberto.Texto;

            }



        }

        private void Novo(object sender, RoutedEventArgs e)
        {
            if (!Confirmação())
            {
                ArqAberto = new Arquivo();
                Titulo = ArqAberto.Nome;
                txtTexto.Text = "";
            }

        }

        private void Deletar(object sender, RoutedEventArgs e)
        {
            //return;
            txtTexto.Text = txtTexto.Text.Remove(txtTexto.SelectionStart, txtTexto.SelectionLength);

        }
    }
}
