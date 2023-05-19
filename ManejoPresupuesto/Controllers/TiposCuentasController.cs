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
        private readonly IServiciosUsuarios serviciosUsuarios;

        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas,
            IServiciosUsuarios serviciosUsuarios)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuasriosId();
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
            return View(tiposCuentas);
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
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> verificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuasriosId();
            var yaExisteTipoCuenta = await repositorioTiposCuentas.existe(nombre, usuarioId);

            if (yaExisteTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe");
            }
            return Json(true);
        }
    }
}
