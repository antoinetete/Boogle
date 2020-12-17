using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Boogle
{
    public class Plateau
    {
        private const int size = 4;
        private De[,] contenu = new De[size, size];
        private Random rnd = new Random();
        private List<De> latestvisit = new List<De>();

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


        public Plateau(){
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

        public bool Test_Plateau(string mot)
        {
            bool res = false;
            this.latestvisit.Clear();
            if(mot!=null)
            {
                if (mot.Length >= 3)
                {
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            res = Test_coord(i, j, mot, 0, new List<De>());//la recurrance est ici
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
        private bool Test_coord(int i, int j, string mot, int index, List<De> visité)
        {
            bool res = false;
            if (index == mot.Length)
            {
                res = true;
                this.latestvisit = visité;
            }
            else
            {
                if ((this.contenu[i, j] == mot[index]) && (!visité.Contains(this.contenu[i, j])))
                {
                    visité.Add(this.contenu[i, j]);
                    if (index < (mot.Length))//si on est pas arrivé a la fin
                    { 
                        for (int l = -1; l < 2; l++)//test all my neighbours
                        {
                            for (int m = -1; m < 2; m++)
                            {
                                if ((l != 0 || m != 0) && valid(i + l, j + m))//si dans le plateau
                                {
                                    res |= Test_coord(i + l, j + m, mot, index + 1, new List<De>(visité));//must be a copy of visité otherwise confuses the stacking
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }
        public void disphighlighted(){
            for (int i = 0; i < this.contenu.GetLength(0); i++)
            {
                for (int j = 0; j < this.contenu.GetLength(1); j++)
                {
                    if(this.latestvisit.Contains(this.contenu[i,j])){
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(this.contenu[i,j].SelectedValue);
                    Console.Write(" ");
                    Console.ResetColor();
                }
                Console.WriteLine("");
            }
        }
        public void Shuffle(){
            for (int i = 0; i < this.contenu.GetLength(0); i++)
            {
                for (int j = 0; j < this.contenu.GetLength(1); j++)
                {
                    this.contenu[i,j].Lance(rnd);
                }
            }
        }
        private bool valid(int i, int j)
        { //? should this be static ?
            return (i < size) && (j < size) && (i >= 0) && (j >= 0);
        }
    }
}