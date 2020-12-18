using System;
using Boogle;
using Xunit;

namespace Boogletests
{
    public class JoueurTest
    {
        [Theory]
        [InlineData("mot")]
        [InlineData("josh")]
        [InlineData("wow")]
        [InlineData("")]
        [InlineData("incredible")]
        [InlineData("mos")]
        /// <summary>
        /// tests si les mots ne se repetttent pas
        /// </summary>
        /// <param name="mot">
        /// le mot a tester
        /// </param>
        public void persistence_des_mots(string mot){
            Joueur josh = new Joueur("josh");
            josh.Add_mot(mot);
            Assert.True(josh.Contains(mot));
        }
        [Theory]
        [InlineData("mot")]
        [InlineData("josh")]
        [InlineData("wow")]
        [InlineData("")]
        [InlineData("incredible")]
        [InlineData("mos")]
        /// <summary>
        /// test validité des mots
        /// </summary>
        /// <param name="mot"></param>
        public void mauvais_mots(string mot){
            Joueur josh = new Joueur("josh");
            josh.Add_mot(mot);
            Assert.False(josh.Contains("cemotnestpaspresent"));
        }
        [Theory]
        [InlineData("mot")]
        [InlineData("josh")]
        [InlineData("wow")]
        [InlineData("")]
        [InlineData("incredible")]
        [InlineData("mos")]
        /// <summary>
        /// test fonctionnement du score
        /// </summary>
        /// <param name="mot"></param>
        public void test_score(string mot){
            Joueur josh = new Joueur("josh");
            josh.Add_mot(mot);
            int val =11;
            if(mot.Length<7){
                val = mot.Length-1;
            }
            Assert.Equal(josh.Score,val);
        }
    }
}
