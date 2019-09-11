using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoUno
{
    public class Baralho
    {
        public List<Carta> Cartas { get; set; }
        //Construindo Baralho
        public Baralho()
        {
            Cartas = new List<Carta>();

            foreach (CorCarta cor in Enum.GetValues(typeof(CorCarta)))
            {
                if(cor != CorCarta.CORINGA)
                {
                    foreach(ValorCarta valor in Enum.GetValues(typeof(ValorCarta)))
                    {
                        switch (valor)
                        {
                            //Carta 0
                            case ValorCarta.Zero:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta 1
                            case ValorCarta.Um:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta 2
                            case ValorCarta.Dois:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta 3
                            case ValorCarta.Tres:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta 4
                            case ValorCarta.Quatro:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta 5
                            case ValorCarta.Cinco:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta 6
                            case ValorCarta.Seis:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta 7
                            case ValorCarta.Sete:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta 8
                            case ValorCarta.Oito:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta 9
                            case ValorCarta.Nove:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = (int)valor });
                                break;
                            //Carta Pular
                            case ValorCarta.Pular:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = 20 });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = 20 });
                                break;
                            //Carta Inverter
                            case ValorCarta.Inverter:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = 20 });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = 20 });
                                break;
                            //Compra 2
                            case ValorCarta.CompraDois:
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = 20 });
                                Cartas.Add(new Carta() { Cor = cor, Valor = valor, Pontuacao = 20 });
                                break;
                        }
                    }
                }
                //Cartas Coringas
                else
                {
                    //Troca cor
                    for (int i = 1; i <= 4; i++)
                    {
                        Cartas.Add(new Carta() { Cor = cor, Valor = ValorCarta.TrocaCor, Pontuacao = 50 });
                    }
                    //Comprar 4
                    for (int i = 1; i <= 4; i++)
                    {
                        Cartas.Add(new Carta() { Cor = cor, Valor = ValorCarta.CompraQuatro, Pontuacao = 50 });
                    }
                }
            }
        }
        //Contrutor lista de Cartas
        public Baralho(List<Carta> carta)
        {
            Cartas = carta;
        }

        //Embaralhar
        public void Embaralhar()
        {
            Random r = new Random();

            List<Carta> cartas = Cartas;

            for(int i = cartas.Count - 1; i > 0; --i)
            {
                int j = r.Next(i + 1);
                Carta carta = cartas[i];
                cartas[i] = cartas[j];
                cartas[j] = carta;
            }
        }

        //Remover cartas do baralho de compra
        public List<Carta> Compra(int count)
        {
            var cartasCompradas = Cartas.Take(count).ToList();
            Cartas.RemoveAll(card => cartasCompradas.Contains(card));
            return cartasCompradas;
        }
    }
}
