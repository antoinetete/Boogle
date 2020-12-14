using System.Collections.Generic;
using System;
namespace Boogle
{
    class Joueur
    {
        private string _name;
        private int _score;
        private List<string> _found;

        public string desc
        {
            get { return "-> " + _name + " " + Convert.ToString(_score); }
        }
        public string Nom
        {
            get { return this._name; }
        }
        public Joueur(string name)
        {
            _name = name;
            _score = 0;
            _found = new List<string>();
        }
        public Joueur()
        {
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
        public int Add_mot(string mot)
        {
            int points = 0;
            if (mot.Length < 7) points += mot.Length - 1;
            else points += 11;
            _found.Add(mot);
            _score += points;
            return points;
        }

        public string action()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// affiche une description du joueur (son nom, son score et les mots qu'il a trouvé)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string res = $"{_name} a un score de {_score} et a trouvé les mots suivants :\n";
            foreach (string mot in _found)
            {
                res += $"{mot} ";
            }
            return res;
        }
        public static bool operator ==(Joueur A, Joueur B)
        {
            return A._name == B._name;
        }
        public static bool operator !=(Joueur A, Joueur B)
        {
            return A._name != B._name;
        }
        public static bool operator ==(Joueur A, string name)
        {
            return A._name == name;
        }
        public static bool operator !=(Joueur A, string name)
        {
            return A._name != name;
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            return this == (Joueur)obj;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return base.GetHashCode();
        }
        class IA : Joueur
        {
            private char[,] _plateau;
            private List<string> mots;

            public IA(char[,] plateau)
            {
                _plateau = plateau;
                _name = "IA";
                mots = new List<string>();
            }


        }
    }
}