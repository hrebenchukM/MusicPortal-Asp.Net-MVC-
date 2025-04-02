namespace MusicPortal_Asp.Net_MVC_.Models
{
    public class SortViewModel
    {
        public SortState TitleSort { get; set; } // значение для сортировки по имени
        public SortState YearSort { get; set; }    // значение для сортировки по возрасту
        public SortState ArtistSort { get; set; }   // значение для сортировки по клубу
        public SortState GenreSort { get; set; }   // значение для сортировки по клубу
        public SortState Current { get; set; }     // значение свойства, выбранного для сортировки
        public bool Up { get; set; }  // Сортировка по возрастанию или убыванию
        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            TitleSort = SortState.TitleAsc;
            YearSort = SortState.YearAsc;
            ArtistSort = SortState.ArtistAsc;
            GenreSort= SortState.GenreAsc;


            Up = true;

            if (sortOrder == SortState.YearDesc || sortOrder == SortState.TitleDesc
                || sortOrder == SortState.ArtistDesc || sortOrder == SortState.GenreDesc)
            {
                Up = false;
            }


            switch (sortOrder)
            {
                case SortState.TitleDesc:
                    Current = TitleSort = SortState.TitleAsc;
                    break;
                case SortState.YearAsc:
                    Current = YearSort = SortState.YearDesc;
                    break;
                case SortState.YearDesc:
                    Current = YearSort = SortState.YearAsc;
                    break;
                case SortState.ArtistAsc:
                    Current = ArtistSort = SortState.ArtistDesc;
                    break;
                case SortState.ArtistDesc:
                    Current = ArtistSort = SortState.ArtistAsc;
                    break;
                case SortState.GenreAsc:
                    Current = GenreSort = SortState.GenreDesc;
                    break;
                case SortState.GenreDesc:
                    Current = GenreSort = SortState.GenreAsc;
                    break;
                default:
                    Current = TitleSort = SortState.TitleDesc;
                    break;
            }


        }
    }
}
