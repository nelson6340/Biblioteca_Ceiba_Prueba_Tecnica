namespace DominioTest.Integracion
{
    using System;
    using BibliotecaDominio;
    using BibliotecaRepositorio.Contexto;
    using BibliotecaRepositorio.Repositorio;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DominioTest.TestDataBuilders;
    using Microsoft.EntityFrameworkCore;

    [TestClass]
    public class BibliotecarioTest
    {
        public const String CRONICA_UNA_MUERTE_ANUNCIADA = "Cronica de una muerte anunciada";
        private const int ANIO = 2012;
        private const string ISBN = "1234";
        private const string ISBN_PALINDROMO = "651156";
        private const string NOMBRE_USUARIO = "Juan";

        private  BibliotecaContexto contexto;
        private  RepositorioLibroEF repositorioLibro;
        private RepositorioPrestamoEF repositorioPrestamo;


        [TestInitialize]
        public void setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContexto>();
            contexto = new BibliotecaContexto(optionsBuilder.Options);
            repositorioLibro  = new RepositorioLibroEF(contexto);
            repositorioPrestamo = new RepositorioPrestamoEF(contexto, repositorioLibro);
        }

        [TestMethod]
        public void PrestarLibroNoExistenteTest()
        {
            // Arrange
            var libro = new Libro(ISBN, CRONICA_UNA_MUERTE_ANUNCIADA, ANIO);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            try
            {
                // Act
                bibliotecario.Prestar(libro.Isbn, NOMBRE_USUARIO);
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual("el libro no esta en la biblioteca", err.Message);
            }
        }

        [TestMethod]
        public void PrestarLibroIsbnPalindromoTest()
        {
            // Arrange
            Libro libro = new Libro(ISBN_PALINDROMO, CRONICA_UNA_MUERTE_ANUNCIADA, ANIO);
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            try
            {
                // Act
                bibliotecario.Prestar(libro.Isbn, NOMBRE_USUARIO);
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual("los libros con isbn palindromos solo se pueden usar en la biblioteca", err.Message);
            }
        }

        [TestMethod]
        public void PrestarLibroTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            // Act
            bibliotecario.Prestar(libro.Isbn, NOMBRE_USUARIO);

            // Assert
            Assert.AreEqual(bibliotecario.EsPrestado(libro.Isbn), true);
            Assert.IsNotNull(repositorioPrestamo.ObtenerLibroPrestadoPorIsbn(libro.Isbn));

        }

        [TestMethod]
        public void PrestarLibroNoDisponibleTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            // Act
            bibliotecario.Prestar(libro.Isbn, NOMBRE_USUARIO);
            try
            {
                bibliotecario.Prestar(libro.Isbn, NOMBRE_USUARIO);
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual("El libro no se encuentra disponible", err.Message);
            }
        
        }

    }
}
