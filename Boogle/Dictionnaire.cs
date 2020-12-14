using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Boogle
{
    class Dictionnaire
    {
        private List<string[]> _dico;
        private string _langue;

        public List<string[]> Dico
        {
            get { return _dico; }
        }
        public int finduDico{
            get{return _dico[3].Length-1;}
        }
        public Dictionnaire(string path, string langue)
        {
            _langue = langue;
            _dico = new List<string[]>();
            string input = File.ReadAllText(path);
            string[] dico = Regex.Split(input, @"[0-9]+");
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