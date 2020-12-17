using System;

namespace Boogle
{
    public class De
    {
        private const int nombredeface = 6;
        private char[] _values = new char[nombredeface];
        private char _selectedValue;
        private static int lastid=0; 
        private int myid;
        
        public char SelectedValue{
            get{return this._selectedValue;}
        }
        public char[] Values{
            get{return _values;}
        }

        /// <summary>
        /// constructeur leger pour 100%random
        /// </summary>
        public De(Random rnd){
            for(int i=0;i<this._values.Length;i++){
                this._values[i] = (char)rnd.Next('a','z');
            }
            this.Lance(rnd);
            lastid++;
            myid = lastid;
        }

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
        public De(string[] values, Random rnd)
        {
            if (values.Length == nombredeface)
            {
                for(int i=0;i<_values.Length;i++){
                    if(values[i].Length==1 && char.IsLetter(values[i][0])){
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
        /// <param name="r"></param>
        public void Lance(Random r)
        {
            _selectedValue = this._values[r.Next(this._values.Length)];
        }

        /// <summary>
        /// affiche une description du dé (ses faces et la face choisie)
        /// </summary>
        /// <returns></returns>
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
    }
}