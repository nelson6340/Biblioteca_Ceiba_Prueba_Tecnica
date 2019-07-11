namespace BibliotecaRepositorio.Repositorio
{
    using BibliotecaRepositorio.Entidades;

    public interface IRepositorioLibroEF
    {
        /// <summary>
        /// Permite obtener un libro entity por un isbn
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        LibroEntidad ObtenerLibroEntidadPorIsbn(string isbn);
    }
}
