namespace DominioTest.Unitarias
{
    using DominioTest.TestDataBuilders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BibliotecaDominio;

    [TestClass]
    public class LibroTest
    {
        private const int ANIO = 2012;
        private const string TITULO = "Cien años de soledad";
        private const string ISBN = "1234";
        private const string ISBN_PALINDROMO = "651156";
        private const string ISBN_ALTO = "98769";
        private const string ISBN_BAJO = "99";
        public LibroTest()
        {

        }

        [TestMethod]
        public void CrearLibroTest()
        {
            // Arrange
            LibroTestDataBuilder libroTestBuilder = new LibroTestDataBuilder().ConTitulo(TITULO).
                ConAnio(ANIO).ConIsbn(ISBN);
            
            // Act
            Libro libro = libroTestBuilder.Build();

            // Assert
            Assert.AreEqual(TITULO, libro.Titulo);
            Assert.AreEqual(ISBN, libro.Isbn);
            Assert.AreEqual(ANIO, libro.Anio);
        }

        [TestMethod]
        public void ValidarPalindromoTest()
        {                       
            // Act
            Libro libro = new Libro(ISBN, TITULO, ANIO);
            Libro libroISBNPalindromo = new Libro(ISBN_PALINDROMO, TITULO, ANIO);

            // Assert
            Assert.IsFalse(libro.ValidarPalindromo());
            Assert.IsTrue(libroISBNPalindromo.ValidarPalindromo());            
        }

        [TestMethod]
        public void SumariaStringTest()
        {
            // Act
            Libro libroISBNAlto = new Libro(ISBN_ALTO, TITULO, ANIO);
            Libro libroISBNBajo = new Libro(ISBN_BAJO, TITULO, ANIO);

            // Assert
            Assert.IsTrue(libroISBNAlto.SumarString() > 30);
            Assert.IsTrue(libroISBNBajo.SumarString() < 30);
        }

        [TestMethod]
        public void ValidarFechaMaximaSumatoriaISBNMayorA30Test()
        {
            // Act
            Libro libro = new Libro(ISBN, TITULO, ANIO);
            
            // Assert
            Assert.IsNotNull(libro.ValidarFechaMaxima(55));
            Assert.IsNull(libro.ValidarFechaMaxima(9));
        }
    }
}
