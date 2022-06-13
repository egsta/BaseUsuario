using BaseUsuario.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace BaseUsuario.ViewModels
{



    // private readonly Usuarios ?usuario;

    public class LoginViewModels
    {
        [Required(ErrorMessage = "Escriba su Email")]
       
        public string UserName { get; set; }

        [Required(ErrorMessage = "Escriba su Password")]

        public string Password { get; set; }
    }

    


}
