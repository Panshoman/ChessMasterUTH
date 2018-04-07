using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMasterUTH.Clases
{
    
        /// <summary>
        /// Color de los jugadores
        /// </summary>
        enum ColorJugador
        {
            Blanco, Negro
        }

        /// <summary>
        /// Posibles estados de juego
        /// </summary>
        enum EstadoMovimiento
        {           
            EsperandoBlanco,
            EsperandoNegro,
            BlancoMoviendo,
            NegroMoviendo,
            GanaBlanco,
            GanaNegro,
        }

        /// <summary>
        /// Define los movimientos posibles
        /// </summary>
        struct Movimiento
        {            
            public int Adelante;
            public int Derecha;
        }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Juego"/>
        /// </summary>
        class Juego
        {
            public Juego()
            {
                Cuadricula = new Tablero();
            }

            /// <summary>
            /// Tabledo del juego
            /// </summary>
            public Tablero Cuadricula { get; private set; }

            /// <summary>
            /// Estado del juego
            /// </summary>
            private EstadoMovimiento _estado;
            public EstadoMovimiento Estado
            {
                get { return _estado; }
                private set
                {
                    if (_estado != value)
                    {
                        _estado = value;
                        CuandoCambioEstado(new EventArgs());

                    }
                }
            }

            public Casilla SeleccionarCasilla { get; private set; }




        }


}

