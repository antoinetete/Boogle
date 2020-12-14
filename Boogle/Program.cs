using System;

namespace Boogle
{
    class Program
    {
        static int SEED = 3;
        static void testDictionnaire(){
            Dictionnaire myDictionnaire = new Dictionnaire("../MotsPossibles.txt", "FR");
            Console.WriteLine(myDictionnaire.ToString());
            Console.WriteLine(myDictionnaire.RechDicoRecursif(0, myDictionnaire.finduDico, "AVERA"));
        }
        static void testLoadPlateau(){
            Random rnd = new Random(SEED);
            Plateau myplateau = new Plateau("../Des.txt",rnd);
            Console.WriteLine(myplateau);
        }
        static void testPlateau(){
            Random rnd = new Random(SEED);
            Plateau myplateau = new Plateau(rnd);
            Console.WriteLine(myplateau);
            Console.Write("mvm: ");
            Console.WriteLine(myplateau.Test_Plateau("mvm"));
            Console.Write("mvmc: ");
            Console.WriteLine(myplateau.Test_Plateau("mvmc"));
            Console.Write("rvqb: ");
            Console.WriteLine(myplateau.Test_Plateau("rvqb"));
            Console.Write("xvcy: ");
            Console.WriteLine(myplateau.Test_Plateau("xvcy"));
            Console.Write("mvma: ");
            Console.WriteLine(myplateau.Test_Plateau("mvma"));
            Console.Write("rmcgycvxwmqfbacy: ");
            Console.WriteLine(myplateau.Test_Plateau("rmcgycvxwmqfbacy"));
            Console.Write("noop: ");
            Console.WriteLine(myplateau.Test_Plateau("noop"));
            Console.Write("mvmf: ");
            Console.WriteLine(myplateau.Test_Plateau("mvmf"));
            Console.Write("m: ");
            Console.WriteLine(myplateau.Test_Plateau("m"));
        }
        static void Main(string[] args)
        {
            testPlateau();
            testDictionnaire();
            testLoadPlateau();
            jeu thegame =new jeu("../MotsPossibles.txt","../Des.txt");
        }
    }
}
