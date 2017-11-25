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


        /// <summary>
        /// Nome do arquivo
        /// </summary>
        public string Nome { get; set; } = "[sem nome]";

        /// <summary>
        /// Diretorio completo do arquivo. Ex.: C://user//Desktop//arquivo.txt
        /// </summary>
        public string DiretorioCompleto { get; set; }

        /// <summary>
        /// Texto que vai dentro do arquivo
        /// </summary>
        public string Texto { get; set; } = "";

        /// <summary>
        /// Salva o arquivo
        /// </summary>
        public void Salvar()
        {
            var teste = File.Create(DiretorioCompleto);
            teste.Close();
            var escrever = new StreamWriter(DiretorioCompleto);
            escrever.Write(Texto);
            escrever.Close();
        }

        /// <summary>
        /// Testa se o arquivo existe
        /// </summary>
        /// <param name="arquivo">Arquivo a ser testado</param>
        /// <returns>TRUE se existe e FALSE se não existe</returns>
        public static bool Existe(Arquivo arquivo)
        {
            return File.Exists(arquivo.DiretorioCompleto);
        }
        
        /// <summary>
        /// Abre um arquivo, setando suas caracteristicas com o objeto
        /// </summary>
        /// <param name="diretorioCompleto">Diretorio completo do arquivo. Ex.:C://user//Desktop//arquivo.txt</param>
        /// <param name="nome">Somente o nome do arquivo. Ex.:arquivo.txt</param>
        public void Abrir(string diretorioCompleto, string nome)
        {
            if (File.Exists(diretorioCompleto))
            {
                var file = new StreamReader(diretorioCompleto);
                this.Nome = nome;
                this.DiretorioCompleto = diretorioCompleto;
                this.Texto = file.ReadToEnd();
                file.Close();
            }
            else
            {
                Reset();
            }
           

        }

        /// <summary>
        /// Reseta o objeto
        /// </summary>
        private void Reset()
        {
            this.Nome = "[sem nome]";
            this.DiretorioCompleto = "";
            this.Texto = "";
        }


    }
}
