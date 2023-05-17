using Microsoft.AspNetCore.Mvc;
using ManejoPresupuesto.Models;
using System.Data.SqlClient;
using Dapper;
using ManejoPresupuesto.Servicios;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
       
        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
                
        }
        public IActionResult Crear()
        {

                return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(TiposCuentas tiposCuentas)
        {
            if (!ModelState.IsValid)
            { 
                return View(tiposCuentas);
            }
            tiposCuentas.UsuarioId = 1;
            var yaExisteTipoCuenta = 
                await repositorioTiposCuentas.existe(tiposCuentas.Nombre, tiposCuentas.UsuarioId);
            if (!yaExisteTipoCuenta) {
                ModelState.AddModelError(nameof(tiposCuentas.Nombre),
                    $"EL nombre { tiposCuentas.Nombre } ya exixte");
                return View(tiposCuentas);
            }
            repositorioTiposCuentas.Crear(tiposCuentas);
            return View();
        }
    }
}
