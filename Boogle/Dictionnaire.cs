using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Boogle
{
    class Dictionnaire
    {
        private SortedList<int, string[]> _dico;
        private string _langue;

        public SortedList<int, string[]> Dico
        {
            get { return _dico; }
        }

        public Dictionnaire(string path, string langue)
        {
            _langue = langue;
            _dico = new SortedList<int, string[]>();
            string input = File.ReadAllText(path);
            string[] dico = Regex.Split(input, @"[0-9]+");
            int i=0;
            foreach (string item in dico)
            {
                if (item != "")
                {
                    _dico.Add(item.Replace("\r\n", "").Split(' '));
                }
            }
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < _dico.Count; i++)
            {
                res += $"Il y a {_dico[i].Length} mots de taille {i + 2}\n";
            }
            return res;
        }

        /// <summary>
        /// recherche dichotomique du mot demandé dans la liste de ceux de la meme taille
        /// </summary>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool RechDicoRecursif(int debut, int fin, string mot)
        {
            int milieu = (debut + fin) / 2;
            if (debut <= fin)
            {
                if (_dico[mot.Length - 2][milieu] == mot)
                {
                    return true;
                }
                else if (String.Compare(mot, _dico[mot.Length - 2][milieu]) < 0)
                {
                    return RechDicoRecursif(debut, milieu - 1, mot);
                }
                else
                {
                    return RechDicoRecursif(milieu + 1, fin, mot);
                }
            }
            else
            {
                return false;
            }
        }
    }
}