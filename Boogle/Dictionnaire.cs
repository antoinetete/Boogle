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

        public int finduDico(string mot)
        {
            return _dico[mot.Length].Length;
        }

        public Dictionnaire(string path, string langue)
        {
            try
            {
                _langue = langue;
                _dico = new SortedList<int, string[]>();
                string input = File.ReadAllText(path);
                string[] dico = Regex.Split(input, @"[0-9]+");
                int i = 2;
                foreach (string item in dico)
                {
                    if (item != "")
                    {
                        _dico.Add(i, item.Replace("\r\n", "").Split(' '));
                        i++;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 2; i < _dico.Keys.Count; i++)
            {
                res += $"Il y a {_dico[i].Length} mots de {i} lettres\n";
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
                if (_dico[mot.Length][milieu] == mot)
                {
                    return true;
                }
                else if (String.Compare(mot, _dico[mot.Length][milieu]) < 0)
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