using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoUno
{
    public class Carta
    {
        public CorCarta Cor { get; set; }
        public ValorCarta Valor { get; set; }
        public int Pontuacao { get; set; }

        public string valorExibibo
        {
            get
            {
                if (Valor == ValorCarta.TrocaCor)
                {
                    return Valor.ToString();
                }
                return Valor.ToString() + "-" + Cor.ToString();
            }
        }
    }
}
