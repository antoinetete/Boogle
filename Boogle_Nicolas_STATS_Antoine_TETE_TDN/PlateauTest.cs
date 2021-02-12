using System;
using Xunit;
using Boogle;
using System.IO;

namespace Boogletests
{
    public class PlateauTest
    {
        [Theory] //liste des tests à faire
        [InlineData("mvm")]//horizontal
        [InlineData("mvmc")]//horizontal complet
        [InlineData("rvqb")]//vertical
        [InlineData("xvcy")]//diagonal
        [InlineData("mvma")]//mix
        [InlineData("rmcgycvxwmqfbacy")]//total
        public void mot_dans_plateau(string input){
            Random rnd = new Random(3);
            Plateau monplteau = new Plateau(rnd);
            Assert.True(monplteau.Test_Plateau(input)); //les tests doivent renvoyer true
        }

        [Theory] //liste des tests à faire
        [InlineData("mv")]//trops cours
        [InlineData("mvzae")]//n existe pas mais existe partiellement
        [InlineData("noop")]// n existe pas
        [InlineData("hello")]// n existe pas
        [InlineData("mvmf")]// n est pas valdie 
        [InlineData("m")]// trops cours
        [InlineData("")]// trops cours
        public void mot_PAS_dans_plateau(string input){
            Random rnd = new Random(3);
            Plateau monplteau = new Plateau(rnd);
            Assert.False(monplteau.Test_Plateau(input)); //les tests doivent renvoyer false
        }

        [Fact] //test à faire
        public void erreur_recherche_de_fichier(){
            try
            {
                Plateau monPlateau = new Plateau("./nexistepas.txt",new Random()); //doit crash parce que le fichier n'existe pas
                Assert.True(false); //si ça crash pas le test est raté
            }
            catch(Exception){
                Assert.True(true); //si ça crash le test est réussi
            }
        }
        
    }
}