namespace Store.Models
{
    public class AvtorKniga
    {

        public int Id { get; set; }
        public int KnigaId { get; set; }
        public int AvtorId { get; set; }
        public Kniga? Kniga { get; set; }
        public Avtor? Avtor { get; set; }
    }
}
