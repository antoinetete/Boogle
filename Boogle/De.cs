using System;

namespace Boogle
{
    class De
    {
        private const int nombredeface = 6;
        private char[] _values = new char[nombredeface];
        private char _selectedValue;
        private static int lastid=0; 
        private int myid;
        
        public char SelectedValue{
            get{return this._selectedValue;}
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

        public De(char[] values)
        {
            if (values.Length == nombredeface)
            {
                _values = values;
                lastid++;
                myid = lastid;
            }else{
                throw new Exception("Le char de valeurs n'est pas égale au nombre de face");
            }
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
            
            // TODO: write your implementation of Equals() here
            
            return base.Equals(obj);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return base.GetHashCode();
        }
    }
}