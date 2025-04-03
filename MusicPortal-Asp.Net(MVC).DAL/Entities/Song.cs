namespace MusicPortal_Asp.Net_MVC_.DAL.Entities
{
    public class Song
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public int ArtistId { get; set; }

        public int GenreId { get; set; }

        public int UserId { get; set; }

        public string? PathS { get; set; }
        public string? PathV { get; set; }
        public string? PathP { get; set; }

        public Genre? Artist { get; set; }
        public Genre? Genre { get; set; }
        public User? User { get; set; }
    }
}


