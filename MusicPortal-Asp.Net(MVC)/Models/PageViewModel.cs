namespace MusicPortal_Asp.Net_MVC_.Models
{
    public class PageViewModel//инкапсулирует всю необходимую инфу для пагинации
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PageViewModel(int count, int pageNumber, int pageSize)//в конструктор из контроллера передается такая инфа
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
