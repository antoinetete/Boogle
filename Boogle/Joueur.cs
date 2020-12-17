using System.Collections.Generic;
using System;
using System.Threading;
namespace Boogle
{
    public class Joueur : IComparable
    {
        private string _name;
        private int _score;
        private List<string> _found;

        public int Score{
            get{return _score;}
        }
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
        public void Clear(){
            this._found.Clear();
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

        public virtual string action(Plateau leplateau)
        {
            return Console.ReadLine().ToUpper();
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
        public static bool operator >=(Joueur A, Joueur B){
            return A._score>=B._score;
        }
        public static bool operator <=(Joueur A, Joueur B){
            return A._score<=B._score;
        }
        public static bool operator >(Joueur A, Joueur B){
            return A._score>B._score;
        }
        public static bool operator <(Joueur A, Joueur B){
            return A._score<B._score;
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
            return this == (Joueur)obj;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        int IComparable.CompareTo(object obj){
            return ((Joueur)obj)._score-this._score;
        }
        public class IA : Joueur
        {
            private int maxmots = 1;
            private Plateau leplateau;
            private Dictionnaire ledico;
            private Stack<string> motstrouve = new Stack<string>();

            public IA(int nbredejoueur,Dictionnaire undico)
            {
                this.ledico = undico;
                this._name = "IA "+Convert.ToString(nbredejoueur);
            }

            public override string action( Plateau unplateau)
            {
                string res = " ";
                int sommeil = 60000/maxmots;
                if(this.motstrouve.Count==0 && this._found.Count==0){
                    this.leplateau = unplateau;
                    this.motstrouve = new Stack<string>();
                    foreach(string mot in this.ledico.getallWords()){
                        if(this.leplateau.Test_Plateau(mot)){
                            this.motstrouve.Push(mot);
                        }
                    }
                    maxmots = this.motstrouve.Count+1;
                    sommeil = 60000/maxmots;
                }
                this.motstrouve.TryPop(out res);
                if(res == null){
                    res = " Je n'ai plus d'idée !!";
                    sommeil = 1000;
                }
                Thread.Sleep(sommeil);
                Console.WriteLine(res);
                return res;
            }

        }  
    }
    
}