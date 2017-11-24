using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EditorDeTextoCore;


namespace EditorDeTexto.Testes
{
    [TestClass]
    public class ArquivoTeste
    {
        [TestMethod]
        public void Salvar_SalvandoArquivo_ArquivoExiste()
        {
            Arquivo file = CriarArquivo();

            Assert.IsTrue(Arquivo.Existe(file));
            
        }

        [TestMethod]
        public void Abrir_AbrindoArquivo_ArquivoContemTextoEsperado()
        {
            var teste = CriarArquivo();
            var abrindo = new Arquivo();
            abrindo.Abrir(teste.DiretorioCompleto, teste.Nome);

            Assert.AreEqual(teste.Texto, abrindo.Texto);


        }

        




        private static Arquivo CriarArquivo()
        {
            var file = new Arquivo
            {
                Nome = "teste.txt",
                DiretorioCompleto = Environment.CurrentDirectory + "teste.txt",
                Texto = "Teste"
            };
            file.Salvar();
            return file;
        }
    }
}
