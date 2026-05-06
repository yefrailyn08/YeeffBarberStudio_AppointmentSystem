using System.Linq.Expressions;

namespace YeeffBarber_AppointmentSystem.UI.Servicios
{
    public interface IService<T> where T : class
    {
        Task<bool> Guardar(T entity);
        Task<List<T>> GetAll();
    }
}
