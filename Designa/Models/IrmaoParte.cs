namespace Designa.Models
{
    public class IrmaoParte
    {
        public int Id { get; set; }
        public int ParteId { get; set; }
        public int IrmaoId { get; set; }
        public Parte Parte { get; set; } = new ();
        public Irmao Irmao { get; set; } = new ();
    }
}
