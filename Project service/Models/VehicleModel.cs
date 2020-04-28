using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Project_service.Models
{
    [Table("VehicleModel")]
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }

        
        public VehicleMake Make { get; set; }
        public  int MakeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required field!")]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required field!")]
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Abreveation")]
        public string Abrv { get; set; }

    }
}
