using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMasterUTH.Clases
{
    /// <summary>
    /// Tablero de ajedrez
    /// </summary>
    class Tablero : List<Casilla>
    {
        
        public Tablero()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                    Add(new Casilla(this, row, column));
            }
            Jugador1 = ColorJugador.Blanco;
        }

        public ColorJugador Jugador1 { get; set; }

        public Casilla GetCasilla(int renglon, int columna)
        {
            return this.FirstOrDefault(s => s.Renglon == renglon && s.Columna == columna);
        }

        public Casilla GetCasilla(Casilla fromCasilla, Movimiento move)
        {
            PiezaAjedrez pieza = fromCasilla.Pieza;
            if (pieza == null)
            {
                return null;
            }

            return GetCasilla(fromCasilla.Renglon + (pieza.Color == Jugador1 ? move.Adelante : -move.Adelante), fromCasilla.Columna + move.Derecha);
        } 
        
        public void Inicializar()
        {
            LimpiarTablero();

            ColorJugador Jugador2 = (Jugador1 == ColorJugador.Blanco ? ColorJugador.Negro : ColorJugador.Blanco);
            //Reyes
            int column = (Jugador1 == ColorJugador.Blanco ? 4 : 3);
            GetCasilla(0, column).Pieza = new Rey(Jugador1);
            GetCasilla(7, column).Pieza = new Rey(Jugador2);
            // Reinas
            column = (column == 4 ? 3 : 4);
            GetCasilla(0, column).Pieza = new Reina(Jugador1);
            GetCasilla(7, column).Pieza = new Reina(Jugador2);
            // Torres
            GetCasilla(0, 0).Pieza = new Torre(Jugador1);
            GetCasilla(0, 7).Pieza = new Torre(Jugador1);
            GetCasilla(7, 0).Pieza = new Torre(Jugador2);
            GetCasilla(7, 7).Pieza = new Torre(Jugador2);
            // Caballos
            GetCasilla(0, 1).Pieza = new Caballo(Jugador1);
            GetCasilla(0, 6).Pieza = new Caballo(Jugador1);
            GetCasilla(7, 1).Pieza = new Caballo(Jugador2);
            GetCasilla(7, 6).Pieza = new Caballo(Jugador2);
            // Alfil
            GetCasilla(0, 2).Pieza = new Alfil(Jugador1);
            GetCasilla(0, 5).Pieza = new Alfil(Jugador1);
            GetCasilla(7, 2).Pieza = new Alfil(Jugador2);
            GetCasilla(7, 5).Pieza = new Alfil(Jugador2);
            // Peones
            for (int i = 0; i < 8; i++)
            {
                GetCasilla(1, i).Pieza = new Peon(Jugador1);
                GetCasilla(6, i).Pieza = new Peon(Jugador2);
            }            

        }

        public void Inicializar(ColorJugador colorJugador1)
        {
            Jugador1 = colorJugador1;
            Inicializar();
        }

        public bool Avance(Casilla deCasilla, Casilla haciaCasilla)
        {
            if (deCasilla != null && haciaCasilla != null && deCasilla.Pieza != null)
            {
                haciaCasilla.Pieza = deCasilla.Pieza;
                deCasilla.Pieza = null;
                return true;
            }
            else
            {
                return false;               
            }
        }

        private void LimpiarTablero()
        {
            foreach (Casilla casilla in this)
            {
                casilla.Pieza = null;
            }
        }
    }

    /// <summary>
    /// Casilla del tablero
    /// </summary>
    class Casilla
    {
        public Casilla(Tablero cuadricula, int renglon, int columna)
        {
            Cuadricula = cuadricula;
            Renglon = renglon;
            Columna = columna;
        }

        public Tablero Cuadricula { get; set; }
        public int Renglon { get; set; }
        public int Columna { get; }
        private PiezaAjedrez _pieza;
        public PiezaAjedrez Pieza
        {
            get { return _pieza; }
            set
            {
                bool cambio = _pieza != value;
                if (_pieza != null)
                {
                    _pieza.CasillaTablero = this;
                    if (cambio)
                    {
                        EnPiezaCambiada(new EventArgs());
                    }
                }
            }
        }

        public event EventHandler PiezaCambiada;

        protected virtual void EnPiezaCambiada(EventArgs e)
        {
            EventHandler handler = PiezaCambiada;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}
