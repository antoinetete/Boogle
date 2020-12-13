using System;
using System.Collections.Generic;
namespace Boogle
{
    public class jeu
    {
        private Dictionnaire monDico;
        private Plateau monPlateau;
        private Random rnd = new Random();
        private int nbTour=0;
        private List<Joueur> participants = new List<Joueur>();
        public jeu(string path_to_dico,string path_to_dice, int the_seed=-1){
            monDico = new Dictionnaire(path_to_dico,"FR");
            if(the_seed!=-1){
                rnd = new Random(the_seed);
            }
            monPlateau = new Plateau(path_to_dice,rnd);
            initilisation();
        }
        public void initilisation(){
            //ici on initie les joueurs
            ConsoleKeyInfo cki;
            const int exomin=1;
            const int exomax=4;
            int cursor = exomin-1;
            int offset=1;
            do
            {
                offset = 1;
                Console.Clear();
                if(participants.Count>0){
                    offset = 2+participants.Count;
                    Console.WriteLine("participants présent: ");
                    foreach (Joueur item in participants)
                    {
                        Console.WriteLine(item.desc);
                    }
                }
                Console.WriteLine("options de jeu :\n"
                                 + "  + ajouter un joueur\n"
                                 + "  + ajouter une ia\n"
                                 + "  - enlever un participant\n"
                                 + "  !-- c'est parti --!\n"
                                 + "\n");
                Console.WriteLine("naviguez avec les flèches");
                Console.SetCursorPosition(0,cursor+offset);
                cki = Console.ReadKey(intercept:true);
                switch(cki.Key){
                    case ConsoleKey.UpArrow:
                        cursor--;
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        cursor++;
                        switch (cursor)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi de rajouter un joueur !!");
                                    Console.WriteLine("veuillez rentrer un nom: ");
                                    participants.Add(new Joueur(Console.ReadLine()));
                                    Console.Clear();
                                    Console.WriteLine("vous avez ajouté un joueur !!");
                                    Console.WriteLine("appuyez sur enter pour continuer");
                                    Console.ReadLine();
                                    break;
                                case 2:
                                    throw new NotImplementedException();
                                    break;
                                case 3:
                                    throw new NotImplementedException();
                                    break;
                                case 4:
                                    throw new NotImplementedException();
                                    break;
                            }
                        break;
                    default:
                        break;
                }
                cursor = ((cursor+exomax)%(exomax));
            } while (cki.Key != ConsoleKey.Escape);
            throw new NotImplementedException();
        }
    }
}