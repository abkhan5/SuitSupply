using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitSupply.DataContracts
{
 public    class StatisticsResponse
    {

        public int Total { get; set; }

        public IEnumerable<Statistic> Statistics { get; set; }
    }
}
