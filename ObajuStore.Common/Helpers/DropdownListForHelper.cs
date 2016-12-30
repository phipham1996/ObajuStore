using System.Collections.Generic;
using System.Web.Mvc;

namespace ObajuStore.Common.Helpers
{
    public static class DropdownListForHelper<T>
    {
        public static SelectList GetDropdownName(IEnumerable<T> source, string value, string name, int? selectedID = null)
        {
            return new SelectList(source, value, name, selectedID);
        }
    }
}