namespace MusicPortal_Asp.Net_MVC_.Models
{
    public class SortViewModel
    {
        public SortState TitleSort { get; set; } // значение для сортировки по имени
        public SortState YearSort { get; set; }    // значение для сортировки по возрасту
        public SortState ArtistSort { get; set; }   // значение для сортировки по клубу
        public SortState GenreSort { get; set; }   // значение для сортировки по клубу
        public SortState Current { get; set; }     // значение свойства, выбранного для сортировки

        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            TitleSort = SortState.TitleAsc;
            YearSort = SortState.YearAsc;
            ArtistSort = SortState.ArtistAsc;
            GenreSort= SortState.GenreAsc;

            TitleSort = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            YearSort = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
            ArtistSort = sortOrder == SortState.ArtistAsc ? SortState.ArtistDesc : SortState.ArtistAsc;
            GenreSort = sortOrder == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
            Current = sortOrder;
        }
    }
}
