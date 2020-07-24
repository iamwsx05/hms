using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weCare.Core.Entity;

namespace Hms.Entity
{
    public class EntityDisplayClientModelAcess : BaseDataContract
    {
        public string modelName { get; set; }
        public string modelScore { get; set; }
        public string modelResult { get; set; }
        public string modelScoreAndResult { get; set; }
    }
}
