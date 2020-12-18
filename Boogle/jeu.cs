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

        /// <summary>
        /// affiche le menu du Boogle et gère la création / suppression de joueurs
        /// </summary>
        public void initilisation(){
            //ici on initialise les joueurs
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
                                    tempstring = validname();
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
                                    this.participants.Add(new Joueur.IA(this.monDico));
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
            Console.Clear();
            Console.WriteLine("Merci d'avoir joué au Boogle");
        }

        /// <summary>
        /// affiche les noms des joueurs dont le nom contient le string en input
        /// </summary>
        /// <param name="test"></param>
        private void partialmatch(string test){
            foreach(Joueur sam in this.participants){
                if(sam.Nom.Contains(test)){
                    Console.WriteLine(sam.desc);
                }
            }
        }

        /// <summary>
        /// lance une partie, affiche le nom du joueur qui doit jouer, son score, le plateau et le temps restant
        /// </summary>
        public void jouerunepartie(){
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
                    this.monPlateau.Shuffle();
                    player.Clear();
                    Console.Write("C'est au tour de ");
                    Console.Write(player.Nom);
                    Console.WriteLine(" de Jouer !!");
                    Console.WriteLine("appuyez sur enter pour commencer !!");
                    Console.ReadLine();
                    superchrono thechrono = new superchrono(durrée);
                    //on lance le superchrono dans un autre thread pour qu'il puisse fonctionner correctement
                    Thread thechronothread = new Thread(new ThreadStart(thechrono.start));
                    thechronothread.Start();
                    while(thechrono.isitoveryet){
                        Console.WriteLine(player.Nom);
                        Console.WriteLine(this.monPlateau);
                        Console.WriteLine("veuillez rentrer votre mot!!");
                        reponse = player.action(this.monPlateau);
                        if(!player.Contains(reponse)){
                            
                            if(monPlateau.Test_Plateau(reponse) && monDico.RechDicoRecursif(0,monDico.finduDico(reponse),reponse)){
                                monPlateau.disphighlighted();
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
                        //Console.Write("Il vous reste ");
                        //Console.WriteLine(durrée-chrono.Elapsed);
                    }
                    Console.WriteLine("temps écoulé !!");
                    Console.WriteLine("Appuyez sur entrer pour continuer");
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// affiche le classement à la fin de la partie
        /// </summary>
        public void leaderboard(){
            this.participants.Sort();
            foreach(Joueur player in this.participants){
                Console.WriteLine(player.desc);
            }
        }

        /// <summary>
        /// vérifie si le nom du joueur est valide (seulement des lettres ou des chiffres)
        /// </summary>
        /// <returns></returns>
        static string validname(){
            string res =Console.ReadLine();
            bool unsolved= true;
            while(unsolved){
                unsolved = false;
                foreach(char letter in res){
                    if(!char.IsLetterOrDigit(letter)){
                        Console.WriteLine("veuillez rentrer un nom avec seulement des lettres ou chiffres");
                        unsolved = true;
                        break;
                    }
                }
                if(res.Length<1){
                    Console.WriteLine("veuillez rentrer un nom avec au moins un charactère");
                    unsolved = true;
                }
                if(unsolved){
                    res = Console.ReadLine();
                }
            }
            return res;
        }
    }
}