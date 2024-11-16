﻿using System.ComponentModel.DataAnnotations;

namespace firstproject.Models.Dto
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string specialDetails { get; set; }
    }
}
