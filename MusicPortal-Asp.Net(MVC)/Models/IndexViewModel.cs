namespace MusicPortal_Asp.Net_MVC_.Models
{
    public class IndexViewModel//по этой моделе типизируется индекс представление
    {
        public IEnumerable<Song> Songs { get; set; }//коллекция игроков
        public PageViewModel PageViewModel { get; }//ссылка на PageViewModel
        public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }

        public IndexViewModel(IEnumerable<Song> songs, PageViewModel pageViewModel,
            FilterViewModel filterViewModel, SortViewModel sortViewModel)
        {
            Songs = songs;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}

