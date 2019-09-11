using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoUno
{
    public class Jogando
    {
        public List<Player> Jogadores { get; set; }
        public Baralho pilhaCompra { get; set; }
        public List<Carta> pilhaDescarte { get; set; }

        public Jogando(int qtdPlayers)
        {
            Jogadores = new List<Player>();
            pilhaCompra = new Baralho();
            pilhaCompra.Embaralhar();
                                   
            for(int i = 0; i < qtdPlayers; i++)
            {
                Jogadores.Add(new Player { Posicao = i });
            }

            int maximoDeCartas = 7 * Jogadores.Count;
            int cartasJogadas = 0;

            while (cartasJogadas < maximoDeCartas)
            {
                for (int i = 0; i < qtdPlayers; i++)
                {
                    Jogadores[i].deck.Add(pilhaCompra.Cartas.First());
                    pilhaCompra.Cartas.RemoveAt(0);
                    cartasJogadas++;
                }
            }

            pilhaDescarte = new List<Carta>();
            pilhaDescarte.Add(pilhaCompra.Cartas.First());
            pilhaCompra.Cartas.RemoveAt(0);

            while (pilhaDescarte.First().Valor == ValorCarta.TrocaCor || pilhaDescarte.First().Valor == ValorCarta.CompraQuatro)
            {
                pilhaDescarte.Insert(0, pilhaCompra.Cartas.First());
                pilhaCompra.Cartas.RemoveAt(0);
            }
        }

        public void Jogar()
        {
            int i = 0;
            bool passado = true;

            //Exibindo deck dos jogadores
            foreach(var jogador in Jogadores)
            {
                jogador.exibirDeck();
            }

            Console.ReadLine();

            Rodada rodadaAtual = new Rodada() { Resultado = ResultadoRodada.JogoIniciado, carta = pilhaDescarte.First(), corDeclarada = pilhaDescarte.First().Cor };

            Console.WriteLine("Carta de inicio: " + rodadaAtual.carta.valorExibibo);

            while(!Jogadores.Any(x => !x.deck.Any()))
            {
                if (pilhaCompra.Cartas.Count < 4)
                {
                    var cartaAtual = pilhaDescarte.First();

                    //Embaralhar pilha de descarte para mover pra compra
                    pilhaCompra.Cartas = pilhaDescarte.Skip(1).ToList();
                    pilhaCompra.Embaralhar();

                    //Deixa apenas a ultima carta jogada na pilha de descarte
                    pilhaDescarte = new List<Carta>();
                    pilhaDescarte.Add(cartaAtual);

                    Console.WriteLine("Cartas Embaralhadas");
                }

                var jogadorAtual = Jogadores[i];

                rodadaAtual = Jogadores[i].jogadaAtual(rodadaAtual, pilhaCompra);

                descartarCarta(rodadaAtual);

                if (rodadaAtual.Resultado == ResultadoRodada.InverteJogo)
                {
                    passado = !passado;
                }

                if (passado)
                {
                    i++;
                    if (i >= Jogadores.Count) //zera contador de player
                    {
                        i = 0;
                    }
                }
                else
                {
                    i--;
                    if (i < 0)
                    {
                        i = Jogadores.Count - 1;
                    }
                }
            }
            var vencedor = Jogadores.Where(x => !x.deck.Any()).First();

            MessageBox.Show("Jogador " + vencedor.Posicao.ToString() + " Ganhou 🏆🏆🏆" + MessageBoxButtons.OK
                );
            Console.WriteLine("Jogador " + vencedor.Posicao.ToString() + " Ganhou!");

            Console.ReadKey();

        }
        

        private void descartarCarta(Rodada rodadaAtual)
        {
            if(rodadaAtual.Resultado == ResultadoRodada.CartaComum 
                || rodadaAtual.Resultado == ResultadoRodada.CompraQuatro 
                || rodadaAtual.Resultado == ResultadoRodada.Pular 
                || rodadaAtual.Resultado == ResultadoRodada.TrocaCor 
                || rodadaAtual.Resultado == ResultadoRodada.InverteJogo)
            {
                pilhaDescarte.Insert(0, rodadaAtual.carta);
            }
        }
    }
}
