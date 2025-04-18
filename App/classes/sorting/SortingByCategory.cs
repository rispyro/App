
using System.Collections.Generic;

namespace App.classes.sorting
{
    /// <summary>
    /// Сортировка по категории
    /// </summary>
    internal class SortingByCategory : IComparer<Events>
    {
        public int Compare(Events x, Events y)
        {
            return string.Compare(x.Category, y.Category);
        }
    }
}
