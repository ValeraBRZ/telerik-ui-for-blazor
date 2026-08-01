using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class IntradayDto
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

        public static Func<Intraday, IntradayDto> IntradayFunc = (intraday) =>
            new IntradayDto
            {
                Id = intraday.Id,
                Symbol = intraday.Symbol,
                Date = intraday.Date,
                Open = intraday.Open,
                High = intraday.High,
                Low = intraday.Low,
                Close = intraday.Close,
                Volume = intraday.Volume,
            };
    }
}
