using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fully_fuzzy_lpp
{
  class Program
  {
      static void Main(string[] args)
      {
         int type, cons, vars;
         int _cons = 0 , _vars = 0;

         // declaration of arrays to hold the values
         //Golden Rule := [a,b,c] = [row, element, tier + rank]
         //float[,,] eqn22 = new float[3,5,5]; 
         //float[,,] eqn23 = new float[3,6,5]; //this is for 2 cons and 3 vars
         //float[,,] eqn32 = new float[4,5,5]; //this is for 3 cons and 2 vars
         //float[,,] eqn33 = new float[4,6,5]; //this is for 3 cons and 3 vars
         float[,,] eqn22 = { //this is for 2 cons and 2 vars
            {{-2,-3,4,5,0},{1,6,-7,10,0},{0,0,0,0,0},{0,0,0,0,0},{3,20,-32,40,0}},
            {{4,5,6,7,0},{7,8,9,-11,0},{0,0,0,0,0},{0,0,0,0,0},{9,-30,41,-50,0}},
            {{1,3,5,-9,0},{2,7,12,-15,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}}
         };

         float[,,] eqn23 = { //this is for 2 cons and 3 vars
            {{2,3,4,5,0},{1,6,7,10,0},{6,7,8,10,0},{0,0,0,0,0},{0,0,0,0,0},{3,20,32,40,0}},
            {{4,5,6,7,0},{7,8,9,11,0},{9,10,11,13,0},{0,0,0,0,0},{0,0,0,0,0},{9,30,41,50,0}},
            {{1,3,5,9,0},{2,7,12,15,0},{8,9,10,12,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}}
         };

         float[,,] eqn32 = { //this is for 3 cons and 2 vars
            {{2,3,4,5,0},{1,6,7,10,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{3,20,32,40,0}},
            {{4,5,6,7,0},{7,8,9,11,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{9,30,41,50,0}},
            {{7,8,9,10,0},{5,6,7,14,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{7,28,39,48,0}},
            {{1,3,5,9,0},{2,7,12,15,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}}
         };

         float[,,] eqn33 = { //this is for 3 cons and 3 vars
            {{1,2,3,4,0},{5,6,7,8,0},{9,10,11,12,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{1,2,3,4,0}},
            {{5,6,7,8,0},{9,10,11,12,0},{0,1,2,3,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{4,5,6,7,0}},
            {{8,9,10,11,0},{1,2,3,4,0},{5,6,7,8,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{9,10,12,15,0}},
            {{2,3,4,5,0},{6,7,8,9,0},{10,12,15,17,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}}
         };

         float[,,] tab1 = new float[3,5,5]; //this is for 2 cons and 2 vars
         float[,,] tab2 = new float[3,6,5]; //this is for 2 cons and 3 vars
         float[,,] tab3 = new float[4,5,5]; //this is for 3 cons and 2 vars
         float[,,] tab4 = new float[4,7,5]; //this is for 3 cons and 3 vars
         bool optimal;


         ///////////////////////////////////////////////////////////////////////////////////
         Console.WriteLine("Select The Type of Fully Fuzzy Linear Program:\n1) Arranged \t\t2) Unarranged");
         type = int.Parse(Console.ReadLine());
         Console.WriteLine("What number of Constraints:\n1) 2 \t\t2) 3");
         cons = int.Parse(Console.ReadLine());
         Console.WriteLine("What number of Variables Per Constraint:\n1) 2 \t\t2) 3");
         vars = int.Parse(Console.ReadLine());

         //ReWriting
         if(cons == 1){
            _cons = 2;
         }else if(cons == 2) {
            _cons = 3;
         }
         if(vars == 1){
            _vars = 2;
         }else if(vars == 2) {
            _vars = 3;
         }

         // The  Main Program begins here
         switch (cons){
            case 1:
               switch (vars){
                  case 1: // 2 cons 2 vars
                     tableau1 tb22 = new tableau1(eqn22,type,_cons,_vars);
                     tb22.toCompForm();
                     tb22.rank();
                     //gives optimal the value of the isOptimal method of the current tableau
                     optimal = tb22.isOptimal();
                     //while(optimal == false){
                     for (;optimal == false;)
                     {
                        if(tb22.isOptimal()){
                           //stop
                           tb22.preview();
                           Console.WriteLine("Tableau is Optimal!!!\n");
                           optimal = true;
                           break;
                        }else{
                           //calls the fuzzy function to carry out the phase of generating a new tableau
                           tb22.preview();
                           Console.WriteLine("P.E = {0}\n", tb22.getPE(tb22.getPC(), tb22.getPR(tb22.getPC())));
                           tab1 = tb22.tab;
                           tb22 = fuzzy1(tb22,type,cons,vars,tab1);
                           Console.WriteLine("Tableau is not Optimal\n");
                           optimal = false;
                        }
                     }  
                     //}
                  break;
                  case 2: // 2 cons 3 vars
                     tableau2 tb23 = new tableau2(eqn23,type,_cons,_vars);
                     tb23.toCompForm();
                     tb23.rank();
                     //gives optimal the value of the isOptimal method of the current tableau
                     optimal = tb23.isOptimal();
                     while(optimal == false){
                        if(tb23.isOptimal()){
                           //stop
                           tb23.tabview();
                           Console.WriteLine("Tableau is Optimal!!!\n");
                           optimal = true;
                           break;
                        }else{
                           //calls the fuzzy function to carry out the phase of generating a new tableau
                           tb23.tabview();
                           Console.WriteLine("P.E = {0}\n", tb23.getPE(tb23.getPC(), tb23.getPR(tb23.getPC())));
                           tab2 = tb23.tab;
                           tb23 = fuzzy2(tb23,type,cons,vars,tab2);
                           Console.WriteLine("Tableau is not Optimal\n");
                           optimal = false;
                        }
                     }
                  break;
               }
            break;
            case 2:
               switch (vars){
               case 1:
                  tableau3 tb32 = new tableau3(eqn32,type,cons,vars);
                  tb32.toCompForm();
                  tb32.rank();
                  //gives optimal the value of the isOptimal method of the current tableau
                  optimal = tb32.isOptimal();
                  while(optimal == false){
                     if(tb32.isOptimal()){
                        //stop
                        tb32.tabview();
                        Console.WriteLine("Tableau is Optimal!!!\n");
                        optimal = true;
                        break;
                     }else{
                        //calls the fuzzy function to carry out the phase of generating a new tableau
                        tb32.tabview();
                        Console.WriteLine("P.E = {0}\n", tb32.getPE(tb32.getPC(), tb32.getPR(tb32.getPC())));
                        tab3 = tb32.tab;
                        tb32 = fuzzy3(tb32,type,cons,vars,tab3);
                        Console.WriteLine("Tableau is not Optimal\n");
                        optimal = false;
                     }
                  }
               break;
               case 2:
                  tableau4 tb33 = new tableau4(eqn33,type,cons,vars);
                  tb33.toCompForm();
                  tb33.rank();
                  //gives optimal the value of the isOptimal method of the current tableau
                  optimal = tb33.isOptimal();
                  /* tb33.tabview();
                  Console.WriteLine("P.C = {0}, P.R = {1}, P.E = {2}\n", tb33.getPC(), tb33.getPR(tb33.getPC()), tb33.getPE(tb33.getPC(), tb33.getPR(tb33.getPC()))); */
                  while(optimal == false){
                     if(tb33.isOptimal()){
                        //stop
                        tb33.tabview();
                        Console.WriteLine("Tableau is Optimal!!!\n");
                        optimal = true;
                        break;
                     }else{
                        //calls the fuzzy function to carry out the phase of generating a new tableau
                        tb33.tabview();
                        Console.WriteLine("P.E = {0}\n", tb33.getPE(tb33.getPC(), tb33.getPR(tb33.getPC())));
                        tab4 = tb33.tab;
                        tb33 = fuzzy4(tb33,type,cons,vars,tab4);
                        Console.WriteLine("Tableau is not Optimal\n");
                        optimal = false;
                     }
                  }
               break;
               }
            break;
         }
      }

      static tableau1 fuzzy1(tableau1 tab,int type,int cons,int vars,float[,,] tabb){
         int pc = tab.getPC();
         int pr = tab.getPR(pc);
         float pe = tab.getPE(pc,pr);
         
         //creating a new tableau instance
         tableau1 _tb = new tableau1(tabb,type,cons,vars);
         
         //then the instance is used to generate a new table, i.e tableau 2
         // and is assigned to the public tab1
			_tb.getNewTableau(tabb,pc,pr,pe);
         _tb.rank();
         return _tb;
      }

      static tableau2 fuzzy2(tableau2 tab,int type,int cons,int vars,float[,,] tabb){
         int pc = tab.getPC();
         int pr = tab.getPR(pc);
         float pe = tab.getPE(pc,pr);
         
         //creating a new tableau instance
         tableau2 _tb = new tableau2(tabb,type,cons,vars);
         
         //then the instance is used to generate a new table, i.e tableau 2
         // and is assigned to the public tab1
			_tb.getNewTableau(tabb,pc,pr,pe);
         _tb.rank();
         return _tb;
      }

      static tableau3 fuzzy3(tableau3 tab,int type,int cons,int vars,float[,,] tabb){
         int pc = tab.getPC();
         int pr = tab.getPR(pc);
         float pe = tab.getPE(pc,pr);
         
         //creating a new tableau instance
         tableau3 _tb = new tableau3(tabb,type,cons,vars);
         
         //then the instance is used to generate a new table, i.e tableau 2
         // and is assigned to the public tab1
         _tb.getNewTableau(tabb,pc,pr,pe);
			_tb.rank();
         return _tb;
      }

      static tableau4 fuzzy4(tableau4 tab,int type,int cons,int vars,float[,,] tabb){
         int pc = tab.getPC();
         int pr = tab.getPR(pc);
         float pe = tab.getPE(pc,pr);
         
         //creating a new tableau instance
         tableau4 _tb = new tableau4(tabb,type,cons,vars);
         
         //then the instance is used to generate a new table, i.e tableau 2
         // and is assigned to the public tab1
         _tb.getNewTableau(tabb,pc,pr,pe);
			_tb.rank();
         return _tb;
		}
   }
}