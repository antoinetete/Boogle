using System;

namespace Boogle
{
    class De
    {
        private const int nombredeface = 6;
        private char[] _values = new char[nombredeface];
        private char _selectedValue;
        

        /// <summary>
        /// constructeur leger pour 100%random
        /// </summary>
        public De(Random rnd){
            for(int i=0;i<this.values.Length;i++){
                this._values[i] = (char)rnd.Next('a','z');
            }
            this.Lance(rnd);
        }

        public De(char[] values)
        {
            if (values.Length == nombredeface)
            {
                _values = values;
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
        public static De operator ==(De unde, char unchar){
            return under._selectedValue == unchar;
        }
        public static De operator !=(De unde, char unchar){
            return under._selectedValue != unchar;
        }
    }
}