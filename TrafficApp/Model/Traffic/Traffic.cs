using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrafficApp.Model.Highway;
using Windows.Web.Syndication;
using System.Linq;
using TrafficApp.Factories;

namespace TrafficApp.Model.Traffic
{
    public class Traffic
    {
        private IEnumerable<Event> events;
        //private string trafficURL = "";

        private ICollection<Road> problemRoads;

        public Traffic()
        {
            //TrafficURL = "http://api.hatrafficinfo.dft.gov.uk/datexphase2/dtxRss.aspx?srcUrl=http://hatrafficinfo.dft.gov.uk/feeds/rss/UnplannedEvents.xml&justToday=Y&sortfield=road&sortorder=up";
        }

        //public Traffic(string trafficURL)
        //{
        //    TrafficURL = trafficURL;
        //}

        //public Traffic(ICollection<Event> events)
        //{
        //    Events = events;
        //}

        public async Task<string> Process(bool showMotorways, bool showARoads)
        {
            Events = await TrafficRepositoryFactory.GetRepository("web").GetTrafficProblem();

            return Filter(showMotorways, showARoads);
        }

        private string EventsListToString(IEnumerable<Event> eventList)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Event e in eventList)
            {
                stringBuilder.Append(e.ToString);
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        public string Filter(bool showMotorways, bool showARoads)
        {
            return EventsListToString(Events.Where(e => (e.Road is Motorway && showMotorways) || (e.Road is ARoad && showARoads)));
        }

        public IEnumerable<Event> Events
        {
            get
            {
                return events ?? new List<Event>(); ;
            }

            set
            {
                if (value == null)
                {
                    value = new List<Event>();
                }

                events = value;
            }
        }

        //public string TrafficURL
        //{
        //    get
        //    {
        //        return trafficURL;
        //    }

        //    set
        //    {
        //        trafficURL = value;
        //    }
        //}

        public new string ToString
        {
            get
            {
                return EventsListToString(Events);
            }
        }

        public ICollection<Road> ProblemRoads
        {
            get
            {
                return problemRoads ?? new List<Road>();
            }

            set
            {
                problemRoads = value;
            }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Traffic other = obj as Traffic;

            if (Events.Equals(other.Events))
            {
                return false;
            }

            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return base.GetHashCode();
        }
    }
}