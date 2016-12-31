using System.Linq;
using System.Collections.Generic;

namespace TrafficApp.Model.Highway
{
    public class Motorway : Road
    {
        public Motorway(string name) : base(name)
        {
        }

        public new IEnumerable<Road> GetAll()
        {
            return Roads.Where(t => t is Motorway);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
