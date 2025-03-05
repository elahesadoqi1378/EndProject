using Achareh.Domain.Core.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achareh.Domain.Core.Entities.BaseEntities
{
    public class Image
    {
        #region Properties
        public int Id { get; set; }
        public string Path { get; set; }
        public int RequestId { get; set; }
        #endregion
        
    }
}
