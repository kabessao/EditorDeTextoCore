using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace EditorDeTextoCore
{
    public class Arquivo
    {
        public string Nome { get; set; } = "[sem nome]";
        public string Diretorio { get; set; }
        public bool Salvo { get; set; }
        public string DiretorioCompleto { get {return Diretorio + Nome; } }
        public string  Texto { get; set; }


        public void Salvar()
        {
            var teste = File.Create(DiretorioCompleto);
            teste.Close();
            var escrever = new StreamWriter(DiretorioCompleto);
            escrever.Write(Texto);
            escrever.Close();
        }

        public static bool Existe (Arquivo arquivo)
        {
            return File.Exists(arquivo.DiretorioCompleto);
        }

       
    }
}
