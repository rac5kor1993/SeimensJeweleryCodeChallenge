using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.BuisnessLogic
{
    public class GoldPriceCalculation
    {
        public double GoldPrice { get; set; }
        public double Weight { get; set; }
        public decimal TotalPrice { get; set; }
        public float Discount { get; set; }
        public decimal DiscountPrice { get; set; }
        public double GetGoldPrice { get { return GoldPrice; } }
        public double GetWeight { get { return Weight; } }
        public decimal GetTotalPrice { get { return TotalPrice; } }
        public float GetDiscount { get { return Discount; } }
        
        //public GoldPriceCalculation(GoldPriceCalculation _CalculateGoldPrice)
        //{
        //    this.Discount = _CalculateGoldPrice.Discount;
        //    this.Weight = _CalculateGoldPrice.Weight;
        //    this.TotalPrice = _CalculateGoldPrice.TotalPrice;
        //    this.GoldPrice = _CalculateGoldPrice.GoldPrice;
        //}

        public void GetTotalValue(float discount)
        {
            this.DiscountPrice= Convert.ToDecimal( (this.Weight * this.GoldPrice * discount) / 100);
            this.TotalPrice= Convert.ToDecimal( this.Weight * this.GoldPrice) - this.DiscountPrice;
        }
    }
}
