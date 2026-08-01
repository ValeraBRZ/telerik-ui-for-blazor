using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class StockDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Symbol { get; set; }

        public DateTime Date { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public long Volume { get; set; }

        public static Func<Stock, StockDto> StockFunc = (stock) =>
            new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                Date = stock.Date,
                Open = stock.Open,
                High = stock.High,
                Low = stock.Low,
                Close = stock.Close,
                Volume = stock.Volume,
            };
    }
}
