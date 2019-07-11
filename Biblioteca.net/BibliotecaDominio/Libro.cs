namespace BibliotecaDominio
{
    using System;
    using System.Linq;

    public class Libro
    {
        public string Isbn { get; }
        public string Titulo { get; }        
        public int Anio { get; }

        public Libro(string isbn, string titulo, int anio)
        {
            this.Isbn = isbn;
            this.Titulo = titulo;
            this.Anio = anio;
        }

        /// <summary>
        /// Metodo que valida si un string es palindromo
        /// </summary>
        /// <returns>retorna el valor boleano true o false decuerdo a la validacion</returns>
        public bool ValidarPalindromo()
        {
            return Isbn.SequenceEqual(Isbn.Reverse());
        }


        /// <summary>
        /// Metodo encargado de realizar la sumatoria del string del isbn
        /// </summary>
        /// <returns>retorna la sumatoria del string</returns>
        public int SumarString()
        {
            int suma = 0;
            foreach (char c in Isbn)
            {
                if (char.IsDigit(c))
                {
                    suma += (int)char.GetNumericValue(c);
                }
            }
            return suma;
        }

        /// <summary>
        /// Metodo usado para validar si la fecha debe retornar un null o entregar la fecha maxima
        /// </summary>
        /// <param name="suma"></param>
        /// <returns>puede retornar un null o un dateTime con la fecha maxima</returns>
        public DateTime? ValidarFechaMaxima(int suma)
        {
            DateTime fechaEntregaMaxima = new DateTime();
            if (suma > 30)
            {
                fechaEntregaMaxima = CalcularFechaEntregaMaxima();
            } else
            {                
                return null;
            }

            return fechaEntregaMaxima;
        }

        /// <summary>
        /// Metodo encargado de calcular la fecha maxima de entrega teniendo en cuenta los domingos
        /// </summary>
        /// <returns>retorna la fecha maxima en la que se retorna el libro</returns>
        public DateTime CalcularFechaEntregaMaxima()
        {            
            DateTime fecha = DateTime.Now;
            for (int i = 1; i < 15; i++)
            {
                if (fecha.DayOfWeek == DayOfWeek.Sunday)
                    i--;

                fecha = fecha.AddDays(1);
            }
            return fecha;
        }

    }
}
