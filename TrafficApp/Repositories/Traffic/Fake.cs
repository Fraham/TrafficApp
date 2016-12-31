using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficApp.Interfaces;
using TrafficApp.Model.Traffic;

namespace TrafficApp.Repositories.Traffic
{
    public class Fake : ITrafficRepository
    {
        public Task<IEnumerable<Event>> GetTrafficProblem()
        {
            throw new NotImplementedException();
        }
    }
}
