using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Mvc.BillSales.Models;
//using ApplicationCore.Interfaces.Service;
//using ApplicationCore.Entities;

namespace Web.Mvc.BillSales.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IParametrizacion_accesoService _parametrizacion_accesoService;
        //private UsuariosViewModel obj_UsuariosViewModel = null;

        public AccountController(IParametrizacion_accesoService parametrizacion_accesoService)
        {
            _parametrizacion_accesoService = parametrizacion_accesoService;
            //_session = contextAccessor.HttpContext.Session;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string requestPath)
        {
            //await HttpContext.SignOutAsync(scheme: "FiverSecurityScheme");

            ViewBag.Title = "Inicio de Sesión";
            ViewBag.RequestPath = requestPath ?? "/";
            return View();
        }
                
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel inputModel, string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                //Instancio los objetos Model en sus sessiones correspondientes
                if (obj_UsuariosViewModel == null)
                    obj_UsuariosViewModel = new UsuariosViewModel();

                //Lo guardo en el objeto y es reutilizable
                //WebNetcoreSsigner.Util.HttpSessionExtension.Set<UsuariosViewModel>(session: _session, key : "UsuariosViewModel", value : obj_UsuariosViewModel);
                ObjetoSessionUsuariosViewModel = obj_UsuariosViewModel;
                //HttpContext.Session.SetComplexData("UsuariosViewModel", obj_UsuariosViewModel);

                if (!IsAuthentic(inputModel.Username, inputModel.Password))
                    return View();

                // create claims
                List<Claim> claims = new List<Claim>
                { 
                    new Claim(ClaimTypes.Name, "Sean Connery"),
                    new Claim(ClaimTypes.Email, inputModel.Username)
                };

                // create identity
                ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

                // create principal
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                // sign-in
                await HttpContext.SignInAsync(
                        scheme: "FiverSecurityScheme",
                        principal: principal,
                        properties: new AuthenticationProperties
                        {
                            IsPersistent = inputModel.RememberMe //, // for 'remember me' feature
                                                                 //ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                });

                //return Redirect(inputModel.RequestPath ?? "/");
                return RedirectToAction("Index", "Home");
            }

            return View(inputModel);
        }

        private bool IsAuthentic(string username, string password)
        {
            bool retorno = false;

            //GetSessionUsuariosViewModel(ref obj_UsuariosViewModel);
            //obj_UsuariosViewModel = WebNetcoreSsigner.Util.HttpSessionExtension.Get<UsuariosViewModel>(_session, "UsuariosViewModel");

            //var datos = HttpContext.Session.GetComplexData<UsuariosViewModel>("UsuariosViewModel");
            GetSessionUsuariosViewModel(ref obj_UsuariosViewModel);

            obj_UsuariosViewModel.Ent_Parametrizacion_acceso = _parametrizacion_accesoService.GetById(new Parametrizacion_acceso() {
                usuario_nombre = username
            });

            if (obj_UsuariosViewModel.Ent_Parametrizacion_acceso != null)
            {
                if (obj_UsuariosViewModel.Ent_Parametrizacion_acceso.usuario_clave.Trim() == password.Trim())
                {
                    retorno = true;
                }
                else
                    ModelState.AddModelError("Error2", "Estimado " + username + " su clave es incorrecta.");
            }
            else
            {
                ModelState.AddModelError("Error1", "No existe el usuario ingresado.");
            }

            ObjetoSessionUsuariosViewModel = obj_UsuariosViewModel;

            return retorno;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string requestPath)
        {
            GetSessionAllClear();
            await HttpContext.SignOutAsync(scheme: "FiverSecurityScheme");
            return RedirectToAction("Login");
        }

        public IActionResult Access()
        {
            return View();
        }

        
    }
}