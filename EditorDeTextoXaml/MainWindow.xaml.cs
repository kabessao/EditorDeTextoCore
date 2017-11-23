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
        private const string _CostTitulo = "Notepad + ou -";

        delegate void delegado();

        event delegado ArquivoModificado;


        Arquivo arq = new Arquivo();
        public MainWindow()
        {
            InitializeComponent();
            Title = _CostTitulo;
            txtTexto.TextChanged += (s, e) => TextoModificado();
        }

        private void TextoModificado()
        {
            if (!txtTexto.Text.Equals(arq.Texto))
                Title = _CostTitulo + "*" + arq.Nome;
            else
                Title = _CostTitulo + arq.Nome;
        }

        private void Salvar(object sender, RoutedEventArgs e)
        {
            if (!Arquivo.Existe(arq))
            {
                var arquivo = new SaveFileDialog();
                arquivo.Filter = "Arquivos de texto|*.txt";
                arquivo.ShowDialog();
                arq.Nome = arquivo.SafeFileName;
                arq.Diretorio = arquivo.InitialDirectory;
                
            }
            arq.Texto = txtTexto.Text;
            arq.Salvar();
            TextoModificado();
        }
    }
}
