using AplicacionWeb.Data;
using AplicacionWeb.Models;
using Microsoft.EntityFrameworkCore;
namespace AplicacionWeb.Services
{
    public class LugarEventoService : ILugarEventoService
    {
        private readonly ExamenIiContext _dbcontext;

        public LugarEventoService(ExamenIiContext context)
        {
            _dbcontext = context;
        }

        public async Task AddAsync(LugarEvento lugarEvento)
        {
            await _dbcontext.LugarEventos.AddAsync(lugarEvento);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lugarEvento = await _dbcontext.LugarEventos.FindAsync(id);

            _dbcontext.LugarEventos.Remove(lugarEvento);
            await _dbcontext.SaveChangesAsync();

        }

        public async Task<IEnumerable<LugarEvento>> Obtener()
        {
            var result = await _dbcontext.LugarEventos.ToListAsync();
            return result;
        }

        public async Task<LugarEvento> GetByIdAsync(int id)
        {
            var result = await _dbcontext.LugarEventos.FindAsync(id);
            return result;
        }

        public async Task<LugarEvento> UpdateAsync(int id, LugarEvento newLugarEvento)
        {
            var lugarEvento = await _dbcontext.LugarEventos.FindAsync(id);
            lugarEvento.Nombre = newLugarEvento.Nombre;
            lugarEvento.Ciudad = newLugarEvento.Ciudad;
            lugarEvento.Pais = newLugarEvento.Pais;
            lugarEvento.Capacidad = newLugarEvento.Capacidad;
            lugarEvento.Deshabilitado = newLugarEvento.Deshabilitado;
            //_dbcontext.Update(newLugarEvento);
            await _dbcontext.SaveChangesAsync();
            return newLugarEvento;
        }
    }
}
