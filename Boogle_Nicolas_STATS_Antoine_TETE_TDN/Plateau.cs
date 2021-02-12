using System;
using System.IO;
using System.Collections.Generic;

namespace Boogle
{
    public class Plateau
    {
        private const int size = 4;
        private De[,] contenu = new De[size, size];
        private Random rnd = new Random();
        private List<De> latestvisit = new List<De>();

        /// <summary>
        /// renvoit le plateau de De convertit en matrice de char (renvoit la selectedValue de chaque De)
        /// </summary>
        public char[,] Contenu
        {
            get
            {
                char[,] res = new char[this.contenu.GetLength(0), this.contenu.GetLength(1)];
                for (int i = 0; i < this.contenu.GetLength(0); i++)
                {
                    for (int j = 0; j < this.contenu.GetLength(1); j++)
                    {
                        res[i, j] = contenu[i, j].SelectedValue;
                    }
                }
                return res;
            }
        }

        public Plateau()
        {
        }

        /// <summary>
        /// constructeur léger qui randomise les des
        /// </summary>
        public Plateau(Random rnd)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    this.contenu[x, y] = new De(rnd);
                }
            }
        }

        /// <summary>
        /// crée un plateau de dés avec les dés fournis dans le fichier rentré en paramètre
        /// </summary>
        /// <param name="path"></param>
        /// <param name="rnd"></param>
        public Plateau(string path, Random rnd)
        {
            StreamReader thereader = null;
            List<De> myDice = new List<De>();
            try
            {
                thereader = new StreamReader(path);
            }
            catch (FileNotFoundException)
            {
                throw new Exception("le fichier de initilisation des Dés n'a pas été trouvé");
            }
            while (!thereader.EndOfStream)
            {
                myDice.Add(new De(thereader.ReadLine().Split(';'), rnd));
            }
            thereader.Close();
            if (myDice.Count != size * size)
            {
                throw new Exception("wrong amount of dice was provided");
            }
            for (int i = 0; i < myDice.Count; i++)
            {
                this.contenu[i % size, i / size] = myDice[i];
            }
        }

        /// <summary>
        /// transcrit le plateau de De en string
        /// </summary>
        /// <returns>le plateau de sous forme de string en choisissant la SelectedValue du dé pour chaque case</returns>
        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < this.contenu.GetLength(0); i++)
            {
                for (int j = 0; j < this.contenu.GetLength(1); j++)
                {
                    res += this.contenu[i, j].SelectedValue;
                    res += " ";
                }
                res += "\n";
            }
            return res;
        }

        /// <summary>
        /// vérifie si un mot peut être formé dans le plateau
        /// </summary>
        /// <param name="mot"></param>
        /// <returns>true si c'est le cas, false sinon</returns>
        public bool Test_Plateau(string mot)
        {
            bool res = false;
            this.latestvisit.Clear();
            if (mot != null)  //si quelque chose est rentré en paramètre
            {
                if (mot.Length >= 3) //si le mot fait au moins 3 lettres
                {
                    for (int i = 0; i < size; i++)
                    { //on parcours tout le plateau
                        for (int j = 0; j < size; j++)
                        {
                            res = Test_coord(i, j, mot, 0, new List<De>());//la recurrance est ici
                            //on vérifie si on peut former le mot à partir des lettres disponibles dans le plateau
                            if (res)
                            {
                                return res;
                            }
                        }
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// fonction récursive qui vérfie si la lettre à une coordonée est la suivante d'un mot
        /// </summary>
        /// <param name="i">coordonée à tester</param>
        /// <param name="j">coordonée à tester</param>
        /// <param name="mot">mot qu'on cherche</param>
        /// <param name="index">charactère qu'on cherche du mot qu'on cherche</param>
        /// <param name="visité">liste des coordonnées déjà utilisées pour faire le mot</param>
        /// <returns>true si c'est le cas, false sinon</returns>
        private bool Test_coord(int i, int j, string mot, int index, List<De> visité)
        {
            bool res = false;
            if (index == mot.Length) //si index == mot.Length alors le mot peut etre créé à partir du plateau
            {
                res = true;
                this.latestvisit = visité;
            }
            else
            {
                if ((this.contenu[i, j] == mot[index]) && (!visité.Contains(this.contenu[i, j]))) //si la lettre correspond à la suivante dans le mot et qu'elle n'est pas déjà utilisée
                {
                    visité.Add(this.contenu[i, j]); //on l'ajoute à la liste des cases visitées
                    if (index < (mot.Length)) //si on est pas arrivé a la fin
                    {
                        //on teste les voisins de la cellule
                        for (int l = -1; l < 2; l++)
                        {
                            for (int m = -1; m < 2; m++)
                            {
                                if ((l != 0 || m != 0) && valid(i + l, j + m)) //si dans le plateau et différend de la lettre dont on cherche les voisins
                                {
                                    //on rappelle Test_coord qui regarde si la lettre suivante du mot est dans les voisins de la lettre qu'on vient de tester
                                    res |= Test_coord(i + l, j + m, mot, index + 1, new List<De>(visité));//must be a copy of visité otherwise confuses the stacking
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// affiche le plateau en blanc avec les lettres utilisées pour faire le dernier mot en vert
        /// </summary>
        public void disphighlighted()
        {
            for (int i = 0; i < this.contenu.GetLength(0); i++)
            {
                for (int j = 0; j < this.contenu.GetLength(1); j++)
                {
                    if (this.latestvisit.Contains(this.contenu[i, j])) //si la lettre est utilisée pour faire le mot on l'affiche en vert
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(this.contenu[i, j].SelectedValue);
                    Console.Write(" ");
                    Console.ResetColor();
                }
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// lance les dés de la matrice
        /// </summary>
        public void Shuffle()
        {
            for (int i = 0; i < this.contenu.GetLength(0); i++)
            {
                for (int j = 0; j < this.contenu.GetLength(1); j++)
                {
                    this.contenu[i, j].Lance(rnd);
                }
            }
        }

        /// <summary>
        /// regarde si les coordonées appartiennent à la matrice
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool valid(int i, int j)
        { //? should this be static ?
            return (i < size) && (j < size) && (i >= 0) && (j >= 0);
        }
    }
}