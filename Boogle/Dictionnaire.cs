using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Boogle
{
    /// <summary>
    /// une classe dictionnaire qui tien un lien entre un groupe de mots et une langue
    /// </summary>
    public class Dictionnaire
    {
        private SortedList<int, string[]> _dico;//la liste des mots valides
        private string _langue;//la langue du dico

        /// <summary>
        /// Le dico des mots en readonly
        /// </summary>
        /// <value></value>
        public SortedList<int, string[]> Dico
        {
            get { return _dico; }
        }

        /// <summary>
        /// un int représentant la fin du dico utile pour l'appel a la recherche recursif sans savoir ou est la fin.
        /// </summary>
        /// <param name="mot">
        /// le mot don la longueur influe sur la taille
        /// </param>
        /// <returns>
        /// un int de position dans le dico
        /// </returns>
        public int finduDico(string mot)
        {
            return _dico[mot.Length].Length;
        }

        /// <summary>
        /// constructeur complet prennant en entrée le path ver le fichier
        /// </summary>
        /// <param name="path">
        /// le chemin vers le ficheir de dictionnaire
        /// </param>
        /// <param name="langue">
        /// la langue du dictionnaire
        /// </param>
        public Dictionnaire(string path, string langue)
        {
            try
            {
                _langue = langue;
                _dico = new SortedList<int, string[]>();
                string input = File.ReadAllText(path);
                string[] dico = Regex.Split(input, @"[0-9]+");
                int i = 2;
                foreach (string item in dico)
                {
                    if (item != "")
                    {
                        _dico.Add(i, item.Replace("\r\n", "").Split(' '));//parsing des mots
                        i++;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new Exception("le fichier n'a pas ete trouvé");
            }
            catch (Exception)
            {
                throw new Exception("erreur inattendu");
            }
        }

        /// <summary>
        /// representation en string du dico
        /// </summary>
        /// <returns>
        /// un long string
        /// </returns>
        public override string ToString()
        {
            string res = "";
            for (int i = 2; i < _dico.Keys.Count; i++)
            {
                res += $"Il y a {_dico[i].Length} mots de {i} lettres\n";
            }
            return res;
        }

        /// <summary>
        /// recherche dichotomique du mot demandé dans la liste de ceux de la meme taille
        /// </summary>
        /// <param name="debut">
        /// 0
        /// </param>
        /// <param name="fin">
        /// finducdico
        /// </param>
        /// <param name="mot">
        /// mot a recherhcher
        /// </param>
        /// <returns>
        /// true if mot existe 
        /// false if mot existe pas
        /// </returns>
        public bool RechDicoRecursif(int debut, int fin, string mot)
        {
            int milieu = (debut + fin) / 2;
            if (debut <= fin)
            {
                if (_dico[mot.Length][milieu] == mot)
                {
                    return true;
                }
                else if (String.Compare(mot, _dico[mot.Length][milieu]) < 0)
                {
                    return RechDicoRecursif(debut, milieu - 1, mot);
                }
                else
                {
                    return RechDicoRecursif(milieu + 1, fin, mot);
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// implementation d'un enumerable permettant de recuperer tous les mots du dico pour l'IA
        /// </summary>
        /// <returns>
        /// tous les mots du dico
        /// </returns>
        public IEnumerable<string> getallWords(){
            foreach(var i in this._dico){//pour chaque longueur de mots 
                foreach(string mot in i.Value){//pour chaque mot de cette longueur
                   yield return mot;//retourner seulement ce mot
                }
            }
        }
    }
}