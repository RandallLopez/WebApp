using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using AplicacionWeb.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;
using AplicacionWeb.Data;
using AplicacionWeb.Services;

namespace WebApp.Tests
{
    public class LugarEventoTests
    {
        // Prueba unitaria para el metodo obtener() del servicio
        // Datos de entrada: Ninguno, ya que el metodo a probar obtiene la lista de registros de la base de datos
        // Salida esperada: La lista de registros de la entidad que se encuentran en la base de datos
        [Fact]
        public async Task Obtener_ShouldReturnListOfLugarEventos()
        {
            var lugarEventos = new List<LugarEvento>
            {
                // Se Crea una lista de objetos LugarEvento para simular los datos en la base de datos
                new LugarEvento { Nombre = "Lugar 1", Ciudad = "Ciudad 1", 
                    Pais = "País 1", Capacidad = 100, Deshabilitado = false },
                new LugarEvento { Nombre = "Lugar 2", Ciudad = "Ciudad 2", 
                    Pais = "País 2", Capacidad = 200, Deshabilitado = true },
                new LugarEvento { Nombre = "Lugar 3", Ciudad = "Ciudad 3", 
                    Pais = "País 3", Capacidad = 300, Deshabilitado = false }
            };

            // Se configura el contexto de la base de datos (usando Moq y una instancia en memoria del DbContext)
            var dbContextOptions = new DbContextOptionsBuilder<ExamenIiContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase4")
                .Options;
            var dbContext = new ExamenIiContext(dbContextOptions);
            dbContext.LugarEventos.AddRange(lugarEventos);
            await dbContext.SaveChangesAsync();

            // Se crea la instancia del servicio y se prueba el metodo obtener()
            var sut = new LugarEventoService(dbContext);
            var result = await sut.Obtener();

            // Assert
            Assert.Equal(lugarEventos.Count, result.Count());
            Assert.Equal(lugarEventos, result);
        }

        // Prueba unitaria para el metodo agregar() del servicio
        // Datos de entrada: Se debe crear un Objeto de LugarEvento ya que el metodo que se prueba
        //      necesita uno para probar si se agrega correctamente
        // Salida esperada: Se utiliza el metodo findAsync para intentar encontrar el registro que se agrego
        //      en la base de datos, por lo tanto la salida esperada es el mismo objeto que se agrega
        [Fact]
        public async Task AddAsync_ShouldAddLugarEvento()
        {
            var lugarEvento = new LugarEvento
            {
                Nombre = "Lugar 1",
                Ciudad = "Ciudad 1",
                Pais = "País 1",
                Capacidad = 100,
                Deshabilitado = false
            }; // Crea una instancia de LugarEvento

            // Configurar el contexto de base de datos (usando Moq y una instancia en memoria de la base de datos)
            var dbContextOptions = new DbContextOptionsBuilder<ExamenIiContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase3")
                .Options;
            var dbContext = new ExamenIiContext(dbContextOptions);

            // Se crea la instancia del servicio y se prueba el metodo agregar()
            var sut = new LugarEventoService(dbContext);
            await sut.AddAsync(lugarEvento);

            // Assert
            var addedLugarEvento = await dbContext.LugarEventos.FindAsync(lugarEvento.Id);
            Assert.NotNull(addedLugarEvento);
        }

        // Prueba unitaria para el metodo eliminar() del servicio
        // Datos de entrada: Se elige como entrada un id para identificar el registro que se va a eliminar en la prueba
        // Salida esperada: Se utiliza el findAsync para intentar encontrar el registro que se borra de la 
        //      base de datos y se espera que retorne Null para comprabar que se elimina correctamente
        [Fact]
        public async Task DeleteAsync_ShouldRemoveLugarEvento()
        {
            var lugarEventoId = 150; // ID del LugarEvento a eliminar

            // Crear una instancia de LugarEvento existente para simular datos en la base de datos
            var lugarEvento = new LugarEvento { Id = lugarEventoId,
                Nombre = "Lugar 1",
                Ciudad = "Ciudad 1",
                Pais = "País 1",
                Capacidad = 100,
                Deshabilitado = false
            };
            var lugarEventos = new List<LugarEvento> { lugarEvento };

            // Configurar el contexto de base de datos (usando Moq y una instancia en memoria de la base de datos)
            var dbContextOptions = new DbContextOptionsBuilder<ExamenIiContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase1")
                .Options;
            var dbContext = new ExamenIiContext(dbContextOptions);
            dbContext.LugarEventos.AddRange(lugarEventos);
            await dbContext.SaveChangesAsync();

            // Se crea la instancia del servicio y se prueba el metodo eliminar()
            var sut = new LugarEventoService(dbContext);
            await sut.DeleteAsync(lugarEventoId);

            // Assert
            var deletedLugarEvento = await dbContext.LugarEventos.FindAsync(lugarEventoId);
            Assert.Null(deletedLugarEvento);
        }

        // Prueba unitaria para el metodo editar() del servicio
        // Datos de entrada: Se elige como entrada un id para identificar el registro que se va a editar en la prueba,
        //      y tambien se crean dos objetos (uno contiene los datos iniciales del registro en la base de datos y
        //      el segundo contiene los nuevos datos con los que se va a editar al primero)
        // Salida esperada: Se comprueba que el registro se encuentra en la base de datos (no retorna Null) y
        //      que los valores de sus atributos correspondan a los del registro editado.
        [Fact]
        public async Task UpdateAsync_ShouldUpdateLugarEventoInDbContext()
        {
            var lugarEventoId = 1; // ID del LugarEvento a editar

            // Crear una instancia de LugarEvento
            var lugarEvento = new LugarEvento { Id = lugarEventoId, Nombre = "Antiguo Nombre", 
                Ciudad = "Antigua Ciudad", Pais = "Antiguo País", Capacidad = 100, Deshabilitado = false };
            var lugarEventos = new List<LugarEvento> { lugarEvento };

            // Crear una instancia de LugarEvento con los datos editados
            var newLugarEvento = new LugarEvento { Id = lugarEventoId, Nombre = "Nuevo Nombre", 
                Ciudad = "Nueva Ciudad", Pais = "Nuevo País", Capacidad = 200, Deshabilitado = true };

            // Configurar el contexto de base de datos (usando Moq y una instancia en memoria de la base de datos)
            var dbContextOptions = new DbContextOptionsBuilder<ExamenIiContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase2")
                .Options;
            var dbContext = new ExamenIiContext(dbContextOptions);
            dbContext.LugarEventos.AddRange(lugarEventos);
            await dbContext.SaveChangesAsync();

            // Se crea la instancia del servicio y se prueba el metodo editar()
            var sut = new LugarEventoService(dbContext);
            var result = await sut.UpdateAsync(lugarEventoId, newLugarEvento);

            // Assert
            Assert.Equal(newLugarEvento, result);

            var updatedLugarEvento = await dbContext.LugarEventos.FindAsync(lugarEventoId);
            Assert.NotNull(updatedLugarEvento);
            Assert.Equal(newLugarEvento.Nombre, updatedLugarEvento.Nombre);
            Assert.Equal(newLugarEvento.Ciudad, updatedLugarEvento.Ciudad);
            Assert.Equal(newLugarEvento.Pais, updatedLugarEvento.Pais);
            Assert.Equal(newLugarEvento.Capacidad, updatedLugarEvento.Capacidad);
            Assert.Equal(newLugarEvento.Deshabilitado, updatedLugarEvento.Deshabilitado);
        }
    }
}