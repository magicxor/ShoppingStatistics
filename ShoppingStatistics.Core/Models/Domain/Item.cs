using System.Collections.Generic;

namespace ShoppingStatistics.Core.Models.Domain
{
    public class Item
    {
        public List<object> modifiers { get; set; }
        public double quantity { get; set; }
        public int ndsNo { get; set; }
        public int sum { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int? nds0 { get; set; }
        public int? nds10 { get; set; }
        public int? nds18 { get; set; }
        public int? ndsCalculated10 { get; set; }
        public int? ndsCalculated18 { get; set; }
        public string barcode { get; set; }
        public List<object> properties { get; set; }
        public int? ndsSum { get; set; }
        public int? nds { get; set; }
        public int? calculationSubjectSign { get; set; }
        public int? ndsRate { get; set; }
        public int? calculationTypeSign { get; set; }
    }
}