using System;

namespace Boogle
{
    class De
    {
        private char[] _values;
        private char _selectedValue;

        public De(char[] values)
        {
            if (values.Length == 6)
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
            _selectedValue = _values[r.Next(7)];
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
    }
}