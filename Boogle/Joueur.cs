using System.Collections.Generic;
using System;
using System.Threading;
namespace Boogle
{
    /// <summary>
    /// une classe joueur qui implémente ICompatable.CompareTo() pour les sorts
    /// </summary>
    public class Joueur : IComparable
    {
        private string _name;//le nom du joueur
        private int _score;//le score du joueur
        private List<string> _found;

        /// <summary>
        /// score public pour accéder dans les affichages
        /// </summary>
        /// <value>
        /// le score du joueur
        /// </value>
        public int Score{
            get{return _score;}
        }

        /// <summary>
        /// une courte decription du joueur pour garder le ToString comme officiel
        /// </summary>
        /// <value>
        /// exe: -> sam 3
        /// </value>
        public string desc
        {
            get { return "-> " + _name + " " + Convert.ToString(_score); }
        }
        /// <summary>
        /// string public utilisé pour faire les choix de joueur
        /// </summary>
        /// <value>
        /// le nom du joueur 
        /// </value>
        public string Nom
        {
            get { return this._name; }
        }
        /// <summary>
        /// constructeur simple prennant seulement le nom du joueur
        /// </summary>
        /// <param name="name"></param>
        public Joueur(string name)
        {
            _name = name;
            _score = 0;
            _found = new List<string>();
        }
        /// <summary>
        /// constructeur le plus simple
        /// </summary>
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

        /// <summary>
        /// fonction utilise pour obtenir la réponse du joueur
        /// est ovveride chez ia pour sa propre implémentation
        /// </summary>
        /// <param name="leplateau"></param>
        /// <returns>
        /// un string representant la reponse du joueur
        /// </returns>
        public virtual string action(Plateau leplateau)//virtual for child override
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

        #region operators and comparators
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
        #endregion
        /// <summary>
        /// une classe dérivée de Joueur ou l'IA joue a la place d un joueur
        /// </summary>
        public class IA : Joueur
        {
            static int id=0;//un id unique pour la creation des noms
            private int sommeil = 1000;//durée de sommeil entre chaque tentative
            private Plateau leplateau;// le plateau de jeu
            private Dictionnaire ledico;// le dico des mots
            private Stack<string> motstrouve = new Stack<string>();//stack contenant les mots trouvé

            /// <summary>
            /// constructeur simple 
            /// </summary>
            /// <param name="undico">
            /// le dico de mot  a chercher
            /// </param>
            public IA(Dictionnaire undico)
            {
                this.ledico = undico;
                this._name = "IA"+Convert.ToString(id);
                id+=1;
            }
            
            /// <summary>
            /// ovveride de la methode de joueur de action ou l IA propose des mots
            /// </summary>
            /// <param name="unplateau">
            /// le plateau dejeu pour le quel les mots doivent etres cherché
            /// </param>
            /// <returns>
            /// un string représentant le choix de l'IA
            /// </returns>
            public override string action( Plateau unplateau)
            {
                string res = " ";
                if(this._found.Count==0){
                    this.leplateau = unplateau;
                    this.motstrouve = new Stack<string>();
                    foreach(string mot in this.ledico.getallWords()){
                        if(this.leplateau.Test_Plateau(mot)){
                            this.motstrouve.Push(mot);
                        }
                    }
                    sommeil = 60000/(this.motstrouve.Count+1);
                }
                this.motstrouve.TryPop(out res);
                if(res == null){
                    res = " beep boop bip bop";
                }
                Thread.Sleep(sommeil);
                Console.WriteLine(res);
                return res;
            }

        }  
    }
    
}