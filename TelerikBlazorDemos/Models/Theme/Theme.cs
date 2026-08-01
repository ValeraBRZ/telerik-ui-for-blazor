using System.Collections.Generic;

namespace TelerikBlazorDemos.Models
{
    public class Theme
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Primary { get; set; }

        public List<Swatch> Swatches { get; set; }
    }
}
