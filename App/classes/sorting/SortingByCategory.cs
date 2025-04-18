
using System.Collections.Generic;

namespace App.classes.sorting
{
    internal class SortingByCategory : IComparer<Events>
    {
        public int Compare(Events x, Events y)
        {
            return string.Compare(x.Category, y.Category);
        }
    }
}
