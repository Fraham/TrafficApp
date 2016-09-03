using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TrafficApp.Model.Highway;
using Windows.Web.Syndication;

namespace TrafficApp.Model.Traffic
{
    public class Traffic
    {
        private List<Event> events = new List<Event>();
        private string trafficURL = "";

        private List<Road> problemRoads = new List<Road>();

        public Traffic()
        {
            TrafficURL = "http://api.hatrafficinfo.dft.gov.uk/datexphase2/dtxRss.aspx?srcUrl=http://hatrafficinfo.dft.gov.uk/feeds/rss/UnplannedEvents.xml&justToday=Y&sortfield=road&sortorder=up";
        }

        public Traffic(string trafficURL)
        {
            TrafficURL = trafficURL;
        }

        public Traffic(List<Event> events)
        {
            Events = events;
        }

        public void Process(string xml)
        {
            /*XmlReader reader = XmlReader.Create(xml);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            ProcessItems(feed.Items);*/
        }

        public async Task Process()
        {
            SyndicationClient client = new SyndicationClient();

            Uri feedUri = new Uri(TrafficURL);

            var feed = await client.RetrieveFeedAsync(feedUri);

            Debug.WriteLine("Got Feed");

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
                catch (Exception ex)
                {

                }
            }
            Debug.WriteLine("Done Items " + Events.Count);
        }

        private void ProcessItems(IEnumerable<SyndicationItem> items)
        {
            Debug.WriteLine("Process Items");
            ProblemRoads = new List<Road>();
            foreach (SyndicationItem item in items)
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
                catch (Exception ex)
                {
                    
                }
            }
        }

        public async void GetRSSFeed()
        {

        }

        public List<Event> Events
        {
            get
            {
                return events;
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
                StringBuilder sb = new StringBuilder();

                foreach (Event e in events)
                {
                    sb.Append(e.ToString);
                    sb.Append(System.Environment.NewLine);
                }

                return sb.ToString();
            }
        }

        public List<Road> ProblemRoads
        {
            get
            {
                return problemRoads;
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