using System;

namespace Boogle
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionnaire test = new Dictionnaire("../MotsPossibles.txt", "FR");
            Console.WriteLine(test.ToString());
            Console.WriteLine(test.RechDicoRecursif(0, test.Dico[3].Length-1, "AVERA"));
        }
    }
}
