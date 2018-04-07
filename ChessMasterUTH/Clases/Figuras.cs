using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChessMasterUTH.Clases
{
    
    abstract class PiezaAjedrez
    {    
        public PiezaAjedrez(ColorJugador color)
        {
            Color = color;
        }

        public ColorJugador Color { get; set; }

        public Casilla CasillaTablero { get; internal set; }

        public abstract IEnumerable<Casilla> ObtenerCasillaDestino();

        protected virtual bool PuedeMoverse(Casilla casillaDestino)
        {
            if (casillaDestino == null)
            {
                return false;
            }

            return (casillaDestino.Pieza == null || casillaDestino.Pieza.Color != Color);
        }

        protected IEnumerable<Casilla> DireccionCasillaDestino(int incrementoAdelante, int incrementoDerecha)
        {
            Tablero cuadricula = CasillaTablero.Cuadricula;

            List<Casilla> casillas = new List<Casilla>();
            int adelante = incrementoAdelante;
            int derecha = incrementoDerecha;
            bool piezaOBorde = false;
            while (!piezaOBorde)
            {
                Casilla destino = cuadricula.GetCasilla(CasillaTablero, new Movimiento { Adelante = adelante, Derecha = derecha });
                if (PuedeMoverse(destino))
                {
                    casillas.Add(destino);
                }  
                piezaOBorde = (destino == null || destino.Pieza == null);
                adelante += incrementoAdelante;
                derecha += incrementoDerecha;
            }
            return casillas;
        }

        protected IEnumerable<Casilla> CasillasDestinoParaMoverse(IEnumerable<Movimiento> movimientos)
        {
            Tablero tablero = CasillaTablero.Cuadricula;
            List<Casilla> casillas = new List<Casilla>();
            foreach (Movimiento movimiento in movimientos)
            {
                Casilla destino = tablero.GetCasilla(CasillaTablero, movimiento);
                if (PuedeMoverse(destino))
                {
                    casillas.Add(destino);
                }                
            }
            return casillas;
        }    
    }

    class Rey : PiezaAjedrez
    {
        public Rey(ColorJugador color) : base(color) { }

        private Movimiento[] movimientos =
        {
                new Movimiento() {Adelante=1, Derecha=-1 },
                new Movimiento() {Adelante=1, Derecha=0 },
                new Movimiento() {Adelante=1, Derecha=1 },
                new Movimiento() {Adelante=0, Derecha=-1 },
                new Movimiento() {Adelante=0, Derecha=1 },
                new Movimiento() {Adelante=-1, Derecha=-1 },
                new Movimiento() {Adelante=-1, Derecha=0 },
                new Movimiento() {Adelante=-1, Derecha=1 }
            };

        public override IEnumerable<Casilla> ObtenerCasillaDestino()
        {
            if (CasillaTablero == null)
            {
                return null;
            }

            return CasillasDestinoParaMoverse(movimientos);
        }
    }

    class Reina : PiezaAjedrez
    {
        public Reina(ColorJugador color) : base(color) { }

        public override IEnumerable<Casilla> ObtenerCasillaDestino()
        {
            if (CasillaTablero == null)
            {
                return null;
            }
            List<Casilla> posibleCasilla = new List<Casilla>();
            // Hacia adelante a la izquierda
            posibleCasilla.AddRange(DireccionCasillaDestino(1, -1));
            // Hacia adelante
            posibleCasilla.AddRange(DireccionCasillaDestino(1, 0));
            // Hacia adelante a la derecha
            posibleCasilla.AddRange(DireccionCasillaDestino(1, 1));
            // Hacia la izquierda
            posibleCasilla.AddRange(DireccionCasillaDestino(0, -1));
            // Hacia la derecha
            posibleCasilla.AddRange(DireccionCasillaDestino(0, 1));
            // Hacia atrás a la izquierda
            posibleCasilla.AddRange(DireccionCasillaDestino(-1, -1));
            // Hacia atrás
            posibleCasilla.AddRange(DireccionCasillaDestino(-1, 0));
            // Hacia atrás a la derecha
            posibleCasilla.AddRange(DireccionCasillaDestino(-1, 1));

            return posibleCasilla;
        }
    }

    class Torre : PiezaAjedrez
    {
        public Torre(ColorJugador color) : base(color) { }

        public override IEnumerable<Casilla> ObtenerCasillaDestino()
        {
            if (CasillaTablero == null)
            {
                return null;
            }
            List<Casilla> posibleCasilla = new List<Casilla>();
            // A la izquierda
            posibleCasilla.AddRange(DireccionCasillaDestino(0, -1));
            // A la derecha
            posibleCasilla.AddRange(DireccionCasillaDestino(0, 1));
            // Hacia adelante
            posibleCasilla.AddRange(DireccionCasillaDestino(1, 0));
            // Hacia atrás
            posibleCasilla.AddRange(DireccionCasillaDestino(-1, 0));

            return posibleCasilla;
        }
    }

    class Caballo : PiezaAjedrez
    {
        public Caballo(ColorJugador color) : base(color) { }

        private Movimiento[] moves =
        {
            new Movimiento {Adelante=1, Derecha=-2 },
            new Movimiento {Adelante=2, Derecha=-1 },
            new Movimiento {Adelante=2, Derecha=1 },
            new Movimiento {Adelante=1, Derecha=2 },
            new Movimiento {Adelante=-1, Derecha=-2 },
            new Movimiento {Adelante=-2, Derecha=-1 },
            new Movimiento {Adelante=-2, Derecha=1 },
            new Movimiento {Adelante=-1, Derecha=2 }
        };

        /// <summary>
        /// Devuelve las celdas a las que puede moverse la pieza
        /// </summary>
        /// <returns>Lista de celdas</returns>
        public override IEnumerable<Casilla> ObtenerCasillaDestino()
        {
            if (CasillaTablero == null) return null;

            return CasillasDestinoParaMoverse(moves);
        }
    }

    /// <summary>
    /// Alfil
    /// </summary>
    class Alfil : PiezaAjedrez
    {

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Bishop"/>
        /// </summary>
        /// <param name="color">Color de la pieza</param>
        public Alfil(ColorJugador color) : base(color) { }

        /// <summary>
        /// Devuelve las celdas a las que puede moverse la pieza
        /// </summary>
        /// <returns>Lista de celdas</returns>
        public override IEnumerable<Casilla> ObtenerCasillaDestino()
        {
            if (CasillaTablero == null) return null;

            List<Casilla > posiblesCasillas = new List<Casilla>();
            // Hacia adelante a la izquierda
            posiblesCasillas.AddRange(DireccionCasillaDestino(1, -1));
            // Hacia adelante a la derecha
            posiblesCasillas.AddRange(DireccionCasillaDestino(1, 1));
            // Hacia atrás a la izquierda
            posiblesCasillas.AddRange(DireccionCasillaDestino(-1, -1));
            // Hacia atrás a la derecha
            posiblesCasillas.AddRange(DireccionCasillaDestino(-1, 1));

            return posiblesCasillas;
        }
    }

    /// <summary>
    /// Peón
    /// </summary>
    class Peon : PiezaAjedrez
    {

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Pawn"/>
        /// </summary>
        /// <param name="color">Color de la pieza</param>
        public Peon(ColorJugador color) : base(color) { }

        /// <summary>
        /// Devuelve las celdas a las que puede moverse la pieza
        /// </summary>
        /// <returns>Lista de celdas</returns>
        public override IEnumerable<Casilla> ObtenerCasillaDestino()
        {
            if (CasillaTablero == null) return null;

            Tablero tablero = CasillaTablero.Cuadricula;
            bool isInStartPosition = (tablero.GetCasilla(CasillaTablero, new Movimiento { Adelante = -2, Derecha = 0 }) == null);

            List<Casilla> posibleSquares = new List<Casilla>();
            Casilla destinationSquare = tablero.GetCasilla(CasillaTablero, new Movimiento { Adelante = 1, Derecha = 0 });
            if (destinationSquare != null && destinationSquare.Pieza == null)
            {
                posibleSquares.Add(destinationSquare);
                if (isInStartPosition)
                {
                    destinationSquare = tablero.GetCasilla(CasillaTablero, new Movimiento { Adelante = 2, Derecha = 0 });
                    if (destinationSquare != null && destinationSquare.Pieza == null)
                        posibleSquares.Add(destinationSquare);
                }
            }
            destinationSquare = tablero.GetCasilla(CasillaTablero, new Movimiento { Adelante = 1, Derecha = -1 });
            if (destinationSquare != null && destinationSquare.Pieza != null && destinationSquare.Pieza.Color != Color)
                posibleSquares.Add(destinationSquare);
            destinationSquare = tablero.GetCasilla(CasillaTablero, new Movimiento { Adelante = 1, Derecha = 1 });
            if (destinationSquare != null && destinationSquare.Pieza != null && destinationSquare.Pieza.Color != Color)
                posibleSquares.Add(destinationSquare);

            return posibleSquares;
        }
    }
}
