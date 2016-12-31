using System.Collections.Generic;
using System.Linq;

namespace TrafficApp.Model.Highway
{
    public class ARoad : Road
    {
        public ARoad(string name) : base(name)
        {

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

        public new IEnumerable<Road> GetAll()
        {
            return Roads.Where(t => t is ARoad);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
