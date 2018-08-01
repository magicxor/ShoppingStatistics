using System.Collections.Generic;

namespace ShoppingStatistics.Core.Models
{
    public class StatisticsItem<TKey, TValue>
    {
        public TKey Key { get; set; }
        public IList<TValue> Values { get; set; } = new List<TValue>();
        public int Count { get; set; }
        public double Average { get; set; }
        public double Median { get; set; }
        public TValue Sum { get; set; }
    }
}
