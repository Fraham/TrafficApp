using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficApp.Interfaces;
using TrafficApp.Model.Traffic;
using Windows.Web.Syndication;

namespace TrafficApp.Repositories.Traffic
{
    public class Web : ITrafficRepository
    {

        private string trafficURL = "http://api.hatrafficinfo.dft.gov.uk/datexphase2/dtxRss.aspx?srcUrl=http://hatrafficinfo.dft.gov.uk/feeds/rss/UnplannedEvents.xml&justToday=Y&sortfield=road&sortorder=up";

        public async Task<IEnumerable<Event>> GetTrafficProblem()
        {
            var events = new List<Event>();

            SyndicationClient client = new SyndicationClient();

            Uri feedUri = new Uri(trafficURL);

            var feed = await client.RetrieveFeedAsync(feedUri);

            foreach (SyndicationItem item in feed.Items)
            {
                try
                {
                    Event newEvent = new Event(item.Title.Text, item.Summary.Text);
                    newEvent.Process();
                    events.Add(newEvent);

                    //if (!ProblemRoads.Contains(newEvent.Road))
                    //{
                    //    ProblemRoads.Add(newEvent.Road);
                    //}
                }
                catch (Exception)
                {
                }
            }

            return events;
        }
    }
}
