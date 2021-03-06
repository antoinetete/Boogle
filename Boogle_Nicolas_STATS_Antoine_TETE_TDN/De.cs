using System;

namespace Boogle
{
    /// <summary>
    /// une class De représantant chque De du Boogle
    /// </summary>
    public class De
    {
        private const int nombredeface = 6;//le nombre de face par De
        private char[] _values = new char[nombredeface];//les differentes faces du De
        private char _selectedValue;//la face visible du De
        private static int lastid=0; // pour comparaison des dés
        private int myid;// mon id pour comparaison des dées
        
        /// <summary>
        /// retourne la face visible du De
        /// </summary>
        /// <value>
        /// un char
        /// </value>
        public char SelectedValue{
            get{return this._selectedValue;}
        }
        /// <summary>
        /// toutes les face sdu de pour inspection
        /// </summary>
        /// <value></value>
        public char[] Values{
            get{return _values;}
        }

        /// <summary>
        /// constructeur leger pour 100%random
        /// </summary>
        public De(Random rnd){
            for(int i=0;i<this._values.Length;i++){
                this._values[i] = (char)rnd.Next('a','z');//genere un char random
            }
            this.Lance(rnd);
            lastid++;
            myid = lastid;
        }

        /// <summary>
        /// constructeur simple
        /// </summary>
        /// <param name="values">
        /// represente les faces possible du de
        /// </param>
        /// <param name="rnd">
        /// un objet Random utilisable pour implémentation de la SEED
        /// </param>
        public De(char[] values,Random rnd)
        {
            if (values.Length == nombredeface)
            {
                foreach(char atester in values){
                    if(!char.IsLetter(atester)){
                        throw new Exception("un des char n est pas une lettre");
                    }
                }
                _values = values;
                lastid++;
                myid = lastid;
                
            }else{
                throw new Exception("Le char de valeurs n'est pas égale au nombre de face");
            }
            this.Lance(rnd);
        }
        /// <summary>
        /// constructeur simple
        /// </summary>
        /// <param name="values">
        /// represente les faces possible du de
        /// </param>
        /// <param name="rnd">
        /// un objet Random utilisable pour implémentation de la SEED
        /// </param>
        public De(string[] values, Random rnd)
        {
            if (values.Length == nombredeface)
            {
                for(int i=0;i<_values.Length;i++){
                    if(values[i].Length==1 && char.IsLetter(values[i][0])){// si char est valid
                        _values[i] = values[i][0];
                    }else{
                        throw new Exception("dice data is invalide");
                    }
                }
                lastid++;
                myid = lastid;
            }else{
                throw new Exception("Le char de valeurs n'est pas égale au nombre de face");
            }
            this.Lance(rnd);
        }

        /// <summary>
        /// affecte aléatoirement une des faces du dé à _selectedValue
        /// </summary>
        /// <param name="r">
        /// obj Random pour utilisation d une seed
        /// </param>
        public void Lance(Random r)
        {
            _selectedValue = this._values[r.Next(this._values.Length)];// nouveau selected value rnd
        }

        /// <summary>
        /// affiche une description du dé (ses faces et la face choisie)
        /// </summary>
        /// <returns>
        /// representation du De par un string tres verbose
        /// </returns>
        public override string ToString()
        {
            string res = "Ce dé possède ces 6 faces : \"";
            foreach (char item in _values)
            {
                res += $"{item} ";
            }
            res += $"\" et la valeur affichée est {_selectedValue}";
            return res;
        }
        #region  implementation des operateurs 
        public static bool operator ==(De unde, char unchar){
            return unde._selectedValue == unchar;
        }
        public static bool operator !=(De unde, char unchar){
            return unde._selectedValue != unchar;
        }
        public static bool operator ==(De A,De B){
            return A.myid==B.myid;
        }
        public static bool operator !=(De A,De B){
            return A.myid!=B.myid;
        }
        
        public override bool Equals(object obj)
        {   
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return base.Equals(obj);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}