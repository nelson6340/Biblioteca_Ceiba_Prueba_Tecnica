namespace BibliotecaDominio
{
    using BibliotecaDominio.IRepositorio;
    using System;

    public class Bibliotecario
    {
        public const string EL_LIBRO_NO_SE_ENCUENTRA_EN_EL_RESPOSITORIO = "el libro no esta en la biblioteca";
        public const string EL_LIBRO_ES_PALINDROMO = "los libros con isbn palindromos solo se pueden usar en la biblioteca";
        public const string EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE = "El libro no se encuentra disponible";

        private  IRepositorioLibro libroRepositorio;
        private  IRepositorioPrestamo prestamoRepositorio;

        public Bibliotecario(IRepositorioLibro libroRepositorio, IRepositorioPrestamo prestamoRepositorio)
        {
            this.libroRepositorio = libroRepositorio;
            this.prestamoRepositorio = prestamoRepositorio;
        }

        public void Prestar(string isbn, string nombreUsuario)
        {
            Libro libroPrestado = prestamoRepositorio.ObtenerLibroPrestadoPorIsbn(isbn);

            if (libroPrestado != null)
            {
                throw new Exception(EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE);
            }

            Libro libroEncontrado = libroRepositorio.ObtenerPorIsbn(isbn);

            if (libroEncontrado == null)
            {
                throw new Exception(EL_LIBRO_NO_SE_ENCUENTRA_EN_EL_RESPOSITORIO);
            }
            else
            {
                bool palindormo = libroEncontrado.ValidarPalindromo();

                if (palindormo)
                {
                    throw new Exception(EL_LIBRO_ES_PALINDROMO);
                }
                else
                {
                    int suma = libroEncontrado.SumarString();
                    DateTime? fechaMaxima = libroEncontrado.ValidarFechaMaxima(suma);
                                        
                    Prestamo prestamo = new Prestamo(DateTime.Now, libroEncontrado, fechaMaxima, nombreUsuario);
                    prestamoRepositorio.Agregar(prestamo);
                }
            }
        }


        public bool EsPrestado(string isbn)
        {
            return prestamoRepositorio.ObtenerLibroPrestadoPorIsbn(isbn) == null ? false : true;

        }
    }
}
