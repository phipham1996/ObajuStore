using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml;

namespace ObajuStore.Common.Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrDefault<T>(this T? value) where T : struct
        {
            return default(T).Equals(value.GetValueOrDefault());
        }

        public static string ElText(this XmlNode node, string elName)
        {
            return node.SelectSingleNode(elName).InnerText;
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            return o == null ? failureValue : evaluator(o);
        }

        public static SelectList GenderSelectList()
        {
            var selectList = new SelectList(
                   new List<SelectListItem>
                   {
                        new SelectListItem {Text = "Nam", Value = "Nam"},
                        new SelectListItem {Text = "Nữ", Value = "Nữ"},
                   }, "Value", "Text");
            return selectList;
        }
    }
}