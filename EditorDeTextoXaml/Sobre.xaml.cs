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

namespace EditorDeTextoXaml
{
    /// <summary>
    /// Interaction logic for Sobre.xaml
    /// </summary>
    public partial class Sobre : Window
    {
        public Sobre()
        {
            InitializeComponent();

            lblGit.MouseDoubleClick += (s, e) => System.Diagnostics.Process.Start("https://github.com/kabessao");
        }
    }
}
