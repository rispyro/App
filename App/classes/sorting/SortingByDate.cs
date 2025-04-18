using System;
using System.Collections.Generic;

namespace App.classes.sorting
{
    /// <summary>
    /// Сортировка по дате
    /// </summary>
    internal class SortingByDate : IComparer<Events>
    {
        public int Compare(Events x, Events y)
        {
            DateTime dx, dy;
            DateTime.TryParse(x.Date, out dx);
            DateTime.TryParse(y.Date, out dy);
            return dx.CompareTo(dy);
        }
    }
}
