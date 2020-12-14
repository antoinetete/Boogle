using System.Collections.Generic;
using System;
namespace Boogle
{
    class Joueur
    {
        public string _name;
        public int _score;
        public List<string> _found;

        public string desc
        {
            get { return "-> " + _name + " " + Convert.ToString(_score); }
        }
        public Joueur(string name)
        {
            _name = name;
            _score = 0;
            _found = new List<string>();
        }
        public Joueur() {
            _name = "t'as rien mis";
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
        }

        /// <summary>
        /// renvoit true si le mot a déjà été trouvé par un des joueurs
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>

    }
}