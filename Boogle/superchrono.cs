using System;
using System.Threading;
using System.Diagnostics;
namespace Boogle
{
    public class superchrono
    {
        private int xcursor=30;
        private int ycursor=5;
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
                double temp = (this.theduration.TotalSeconds-this.chrono.Elapsed.TotalSeconds)/(this.theduration.TotalSeconds);
                temp*=100;
                int cursorleft = Console.CursorLeft;
                int cursortop = Console.CursorTop;
                Console.SetCursorPosition(xcursor,ycursor);
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
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                }
                if(temp<25){
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                if(temp<15){
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write("Il vous reste: ");
                Console.Write((this.theduration-chrono.Elapsed).Seconds);
                Console.ResetColor();
                Console.CursorLeft = cursorleft;
                Console.CursorTop = cursortop;
            }
        }
    }
}