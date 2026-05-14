using System.Linq.Expressions;

namespace YeeffBarber_AppointmentSystem.UI.Servicios
{
    public interface IService<T> where T : class
    {
        Task<T?> Get(int id);
        Task<List<T>> GetAll();
        Task<bool> Guardar(T entity);
        Task<bool> Eliminar(int id);
    }
}
