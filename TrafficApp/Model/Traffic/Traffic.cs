using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrafficApp.Model.Highway;
using Windows.Web.Syndication;
using System.Linq;

namespace TrafficApp.Model.Traffic
{
    public class Traffic
    {
        private ICollection<Event> events;
        private string trafficURL = "";

        private ICollection<Road> problemRoads;

        public Traffic()
        {
            TrafficURL = "http://api.hatrafficinfo.dft.gov.uk/datexphase2/dtxRss.aspx?srcUrl=http://hatrafficinfo.dft.gov.uk/feeds/rss/UnplannedEvents.xml&justToday=Y&sortfield=road&sortorder=up";
        }

        public Traffic(string trafficURL)
        {
            TrafficURL = trafficURL;
        }

        public Traffic(ICollection<Event> events)
        {
            Events = events;
        }

        public async Task<string> Process(bool showMotorways, bool showARoads)
        {
            Events = new List<Event>();

            SyndicationClient client = new SyndicationClient();

            Uri feedUri = new Uri(TrafficURL);

            var feed = await client.RetrieveFeedAsync(feedUri);

            foreach (SyndicationItem item in feed.Items)
            {
                try
                {
                    Event newEvent = new Event(item.Title.Text, item.Summary.Text);
                    newEvent.Process();
                    Events.Add(newEvent);

                    if (!ProblemRoads.Contains(newEvent.Road))
                    {
                        ProblemRoads.Add(newEvent.Road);
                    }
                }
                catch (Exception)
                {
                }
            }

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

        public ICollection<Event> Events
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

        public string TrafficURL
        {
            get
            {
                return trafficURL;
            }

            set
            {
                trafficURL = value;
            }
        }

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