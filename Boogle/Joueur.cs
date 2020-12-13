using System.Collections.Generic;
using System;
namespace Boogle
{
    class Joueur
    {
        private string _name;
        private int _score;
        private List<string> _found;
        private static List<string> _foundByAll = new List<string>();

        public string desc{
            get{return "-> "+_name+" "+Convert.ToString(_score);}
        }
        public Joueur(string name)
        {
            _name = name;
            _score = 0;
            _found = new List<string>();
        }

        /// <summary>
        /// renvoit true si le mot a déjà été trouvé par ce joueur
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool Contains(string mot)
        {
            return _found.Contains(mot);
        }

        /// <summary>
        /// ajoute le mot dans la liste des trouvés par le joueur et la liste des mots trouvés par tous les joueurs
        /// </summary>
        /// <param name="mot"></param>
        public void Add_mot(string mot)
        {
            if (mot.Length < 7) _score += mot.Length - 1;
            else _score += 11;
            _found.Add(mot);
            _foundByAll.Add(mot);
        }

        /// <summary>
        /// renvoit true si le mot a déjà été trouvé par un des joueurs
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool Mot_Cite(string mot)
        {
            return _foundByAll.Contains(mot);
        }
        
        /// <summary>
        /// affiche une description du joueur (son nom, son score et les mots qu'il a trouvé)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string res = $"{_name} a un score de {_score} et a trouvé les mots suivants :\n";
            foreach (string mot in _found) {
                res += $"{mot} ";
            }
            return res;
        }
        
    }
}