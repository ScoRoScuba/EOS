namespace EOS2.Web.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class InternationalizationHelpers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Neccessary complexity")]
        public static MvcHtmlString DropDownListI18NFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            var selectListItems = selectList as SelectListItem[] ?? selectList.ToArray();
            foreach (var selectListItem in selectListItems)
            {
                selectListItem.Text = "[[[" + selectListItem.Text + "]]]";
            }

            return htmlHelper.DropDownListFor(expression, selectListItems, optionLabel, htmlAttributes);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Neccessary complexity")]
        public static MvcHtmlString DropDownListI18NFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SelectList selectList, object htmlAttributes)
        {
            if (selectList == null) throw new ArgumentNullException("selectList");

            foreach (var selectListItem in selectList)
            {
                selectListItem.Text = "[[[" + selectListItem.Text + "]]]";
            }

            return htmlHelper.DropDownListFor(expression, selectList.Items.Cast<SelectListItem>(), htmlAttributes);
        }
    }
}