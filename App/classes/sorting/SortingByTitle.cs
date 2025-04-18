
using System.Collections.Generic;

namespace App.classes.sorting
{
    /// <summary>
    /// сортировка по порядку (по названию)
    /// </summary>
    internal class SortingByTitle : IComparer<Events>
    {
        public int Compare(Events x, Events y)
        {
            return string.Compare(x.Title, y.Title);
        }
    }
}
