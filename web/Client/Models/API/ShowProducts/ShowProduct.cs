﻿namespace FMFT.Web.Client.Models.API.ShowProducts
{
    public class ShowProduct
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }
    }
}
