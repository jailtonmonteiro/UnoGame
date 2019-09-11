using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoUno
{
    public class Rodada
    {
        public Carta carta { get; set; }
        public CorCarta corDeclarada { get; set; } //Armazena a ultima cor da pilha de descarte
        public ResultadoRodada Resultado { get; set; } //Armazena ação da ultima rodada

    }
}
