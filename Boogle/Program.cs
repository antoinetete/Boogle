using System;

namespace Boogle
{
    class Program
    {
        static int SEED = 3;
        static void testDictionnaire(){
            Dictionnaire myDictionnaire = new Dictionnaire("../MotsPossibles.txt", "FR");
            Console.WriteLine(myDictionnaire.ToString());
            Console.WriteLine(myDictionnaire.RechDicoRecursif(0, myDictionnaire.Dico[3].Length-1, "AVERA"));
        }
        static void testPlateau(){
            Random rnd = new Random(SEED)
            Plateau myplateau = new Plateau(rnd);
            Console.WriteLine(myplateau.ToString());
        }
        static void Main(string[] args)
        {
            testPlateau();
        }
    }
}
