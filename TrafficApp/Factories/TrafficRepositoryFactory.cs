using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficApp.Interfaces;
using TrafficApp.Repositories.Traffic;

namespace TrafficApp.Factories
{
    public class TrafficRepositoryFactory
    {
        public static ITrafficRepository GetRepository(string repo)
        {
            if (repo.ToLower().Equals("web"))
            {
                return new Web();
            }
            else
            {
                return new Fake();
            }
        }
    }
}
