using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zq.Domain
{
    public class Entity<TIdentity> : IEntity
    {
        public TIdentity Id { get; set; }
    }
}
