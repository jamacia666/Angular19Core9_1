namespace PeliculasAPI.DTOs
{
    public class LandingPageDTO
    {
        public List<PeliculaDTO> EnCines { get; set; } = new List<PeliculaDTO>();
        public List<PeliculaDTO> ProximosExtrenos { get; set; } = new List<PeliculaDTO>();

    }
}
