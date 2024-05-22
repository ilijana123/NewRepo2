using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Avtor
    {
        public int Id { get; set; }


        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(50)]
        public string Ime { get; set; }




        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }


        [Required]
        [StringLength(50)]
        public string? Pol { get; set; }


        [Required]
        [StringLength(50)]
        public string? Nacionalnost { get; set; }

        public DateTime? DatumRagjanje { get; set; }

        public ICollection<AvtorKniga>? Knigi { get; set; }
        public string FullName
        {
            get { return String.Format("{0} {1}", Ime, Prezime); }
        }
        

    }
}
