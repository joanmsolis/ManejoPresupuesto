﻿using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TiposCuentas // IValidatableObject
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="el campo {0} es requerido")]
      
        public string Nombre { get; set; }
        public int UsuarioId { get;set; }
        public int Orden { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Nombre != null && Nombre.Length > 0)
        //    { 
        //        var primereLetra = Nombre[0].ToString();

        //        if(primereLetra != primereLetra.ToUpper())
        //                {
        //            yield return new ValidationResult("la primera letra debe de ser mayscula",
        //                new[] { nameof(Nombre)});
        //        }
        //    }
        //}
    }
}
