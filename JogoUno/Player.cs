using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoUno
{
    public class Player
    {
        public List<Carta> deck { get; set; }
        public int Posicao { get; set; }

        public Player()
        {
            deck = new List<Carta>();
        }
        private void exibirRodada(Rodada rodada)
        {
            Console.ReadKey();
            if (rodada.Resultado == ResultadoRodada.Compra)
            {
                Console.WriteLine("Jogador: " + Posicao.ToString() + " Comprou");
            }
            if(rodada.Resultado == ResultadoRodada.Comprarjogar)
            {
                Console.WriteLine("Jogador: " + Posicao.ToString() + " Comprou porque não tinha cartas");
            }

            if(rodada.Resultado == ResultadoRodada.CartaComum
               || rodada.Resultado == ResultadoRodada.Pular
               || rodada.Resultado == ResultadoRodada.CompraDois
               || rodada.Resultado == ResultadoRodada.TrocaCor
               || rodada.Resultado == ResultadoRodada.CompraQuatro
               || rodada.Resultado == ResultadoRodada.Comprarjogar
               || rodada.Resultado == ResultadoRodada.CompraQuatro)
            {
                Console.WriteLine("Jogador: " + Posicao.ToString() + " descartou a carta " + rodada.carta.valorExibibo);
                if(rodada.carta.Cor == CorCarta.CORINGA)
                {
                    Console.WriteLine("Jogador " + Posicao.ToString() + " declarou " + rodada.corDeclarada.ToString() + " como nova cor");
                }
                if (rodada.Resultado == ResultadoRodada.InverteJogo)
                {
                    Console.WriteLine(" Sentido do jogo invertido");
                }
            }
            if (deck.Count == 1)
            {
                Console.WriteLine("Jogador " + Posicao.ToString() + " Gritou Uno!");
            }
            
        }

        private Rodada Ataque(Carta descarteAtual, Baralho pilhaDescarte)
        {
            Rodada rodada = new Rodada();
            rodada.Resultado = ResultadoRodada.CompraIndireta;
            rodada.carta = descarteAtual;
            rodada.corDeclarada = descarteAtual.Cor;
            if (descarteAtual.Valor == ValorCarta.Pular)
            {
                Console.WriteLine("Jogador: " + Posicao.ToString() + " Bloqueado");
                return rodada;
            }
            else if (descarteAtual.Valor == ValorCarta.CompraDois)
            {
                Console.WriteLine("Jogador " + Posicao.ToString() + " Comprou 2 cartas");
                deck.AddRange(pilhaDescarte.Compra(2));
            }
            else if (descarteAtual.Valor == ValorCarta.CompraQuatro)
            {
                Console.WriteLine("Jogador " + Posicao.ToString() + " Comprou 4 cartas");
                deck.AddRange(pilhaDescarte.Compra(4));
            }

            return rodada;
        }
        private void sortearDeck()
        {
            this.deck = this.deck.OrderBy(x => x.Cor).ThenBy(x => x.Valor).ToList();
        }
        public void exibirDeck()
        {
            sortearDeck();
            Console.WriteLine("Jogador: " + Posicao + " deck: ");
            foreach (var card in deck)
            {
                Console.Write(Enum.GetName(typeof(CorCarta), card.Cor) + "-" + Enum.GetName(typeof(ValorCarta), card.Valor) + " ");
            }
            Console.WriteLine("");
        }

        private CorCarta selecionarCor()
        {
            if (!deck.Any())
            {
                return CorCarta.CORINGA;
            }
            var cores = deck.GroupBy(x => x.Cor).OrderByDescending(x => x.Count());
            return cores.First().First().Cor;
        }
        

        private Rodada jogandoCartas(Carta descarteAtual)
        {
            var rodada = new Rodada();
            rodada.Resultado = ResultadoRodada.CartaComum;
            var partida = deck.Where(x => x.Cor == descarteAtual.Cor || x.Valor == descarteAtual.Valor || x.Cor == CorCarta.CORINGA).ToList();

            if (partida.All(x => x.Valor == ValorCarta.CompraQuatro))
            {
                rodada.carta = partida.First();
                rodada.corDeclarada = selecionarCor();
                rodada.Resultado = ResultadoRodada.CompraQuatro;
                deck.Remove(partida.First());

                return rodada;
            }

            if (partida.Any(x => x.Valor == ValorCarta.CompraDois))
            {
                rodada.carta = partida.First(x => x.Valor == ValorCarta.CompraDois);
                rodada.Resultado = ResultadoRodada.CompraDois;
                rodada.corDeclarada = rodada.carta.Cor;
                deck.Remove(rodada.carta);

                return rodada;
            }

            if (partida.Any(x => x.Valor == ValorCarta.Pular))
            {
                rodada.carta = partida.First(x => x.Valor == ValorCarta.Pular);
                rodada.Resultado = ResultadoRodada.Pular;
                rodada.corDeclarada = rodada.carta.Cor;
                deck.Remove(rodada.carta);

                return rodada;
            }

            if (partida.Any(x => x.Valor == ValorCarta.Inverter))
            {
                rodada.carta = partida.First(x => x.Valor == ValorCarta.Inverter);
                rodada.Resultado = ResultadoRodada.InverteJogo;
                rodada.corDeclarada = rodada.carta.Cor;
                deck.Remove(rodada.carta);

                return rodada;
            }

            var corPartida = partida.Where(x => x.Cor == descarteAtual.Cor);
            var valorPartida = partida.Where(x => x.Valor == descarteAtual.Valor);
            if (corPartida.Any() && valorPartida.Any())
            {
                var correspondingColor = deck.Where(x => x.Cor == corPartida.First().Cor);
                var correspondingValue = deck.Where(x => x.Valor == valorPartida.First().Valor);
                if (correspondingColor.Count() >= correspondingValue.Count())
                {
                    rodada.carta = corPartida.First();
                    rodada.corDeclarada = rodada.carta.Cor;
                    deck.Remove(corPartida.First());

                    return rodada;
                }
                else
                {
                    rodada.carta = valorPartida.First();
                    rodada.corDeclarada = rodada.carta.Cor;
                    deck.Remove(valorPartida.First());

                    return rodada;
                }
                //Seleciona a melhor carta
            }
            else if (corPartida.Any())
            {
                rodada.carta = corPartida.First();
                rodada.corDeclarada = rodada.carta.Cor;
                deck.Remove(corPartida.First());

                return rodada;
            }
            else if (valorPartida.Any())
            {
                rodada.carta = valorPartida.First();
                rodada.corDeclarada = rodada.carta.Cor;
                deck.Remove(valorPartida.First());

                return rodada;
            }

            //Deixar troca cor por ultimo
            if (partida.Any(x => x.Valor == ValorCarta.TrocaCor))
            {
                rodada.carta = partida.First(x => x.Valor == ValorCarta.TrocaCor);
                rodada.corDeclarada = selecionarCor();
                rodada.Resultado = ResultadoRodada.TrocaCor;
                deck.Remove(rodada.carta);

                return rodada;
            }

            //Passa o resultado para a rodada
            rodada.Resultado = ResultadoRodada.Comprarjogar;
            return rodada;
        }

        private Rodada jogandoCartas(CorCarta cor)
        {
            var rodada = new Rodada();
            rodada.Resultado = ResultadoRodada.CartaComum;
            var partida = deck.Where(x => x.Cor == cor || x.Cor == CorCarta.CORINGA).ToList();

            
            if (partida.All(x => x.Valor == ValorCarta.CompraQuatro))
            {
                rodada.carta = partida.First();
                rodada.corDeclarada = selecionarCor();
                rodada.Resultado = ResultadoRodada.CompraQuatro;
                deck.Remove(partida.First());

                return rodada;
            }

            
            if (partida.Any(x => x.Valor == ValorCarta.CompraDois))
            {
                rodada.carta = partida.First(x => x.Valor == ValorCarta.CompraDois);
                rodada.Resultado = ResultadoRodada.CompraDois;
                rodada.corDeclarada = rodada.carta.Cor;
                deck.Remove(rodada.carta);

                return rodada;
            }

            if (partida.Any(x => x.Valor == ValorCarta.Pular))
            {
                rodada.carta = partida.First(x => x.Valor == ValorCarta.Pular);
                rodada.Resultado = ResultadoRodada.Pular;
                rodada.corDeclarada = rodada.carta.Cor;
                deck.Remove(rodada.carta);

                return rodada;
            }

            if (partida.Any(x => x.Valor == ValorCarta.Pular))
            {
                rodada.carta = partida.First(x => x.Valor == ValorCarta.Inverter);
                rodada.Resultado = ResultadoRodada.Pular;
                rodada.corDeclarada = rodada.carta.Cor;
                deck.Remove(rodada.carta);

                return rodada;
            }

            var corPartida = partida.Where(x => x.Cor == cor);
            if (corPartida.Any())
            {
                rodada.carta = corPartida.First();
                rodada.corDeclarada = rodada.carta.Cor;
                deck.Remove(corPartida.First());

                return rodada;
            }

            if (partida.Any(x => x.Valor == ValorCarta.TrocaCor))
            {
                rodada.carta = partida.First(x => x.Valor == ValorCarta.TrocaCor);
                rodada.corDeclarada = selecionarCor();
                rodada.Resultado = ResultadoRodada.TrocaCor;
                deck.Remove(rodada.carta);

                return rodada;
            }

            //Passa o resultado para a rodada
            rodada.Resultado = ResultadoRodada.Comprarjogar;
            return rodada;
        }

        private Rodada comprarCarta(Rodada rodadaAnterior, Baralho pilhaDescarte)
        {
            Rodada rodada = new Rodada();
            var compraCarta = pilhaDescarte.Compra(1);
            deck.AddRange(compraCarta);

            if (valorCorrespondente(rodadaAnterior.carta))
            {
                rodada = jogandoCartas(rodadaAnterior.carta);
                rodada.Resultado = ResultadoRodada.Compra;
            }
            else
            {
                rodada.Resultado = ResultadoRodada.Comprarjogar;
                rodada.carta = rodadaAnterior.carta;
            }

            return rodada;
        }

        private bool valorCorrespondente(Carta carta)
        {
            return deck.Any(x => x.Cor == carta.Cor || x.Valor == carta.Valor || x.Cor == CorCarta.CORINGA);
        }

        private bool valorCorrespondente(CorCarta color)
        {
            return deck.Any(x => x.Cor == color || x.Cor == CorCarta.CORINGA);
        }


        public Rodada jogadaAtual(Rodada rodadaAnterior, Baralho pilhaCompra)
        {
            Rodada rodada = new Rodada();
            if (rodadaAnterior.Resultado == ResultadoRodada.Pular
                || rodadaAnterior.Resultado == ResultadoRodada.CompraDois
                || rodadaAnterior.Resultado == ResultadoRodada.CompraQuatro)
            {
                return Ataque(rodadaAnterior.carta, pilhaCompra);
            }
            else if ((rodadaAnterior.Resultado == ResultadoRodada.TrocaCor
                        || rodadaAnterior.Resultado == ResultadoRodada.CompraIndireta
                        || rodadaAnterior.Resultado == ResultadoRodada.Comprarjogar)
                        && valorCorrespondente(rodadaAnterior.corDeclarada))
            {
                rodada = jogandoCartas(rodadaAnterior.corDeclarada);
            }
            else if (valorCorrespondente(rodadaAnterior.carta))
            {
                rodada = jogandoCartas(rodadaAnterior.carta);
            }
            else //Compra carta e verifica se poder ser jogada
            {
                rodada = comprarCarta(rodadaAnterior, pilhaCompra);
            }

            exibirRodada(rodada);
            return rodada;
        }
    }
}
