using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoUno
{
    public enum ResultadoRodada
    {
        JogoIniciado,
        CartaComum, //Cartas de numeros e cores
        Pular, //Carta de bloqueio
        CompraDois, //Carta comprar 2
        CompraIndireta, // Compra para descarte
        Compra, // Compra inicial
        Comprarjogar, // Compra forçada
        TrocaCor, //Carta troca de cor
        CompraQuatro, //Carta compra 4
        InverteJogo //Inverte o sentido do jogo
    }
}
