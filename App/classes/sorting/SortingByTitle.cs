
using System.Collections.Generic;

namespace App.classes.sorting
{
    internal class SortingByTitle : IComparer<Events>
    {
        public int Compare(Events x, Events y)
        {
            return string.Compare(x.Title, y.Title);
        }
    }
}
