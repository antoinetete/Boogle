using System.Collections.Generic;

namespace Boogle
{
    class IA : Joueur
    {
        private char[,] _plateau;
        private List<string> mots;

        public IA(char[,] plateau) {
            _plateau = plateau;
            _name = "IA";
            mots = new List<string>();
        }


    }
}