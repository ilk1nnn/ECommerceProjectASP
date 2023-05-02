﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace ECommerce.WebUI.TagHelpers
{
    [HtmlTargetElement("product-list-pagerZA")]
    public class PagingTagHelperZA : TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            var sb = new StringBuilder();
            sb.Append("<ul class='pagination'>");
            for (int i = 1; i <= PageCount; i++)
            {
                sb.AppendFormat("<li class='{0}'>", (i == CurrentPage) ? "page-item active" : "page-item");
                sb.AppendFormat("<a class='page-link' href='/product/SortZToA?page={0}&category={1}'>{2}</a>", i,
                    CurrentCategory, i);
                sb.Append("</li>");
            }
            sb.Append("</ul>");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
