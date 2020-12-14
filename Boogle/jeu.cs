using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace Boogle
{
    public class jeu
    {
        static int nbredetour=4;
        private Dictionnaire monDico;
        private Plateau monPlateau;
        private Random rnd = new Random();
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
                    leaderboard();
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
                                string tempstring;
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi de rajouter un joueur !!");
                                    Console.WriteLine("veuillez rentrer un nom: ");
                                    tempstring = Console.ReadLine();
                                    Console.Clear();
                                    if(this.participants.Contains(new Joueur(tempstring))){
                                        Console.WriteLine("ce joueur existe déja, veuillez choisir un autre nom");
                                    }else{
                                        participants.Add(new Joueur(tempstring));
                                        Console.WriteLine("vous avez ajouté un joueur !!");
                                    }
                                    Console.WriteLine("appuyez sur enter pour continuer");
                                    Console.ReadLine();
                                    break;
                                case 2:
                                    throw new NotImplementedException();
                                    break;
                                case 3:
                                    string building="";

                                    Console.Clear();
                                    Console.WriteLine("vous avez choisi de retirer un joueur");
                                    Console.WriteLine("veuillez renter le nom du joueur a retirer: ");
                                    Console.WriteLine(building);
                                    this.partialmatch(building);
                                    ConsoleKeyInfo input = Console.ReadKey(intercept:true);
                                    while (input.Key != ConsoleKey.Enter){
                                        
                                        if(input.Key == ConsoleKey.Backspace && building.Length>0){
                                            building = building.Remove(building.Length-1);
                                        }else{
                                            if( char.IsLetterOrDigit(input.KeyChar)){
                                                building+=input.KeyChar;
                                            }
                                        }
                                        Console.Clear();
                                        Console.WriteLine("vous avez choisi de retirer un joueur");
                                        Console.WriteLine("veuillez renter le nom du joueur a retirer: ");
                                        Console.WriteLine(building);
                                        this.partialmatch(building);
                                        input = Console.ReadKey(intercept:true);
                                    }
                                    Console.Clear();
                                    if(this.participants.Contains(new Joueur(building))){
                                        participants.Remove(new Joueur(building));
                                        Console.WriteLine("le Joueur a été retiré avec succées");
                                    }else{
                                        Console.WriteLine("aucun joueur avec ce nom a été trouvé");
                                    }
                                    Console.WriteLine("veuillez apuyer sur enter pour continuer");
                                    Console.ReadLine();
                                    break;
                                case 4:
                                    if(this.participants.Count>0){
                                        this.jouerunepartie();
                                    }else{
                                        Console.Clear();
                                        Console.WriteLine("Il faut minimum 1 joueur pour jouer une partie");
                                        Console.WriteLine("Appuyez sur enter pour continuer");
                                        Console.ReadLine();
                                    }
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
        private void partialmatch(string test){
            foreach(Joueur sam in this.participants){
                if(sam.Nom.Contains(test)){
                    Console.WriteLine(sam.desc);
                }
            }
        }
        public void jouerunepartie(){
            Stopwatch chrono = new Stopwatch();
            TimeSpan durrée = new TimeSpan(0,1,0);
            string reponse="";
            int points_marqué=0;
            for(int i=0;i<nbredetour;i++){
                Console.Clear();
                Console.Write("Début du Round ");
                Console.WriteLine(i);
                Console.WriteLine(" ");
                Console.WriteLine("Le score est de: ");
                leaderboard();
                Console.WriteLine(" ");
                Console.WriteLine("appuyez sur enter pour continuer");
                Console.ReadLine();
                foreach(Joueur player in this.participants){
                    Console.Clear();
                    Console.Write("C'est au tour de ");
                    Console.Write(player.Nom);
                    Console.WriteLine(" de Jouer !!");
                    Console.WriteLine("appuyez sur enter pour commencer !!");
                    Console.ReadLine();
                    chrono.Restart();
                    while(chrono.Elapsed< durrée){
                        Console.WriteLine(player.Nom);
                        Console.WriteLine(this.monPlateau);
                        Console.WriteLine("veuillez rentrer votre mot!!");
                        reponse = player.action();
                        if(!player.Contains(reponse)){
                            
                            if(monPlateau.Test_Plateau(reponse) && monDico.RechDicoRecursif(0,monDico.finduDico(reponse),reponse)){
                                points_marqué = player.Add_mot(reponse);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Le mot est valide et ");
                                Console.Write(player.Nom);
                                Console.Write(" marque ");
                                Console.Write(points_marqué);
                                Console.WriteLine("points!!");
                                Console.ResetColor();
                            }else{
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("mince le mot n'est pas valide");
                                Console.ResetColor();
                            }
                        }else{
                            Console.WriteLine("vous avez déja rentré ce mot !!");
                        }
                        Console.Write("Il vous reste ");
                        Console.WriteLine(durrée-chrono.Elapsed);
                    }
                    Console.WriteLine("temps écoulé !!");
                    Console.WriteLine("Appuyez sur entrer pour continuer");
                    Console.ReadLine();
                }
            }
        }
        public void leaderboard(){
            this.participants.Sort();
            foreach(Joueur player in this.participants){
                Console.WriteLine(player.desc);
            }
        }
    }
}