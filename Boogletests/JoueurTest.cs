using System;
using Boogle;
using Xunit;

namespace Boogletest
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