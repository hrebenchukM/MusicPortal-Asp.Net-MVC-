using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MusicPortal_Asp.Net_MVC_.Models;

namespace MusicPortal_Asp.Net_MVC_.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        public ViewContext ViewContext { get; set; } = null!;
        public PageViewModel? PageModel { get; set; }//ССЫЛКА НА ВЬЮМОДЕЛЬ БЫЛА ПЕРЕДАНА В ТЕГХЕЛПЕР ЧЕРЕЩЗ ВЬЮШКУ       
        public string PageAction { get; set; } = "";

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]//ЧТОБЫ ПО ОТДЕЛЬНОСТИ НЕ ДОСТАВАТЬ ОН БУДЕТ АВТОМАТИЧЕСКИ ЗАПОЛНЯТСЯ
        public Dictionary<string, object> PageUrlValues { get; set; } = new();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (PageModel == null) throw new Exception("PageModel is not set");
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            // набор ссылок будет представлять список ul
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination mt-5 justify-content-center");

            // формируем три ссылки - на текущую, предыдущую и следующую
            TagBuilder currentItem = CreateTag(PageModel.PageNumber, urlHelper);

            // создаем ссылку на предыдущую страницу, если она есть
            if (PageModel.HasPreviousPage)
            {
                TagBuilder prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper, "bi-chevron-double-left me-1");
                tag.InnerHtml.AppendHtml(prevItem);
            }
            else
            {
                TagBuilder prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper, "bi-chevron-double-left me-1", true);
                tag.InnerHtml.AppendHtml(prevItem);
            }

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder pageItem = CreateTag(i, urlHelper);
                tag.InnerHtml.AppendHtml(pageItem);
            }

            //tag.InnerHtml.AppendHtml(currentItem);
            // создаем ссылку на следующую страницу, если она есть
            if (PageModel.HasNextPage)
            {
                TagBuilder nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper, "bi-chevron-double-right ms-1");
                tag.InnerHtml.AppendHtml(nextItem);
            }
            else
            {
                TagBuilder nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper, "bi-chevron-double-right ms-1", true);
                tag.InnerHtml.AppendHtml(nextItem);
            }
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper, string? iconClass = null, bool isDisabled = false)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (isDisabled)
            {
                item.AddCssClass("disabled");
                link.Attributes["href"] = "#";
            }
            else
            {
                if (pageNumber == PageModel?.PageNumber)
                {
                    item.AddCssClass("active");
                }
                else
                {

                    PageUrlValues["page"] = pageNumber;
                    link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                }

            }
            item.AddCssClass("page-item");

            if (!string.IsNullOrEmpty(iconClass))
            {
                link.InnerHtml.AppendHtml($"<i class=\"bi {iconClass}\"></i>");
            }
            else
            {
                link.InnerHtml.Append(pageNumber.ToString());
            }

            link.AddCssClass("page-link");
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}