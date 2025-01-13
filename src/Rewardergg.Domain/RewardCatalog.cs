using Rewardergg.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Domain
{
    public class RewardCatalog : BaseDomainModel
    {
        public string Name { get; set; }

        public int PointCost { get; set; }
    }
}
