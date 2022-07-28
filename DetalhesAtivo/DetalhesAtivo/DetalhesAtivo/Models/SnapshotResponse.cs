using System.Collections.Generic;

namespace DetalhesAtivo.Models
{
    public class SnapshotResponse
    {
        public IEnumerable<Security> Value { get; set; }

        public class Security
        {
            public string Symbol { get; set; }

            public SecurityProperties Properties { get; set; } = new SecurityProperties();

            public class SecurityProperties
            {
                public decimal? OpeningPrice { get; set; }
                public decimal? MaxPrice { get; set; }
                public decimal? MinPrice { get; set; }
                public decimal? AveragePrice { get; set; }
                public decimal? Price { get; set; }
            }
        }
    }
}