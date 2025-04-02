using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MusicPortal_Asp.Net_MVC_.Models;

namespace MusicPortal_Asp.Net_MVC_.TagHelpers
{
    public class SortHeaderTagHelper : TagHelper
    {
        public SortState Property { get; set; } // значение текущего свойства, для которого создается тег
        public SortState Current { get; set; }  // значение активного свойства, выбранного для сортировки
        public string? Action { get; set; }  // действие контроллера, на которое создается ссылка
        public bool Up { get; set; }    // сортировка по возрастанию или убыванию

        [ViewContext]
        public ViewContext ViewContext { get; set; } = null!;//чтоб понимать  какой вьюшке будет использоватья тегхелпер.БУДЕТ САМО ЗАПОЛНЕНО КОНТЕКСТОМ ВЬЮШКИ

        IUrlHelperFactory urlHelperFactory;
        public SortHeaderTagHelper(IUrlHelperFactory helperFactory)//ФАБРИКА ИНЖЕКТИРУЕТСЯ
        {
            urlHelperFactory = helperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)//САМ ВЫЗЫВАЕТСЯ 4 РАЗА И РЕНДЕРИТ
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";//УКАЗАЛИ ЧТО ЭТО ГИПЕРССЫЛКА
            string? url = urlHelper.Action(Action, new { sortOrder = Property });
            output.Attributes.SetAttribute("href", url);//ДОБАВЛЯЕМ АТРИБУТ  И УКАЗЫАЕМ ТОТ МАРШРУТ КОТОРЫЙ УЖЕ СОЗДАЛИ urlHelper

            output.Attributes.SetAttribute("class", "btn btn-outline-theme btn-sm sort-link me-2");
            // если текущее свойство имеет значение CurrentSort
            if (Current == Property)
            {
                TagBuilder tag = new TagBuilder("i");
                tag.AddCssClass("glyphicon");

                if (Up == true)   // если сортировка по возрастанию
                    tag.AddCssClass("glyphicon-chevron-up");
                else   // если сортировка по убыванию
                    tag.AddCssClass("glyphicon-chevron-down");

                output.PreContent.AppendHtml(tag);//ДОБАВЛЯЕМ ПСЕВДО ЭЛЕМЕНТ ПЕРЕД ГИПЕРССЫЛКОЙ
            }
        }
    }
}
