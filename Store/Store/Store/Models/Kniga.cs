using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Store.Models
{
    public class Kniga
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Naslov { get; set; }


        public int? Godina { get; set; }
        public int? BrStrani { get; set; }


        [StringLength(int.MaxValue)]
        public string? Opis{ get; set; }


        [StringLength(50)]
        public string? Zanr { get; set; }

        public int? Tirazh { get; set; }


        [StringLength(100)]
        public string? Izdavac { get; set; }


        [StringLength(int.MaxValue)]
        [Display(Name = "Slika")]

        public string? SlikaUrl { get; set; }
        [NotMapped]
        public IFormFile? SlikaFile { get; set; }

        public ICollection<AvtorKniga>? Avtori { get; set; }
    }
}
