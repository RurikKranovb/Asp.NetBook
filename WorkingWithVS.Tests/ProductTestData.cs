﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using WorkingWithVS.Models;

namespace WorkingWithVS.Tests
{
    public class ProductTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { GetPricesUnder50() };
            yield return new object[] { GetPricesOver50() };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<Product> GetPricesUnder50()
        {
            decimal[] prices = new decimal[] { 275, 49.9M, 19.50M, 24.95M };
            for (int i = 0; i < prices.Length; i++)
            {
                yield return new Product() { Name = $"P{i + 1}", Price = prices[i] };
            }
        }
   
        private Product[] GetPricesOver50() => new Product[]
        {
                new Product() { Name = "P1", Price = 5 },
                new Product() { Name = "P2", Price = 48.95M },
                new Product() { Name = "P3", Price = 19.50M },
                new Product() { Name = "P4", Price = 24.95M },
        };
    }
}
