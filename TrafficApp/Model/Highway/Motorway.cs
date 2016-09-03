using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficApp.Model.Highway
{
    public class Motorway : Road
    {
        public Motorway(string name) : base(name)
        {
        }

        public new List<Road> GetAll()
        {
            List<Road> roads = new List<Road>();

            foreach (Road road in Roads)
            {
                if (road is Motorway)
                {
                    roads.Add(road);
                }
            }

            return roads;
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
