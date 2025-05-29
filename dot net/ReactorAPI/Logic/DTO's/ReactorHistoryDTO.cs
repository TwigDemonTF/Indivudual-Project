using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO_s
{
    public class ReactorHistoryDTO
    {
        public int ReactorId { get; set; }
        public int Temperature { get; set; }
        public int FieldStrength { get; set; }
        public int EnergySaturation { get; set; }
        public int FuelExhaustion { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
