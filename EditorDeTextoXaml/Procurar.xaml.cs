using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EditorDeTextoXaml
{
    /// <summary>
    /// Interaction logic for Procurar.xaml
    /// </summary>
    public partial class Procurar : Window
    {
        public delegate TextBox Funcao();

        public Funcao Getter;

        
        public Procurar(Funcao funcao)
        {
            Getter = funcao;
            InitializeComponent();
            ShowInTaskbar = false;
        }

        private void Proximo(object sender, RoutedEventArgs e)
        {
            if (Getter().Text.Contains(txtProcurar.Text))
            {
                Getter().Select(Getter().Text.IndexOf(txtProcurar.Text), txtProcurar.Text.Length);
                Getter().Focus();
            }
            else
                MessageBox.Show("\"" + txtProcurar.Text + "\" não encontrado");


        }
    }
}
