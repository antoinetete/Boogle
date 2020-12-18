using System;
using System.Threading;
using System.Diagnostics;
namespace Boogle
{
    public class superchrono
    {
<<<<<<< Updated upstream
        private int xcursor=0;//offset horizontal du curseur
        private int ycursor=0;//offset vertical du curseur
        private Stopwatch chrono = new Stopwatch();//le chrono utilisé pour chronometrer
        private TimeSpan theduration;//la duration d un round

        /// <summary>
        /// évalue si il reste du temps ou non
        /// </summary>
        /// <value>
        /// true if time remains
        /// false if time is up
        /// </value>
=======
        private int xcursor=0;
        private int ycursor=0;
        private Stopwatch chrono = new Stopwatch();
        private TimeSpan theduration;

        /// <summary>
        /// regarde si le temps est écoulé
        /// </summary>
        /// <value>true si c'est le cas, false sinon</value>
>>>>>>> Stashed changes
        public bool isitoveryet{
            get{return chrono.Elapsed < theduration;}
        }

<<<<<<< Updated upstream
        /// <summary>
        /// constructeur simple prennant en entré la duration d un round
        /// </summary>
        /// <param name="duration">
        /// la durrée d un round
        /// </param>
=======
>>>>>>> Stashed changes
        public superchrono(TimeSpan duration){
            this.theduration = duration;
        }
        
        /// <summary>
<<<<<<< Updated upstream
        /// démarre le chrono et gére l affichage du temps qui s'écoule
=======
        /// 
>>>>>>> Stashed changes
        /// </summary>
        public void start(){
            chrono.Restart();
            while(this.isitoveryet){ //tant que le chrono n'est pas arrivé à 0
                Thread.Sleep(100); //attend 1s
                string empty ="Il vous reste: "+Convert.ToString((this.theduration-chrono.Elapsed).Seconds)+"s";
                empty = empty.PadLeft(18); //taille de la ligne avec un chrono à 2 chiffres
                //on calcule le temps restant (en pourcentage)
                double temp = (this.theduration.TotalSeconds-this.chrono.Elapsed.TotalSeconds)/(this.theduration.TotalSeconds);
                temp*=100;
                int cursorleft = Console.CursorLeft;
                int cursortop = Console.CursorTop;
                Console.SetCursorPosition(Console.WindowWidth - empty.Length - xcursor,Console.WindowTop-ycursor); //on met le curseur en haut à droite de la fenetre
                //on définit une couleur d'affichage selon le temps restant
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                if(temp<80){
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                if(temp<50){
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if(temp<40){
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                if(temp<30){
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                if(temp<15){
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(empty);
                Console.ResetColor();
                Console.CursorLeft = cursorleft;
                Console.CursorTop = cursortop;
            } //une fois le chrono on finit on écrit que c'est fini
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Le temps est écoulé !!!");
            Console.ResetColor();
        }
    }
}