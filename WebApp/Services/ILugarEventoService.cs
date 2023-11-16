using AplicacionWeb.Models;

namespace AplicacionWeb.Services
{
    public interface ILugarEventoService
    {
        Task<IEnumerable<LugarEvento>> Obtener();
        Task<LugarEvento> GetByIdAsync(int id);
        Task AddAsync(LugarEvento lugarEvento);
        Task<LugarEvento> UpdateAsync(int id, LugarEvento newLugarEvento);
        Task DeleteAsync(int id);
    }
}
