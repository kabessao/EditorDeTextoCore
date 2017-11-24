// Bibliotecas
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EditorDeTextoCore;
using Microsoft.Win32;



namespace EditorDeTextoXaml
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        

        

        public string Titulo { get { return "Notepad + ou -  "  ; } set { Title = Titulo + value; } }



        static public Arquivo ArqAberto = new Arquivo();

        

        
        public MainWindow()
        {
            InitializeComponent();
            Title = Titulo;
            txtTexto.TextChanged += (s, e) => TextoModificado();
            txtTexto.Focus();
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
            if (!string.Equals(ArqAberto.Texto, txtTexto.Text))
                switch (MessageBox.Show("Deseja salvar?", "Arquivo não salvo!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning))
                {
                    case MessageBoxResult.Yes:
                        Salvar(null, null);
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
                    

        }
        
        private void Abrir(object sender, RoutedEventArgs e)
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
}
