using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoUno
{
    class Program
    {
        static void Main(string[] args)
        {
            int players;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("####################################################\n" +
                              "##                       UNO                      ##\n" +
                              "##      PROGRAMAÇÃO ORIENTADA A OBJETOS - UVV     ##\n" +
                              "##                           by: Jailton Monteiro ##\n" +
                              "##                                                ##\n" +
                              "##                                    Versao 1.00 ##\n" +
                              "####################################################\n" 
                              );

            try
            {
                Console.WriteLine("Informe o numero de Jogadores: ");
                players = Convert.ToInt32(Console.ReadLine());
                if (players > 7)
                {
                    throw new ArgumentException();
                }
                else if (players < 2)
                {
                    throw new ArgumentException();
                }
                Jogando jogando = new Jogando(players);

                jogando.Jogar();
            
            }    
            catch(ArgumentException e)
            {
                MessageBox.Show("Valor fora do intervalo permitido (minimo 2 e maximo 7 jogadores): " + e.ToString() + MessageBoxButtons.OK);
                //Console.WriteLine(e);
            }

            Console.ReadKey();

        }
    }
}
