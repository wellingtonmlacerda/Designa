namespace Designa.Models
{
    public class PublicadorReuniaoViewModel
    {
        public IEnumerable<PublicadorParte> PublicadorParte { get; set; } = new List<PublicadorParte>();
        public Reuniao Reuniao { get; set; } = new ();
    }
}
