using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficApp.Model.Traffic;

namespace TrafficApp.Interfaces
{
    public interface ITrafficRepository
    {
        Task<IEnumerable<Event>> GetTrafficProblem();
    }
}
