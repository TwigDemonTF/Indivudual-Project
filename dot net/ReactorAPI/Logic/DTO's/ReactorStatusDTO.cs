using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO_s
{
    public class ReactorStatusDTO
    {
        public double Temperature { get; set; }
        public double EnergySaturation { get; set; }
        public double FieldStrength { get; set; }
        public double FuelExhaustion { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ReactorId { get; set; }

    }
}
