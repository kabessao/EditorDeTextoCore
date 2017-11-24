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
        // Propriedades

        public string Nome { get; set; } = "[sem nome]";
        public string DiretorioCompleto { get; set; }
        public string Texto { get; set; } = "";

        

        
        public void Salvar()
        {
            var teste = File.Create(DiretorioCompleto);
            teste.Close();
            var escrever = new StreamWriter(DiretorioCompleto);
            escrever.Write(Texto);
            escrever.Close();
        }

        

        // Existe
        public static bool Existe (Arquivo arquivo)
        {
            return File.Exists(arquivo.DiretorioCompleto);
        }


        

        // Abrir

        public void Abrir (string diretorioCompleto, string nome)
        {
            if (!File.Exists(diretorioCompleto))
                throw new FileNotFoundException();


            var file = new StreamReader(diretorioCompleto);
            this.Nome = nome;
            this.DiretorioCompleto = diretorioCompleto;
            this.Texto = file.ReadToEnd();
            file.Close();
            
        }

        

    }
}
