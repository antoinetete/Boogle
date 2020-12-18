using System;
using System.Threading;
using System.Diagnostics;
namespace Boogle
{
    public class superchrono
    {
        private int xcursor=2;
        private int ycursor=0;
        private Stopwatch chrono = new Stopwatch();
        private TimeSpan theduration;
        public bool isitoveryet{
            get{return chrono.Elapsed < theduration;}
        }
        public superchrono(TimeSpan duration){
            this.theduration = duration;
        }
        
        public void start(){
            chrono.Restart();
            while(this.isitoveryet){
                Thread.Sleep(100);
                string empty ="Il vous reste: "+Convert.ToString((this.theduration-chrono.Elapsed).Seconds)+"s";
                double temp = (this.theduration.TotalSeconds-this.chrono.Elapsed.TotalSeconds)/(this.theduration.TotalSeconds);
                temp*=100;
                int cursorleft = Console.CursorLeft;
                int cursortop = Console.CursorTop;
                Console.SetCursorPosition(Console.WindowWidth - empty.Length - xcursor,ycursor);
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
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Le temps est écoulé !!!");
            Console.ResetColor();
        }
    }
}