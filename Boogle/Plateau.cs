using System.Collections.Generic;
namespace Boogle
{
    public class Plateau
    {
        private const int size=4;
        private De[,] contenu = new De[size,size];

        /// <summary>
        /// constructeur léger qui randomise les des
        /// </summary>
        public Plateau(random rnd){
            for( int x=0;x<this.size;x++){
                for( int y=0;y<this.size;y++){
                    this.contenu[x,y] = new De(rnd);
                }
            }
        }
        public override string ToString()
        {
            string res="";
            for(int i=0;i<this.contenu.GetLength(0);i++){
                for(int j=0;j<this.contenu.GetLength(1);j++){
                    res+=this.contenu[i,j];
                    res+=" ";
                }
                res+="\n";
            }
            return res;
        }

        public bool Test_Plateau(string mot){
            bool res = false;
            List<De> visité= new List<De>();
            if(mot.Length>=3){
                for(int i=0;i<this.contenu.GetLength(0);i++){
                    for(int j=0;j<this.contenu.GetLength(1);j++){
                        res = Test_coord(i,j,mot,0,visité);//la recurrance est ici
                    }
                }
            }
            return res;
        }
        private bool Test_coord(int i, int j, string mot, int index, List<De> visité){
            bool res = false;
            if(valid(i,j) && (this.contenu[i,j]==mot[index]) && (!visité.Contains(this.contenu[i,j]))){
                visité.Add(this.contenu[i,j]);
                if(index<mot.Length-1){//si on est pas arrivé a la fin 
                    res = Test_coord(i-1,j,mot,++index, new List<De>(visité)) || Test_coord(i+1,j,mot,++index, new List<De>(visité))|| Test_coord(i,j-1,mot,++index, new List<De>(visité))|| Test_coord(i,j+1,mot,++index, new List<De>(visité));//desolé cette ligne est un peu longue...
                }else{//on est arrivé a la fin 
                    res = true;
                }
            }
            return res;
        }

        private bool valid(int i, int j){ //? should this be static ?
            return (i<this.size)&&(j<this.size)&&(i>=0)&&(j>=0);
        } 
    }
}