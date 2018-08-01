namespace ShoppingStatistics.Core.Models.Domain
{
    public class Modifier
    {
        public long? discountSum { get; set; }
        public string discountName { get; set; }
        public string markupName { get; set; }
        public long? markupSum { get; set; }
    }
}
