using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PREGUNTA_02_EINER_CHAVEZ.Data;
using PREGUNTA_02_EINER_CHAVEZ.Models;
using PREGUNTA_02_EINER_CHAVEZ.ViewModels;
using System.Threading.Tasks;

namespace PREGUNTA_02_EINER_CHAVEZ.Controllers
{
    public class RegistrosController : Controller
    {
        private readonly PREGUNTA_02_EINER_CHAVEZContext _context;

        public RegistrosController(PREGUNTA_02_EINER_CHAVEZContext context)
        {
            _context = context;
        }

        // GET: Registros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Usuario,Correo,Contraseña,ConfirmarContraseña")] UsuarioVM usuarioVM)
        {
            if (ModelState.IsValid)
            {
   
                if (usuarioVM.Contraseña != usuarioVM.ConfirmarContraseña)
                {
                    ModelState.AddModelError("ConfirmarContraseña", "Las contraseñas no coinciden.");
                    return View(usuarioVM); 
                }
                var existingUser = await _context.Usuarios
                    .FirstOrDefaultAsync(r => r.Correo == usuarioVM.Correo);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Correo", "Este correo ya está registrado.");
                    return View(usuarioVM); 
                }

                var registro = new Registro
                {
                    Usuario = usuarioVM.Usuario,
                    Correo = usuarioVM.Correo,
                    Contraseña = usuarioVM.Contraseña 
                };

                _context.Add(registro);
                await _context.SaveChangesAsync();

                TempData["IsSuccessful"] = true;

                return RedirectToAction("Create");
            }

            return View(usuarioVM); 
        }
    }
}
