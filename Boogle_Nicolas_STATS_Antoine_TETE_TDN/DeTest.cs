using System;
using Xunit;
using Boogle;

namespace Boogletests
{
    public class Detest
    {
        
        [Theory]
        [InlineData ("a","b")]//too short
        [InlineData ("a","b","e","R","f","6")]//invalid char 
        [InlineData ("a","b","*","g","f","c")]//invalid char
        [InlineData ("a","b","c","g","ff","c")]//too long
        /// <summary>
        /// test si le constructeur détecte correctement les combinaisions non accepeté
        /// </summary>
        /// <param name="input">
        /// les combinaisions a tester
        /// </param>
        public void erreur_de_creation_string(params string[] input)
        {   
            Random rnd = new Random();
            try{
                new De(input, rnd);
                Assert.False(true);
            }
            catch (Exception){
                Assert.True(true);
            }
            
        }
        [Theory]
        [InlineData ('a','b')]//too short
        [InlineData ('a','b','e','R','f','6')]//invalid char 
        [InlineData ('a','b','*','g','g','c')]//invalid char
        /// <summary>
        /// test si le constructeur détecte correctement les combinaisions non accepeté
        /// </summary>
        /// <param name="input">
        /// les combinaisons a tester
        /// </param>
        public void erreur_de_creation_char(params char[] input)
        {   
            Random rnd = new Random();
            try{
                new De(input, rnd);
                Assert.False(true);//echec, on n est jamais sensé arriver ici
            }
            catch (Exception){
                Assert.True(true);//reussite car toutes les erreurs ont été attrapé
            }
            
        }
        [Theory]
        [InlineData ("a","b","g","g","f","c")]//valide
        [InlineData ("l","k","v","a","x","a")]//valide
        /// <summary>
        /// test si le constructeur fonctionne et conserve les faces
        /// </summary>
        /// <param name="input"></param>
        public void conservation_des_arguments_string(params string[] input){
            Random rnd = new Random();
            De temp = new De(input, rnd);
            char[] input_as_charrray  = new char[input.Length];
            for(int i=0;i<input.Length;i++){
                input_as_charrray[i] = input[i][0];
            }
            Assert.Equal(input_as_charrray, temp.Values);
        }
        [Theory]
        [InlineData ('a','b','g','g','f','c')]//valide
        [InlineData ('l','k','v','a','x','a')]//valide
        /// <summary>
        /// le De conserve proprement les faces
        /// </summary>
        /// <param name="input"></param>
        public void conservation_des_arguments_char(params char[] input){
            Random rnd = new Random();
            De temp = new De(input, rnd);
            Assert.Equal(input, temp.Values);
        }
    }
}
