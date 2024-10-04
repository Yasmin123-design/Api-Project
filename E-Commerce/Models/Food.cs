﻿namespace E_Commerce.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
