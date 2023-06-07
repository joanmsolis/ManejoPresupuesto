using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TiposCuentas tiposCuentas);
        void Crear(TiposCuentas tiposCuentas);
        Task<bool> existe(string nombre, int usuarioId);
        Task<IEnumerable<TiposCuentas>> Obtener(int usuarioId);
        Task<TiposCuentas> ObtenerPorId(int id, int usuarioId);
    }
    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {
        private readonly string connectionStrings;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionStrings = configuration.GetConnectionString("defaultConnection");

        }
        public void Crear(TiposCuentas tiposCuentas) {
            using var connection = new SqlConnection(connectionStrings);
            var id = connection.QuerySingle<int>($@"insert into tiposCuentas(Nombre, UsuarioId, Orden)
                                                 Values(@Nombre, @UsuarioId, 0);
                                                 SELECT SCOPE_IDENTITY();", tiposCuentas);
            tiposCuentas.Id= id;
            
        }
        public async Task<bool> existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionStrings);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                      @"SELECT 1 
                                      FROM tiposCuentas
                                      WHERE Nombre = Nombre and UsuarioId = @UsuarioId;",
                                      new { nombre, usuarioId });
            return existe == 1;
                                      
        }
        public async Task<IEnumerable<TiposCuentas>> Obtener(int usuarioId) { 
            using var connection = new SqlConnection(connectionStrings);
            return await connection.QueryAsync<TiposCuentas>(@"SELECT Id, Nombre,Orden
                                                               FROM tiposCuentas
                                                               WHERE UsuarioId = @UsuarioId;", new { usuarioId}); 
        }
        public async Task Actualizar(TiposCuentas tiposCuentas) 
        {
            using var connection = new SqlConnection(connectionStrings);
            await connection.ExecuteAsync(@"UPDATE tiposCuentas
                                            SET Nombre = @Nombre
                                            WHERE id = @id", tiposCuentas);
        }
        public async Task<TiposCuentas> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionStrings);
            return await connection.QueryFirstOrDefaultAsync<TiposCuentas>(
                @"SELECT id, Nombre, Orden
                FROM TiposCuentas
                WHERE id= @id AND usuarioId = @UsuarioId",
                new { id, usuarioId });
        }
    }
}
