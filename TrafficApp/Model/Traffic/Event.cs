using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using TrafficApp.Model.Highway;
using TrafficApp.Model.Problem;

namespace TrafficApp.Model.Traffic
{
    public class Event
    {
        private string location;
        private string status;
        private string timeToClear;
        private string returnToNormal;
        private string lanesClosed;
        private TrafficProblem reason;
        private string delay;

        private Road road;
        private string startClear;
        private string endClear;
        private string startNormal;
        private string endNormal;
        private int delayedMinutes;
        private string direction;
        private string areaEffected;

        private string title;
        private string summary;

        //private Traffic traffic;

        public Event(string title, string summary)
        {
            Title = title;
            Summary = summary;
        }

        public Event(string location, string status, string timeToClear, string returnToNormal, string lanesClosed, TrafficProblem reason, Road road, string startClear, string endClear, string startNormal, string endNormal, string delay, int delayedMinutes, string direction, string areaAffected)
        {
            Location = location;
            Status = status;
            TimeToClear = timeToClear;
            ReturnToNormal = returnToNormal;
            LanesClosed = lanesClosed;
            Reason = reason;
            Road = road;
            StartClear = startClear;
            EndClear = endClear;
            StartNormal = startNormal;
            EndNormal = endNormal;
            Delay = delay;
            DelayedMinutes = delayedMinutes;
            Direction = direction;
            AreaEffected = areaAffected;
        }

        public Event(string location, string status, string timeToClear, string returnToNormal, string lanesClosed, TrafficProblem reason, string delay)
        {
            Location = location;
            Status = status;
            TimeToClear = timeToClear;
            ReturnToNormal = returnToNormal;
            LanesClosed = lanesClosed;
            Reason = reason;
            Delay = delay;
        }

        public void Process()
        {
            Status = ProcessRegex(@"Status : (.*)", 1).Trim();
            Location = ProcessRegex(@"Location : The (.*)", 1).Trim();
            TimeToClear = ProcessRegex(@"Time To Clear : (.*)", 1).Trim();
            ReturnToNormal = ProcessRegex(@"Return To Normal : (.*)", 1).Trim();
            LanesClosed = ProcessRegex(@"Lanes Closed : (.*)", 1).Trim();
            Reason = TrafficProblem.GetProblem(ProcessRegex(@"Reason : (.*)", 1).Trim());
            Delay = ProcessRegex(@"Delay : (.*)", 1).Trim();
        }

        private string ProcessRegex(string regexString, int group, string matchingWith, int index)
        {
            Regex regex = new Regex(regexString);
            MatchCollection match = regex.Matches(matchingWith);

            if (match.Count <= index)
            {
                return "";
            }

            return match[index].Groups[group].Success ? match[index].Groups[group].Value : "";
        }

        private string ProcessRegex(string regexString, int group, string matchingWith)
        {
            return ProcessRegex(regexString, group, matchingWith, 0);
        }

        private string ProcessRegex(string regexString, int group)
        {
            return ProcessRegex(regexString, group, summary);
        }

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }

                location = value;

                Road = Road.GetRoad(ProcessRegex(@"\b[MAa-z0-9]+\b", 0, Location));
                Direction = ProcessRegex(@"(eastbound|westbound|northbound|southbound|clockwise|anticlockwise)", 0, Location);
                AreaEffected = ProcessRegex(@"\b[MAa-z0-9]+\b \b[a-z0-9]+\b (.*) ", 1, Location);
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }

                status = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Summary
        {
            get
            {
                return summary;
            }

            set
            {
                summary = value;
            }
        }

        public string TimeToClear
        {
            get
            {
                return timeToClear;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }

                timeToClear = value;

                StartClear = ProcessRegex(@"([0-9]+:[0-9]+)", 0, TimeToClear, 0);
                EndClear = ProcessRegex(@"([0-9]+:[0-9]+)", 0, TimeToClear, 1);
            }
        }

        public string ReturnToNormal
        {
            get
            {
                return returnToNormal;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }

                returnToNormal = value;

                StartNormal = ProcessRegex(@"([0-9]+:[0-9]+)", 0, ReturnToNormal, 0);
                EndNormal = ProcessRegex(@"([0-9]+:[0-9]+)", 0, ReturnToNormal, 1);
            }
        }

        public string LanesClosed
        {
            get
            {
                return lanesClosed;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }

                lanesClosed = value;
            }
        }

        public TrafficProblem Reason
        {
            get
            {
                return reason;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = TrafficProblem.GetProblem("Unknown");
                }

                reason = value;
            }
        }

        public new string ToString
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(string.Format("Road: {0}", Road.Name));

                if (!Direction.Equals("Unknown"))
                {
                    sb.Append(string.Format(", Direction: {0}", Direction));
                }
                if (!AreaEffected.Equals("Unknown"))
                {
                    sb.Append(string.Format(", Area: {0}", AreaEffected));
                }
                if (!Status.Equals("Unknown"))
                {
                    sb.Append(string.Format(", Status: {0}", Status));
                }
                if (!StartClear.Equals("Unknown"))
                {
                    sb.Append(string.Format(", Time To Clear: {0}", StartClear + " - " + EndClear));
                }
                if (!StartNormal.Equals("Unknown"))
                {
                    sb.Append(string.Format(", Return To Normal: {0}", StartNormal + " - " + EndNormal));
                }
                if (!LanesClosed.Equals("Unknown"))
                {
                    sb.Append(string.Format(", Lanes Closed: {0}", LanesClosed));
                }
                if (!Reason.Reason.Equals("Unknown"))
                {
                    sb.Append(string.Format(", Reason: {0}", Reason.ToString));
                }
                if (!Delay.Equals("Unknown"))
                {
                    sb.Append(string.Format(", Delay: {0}", Delay));
                }

                return sb.ToString();
            }
        }

        public Road Road
        {
            get
            {
                return road;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = Road.GetRoad("Unknown");
                }
                road = value;

                //if (traffic)
            }
        }

        public string StartClear
        {
            get
            {
                return startClear;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }
                startClear = value;
            }
        }

        public string EndClear
        {
            get
            {
                return endClear;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }
                endClear = value;
            }
        }

        public string StartNormal
        {
            get
            {
                return startNormal;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }
                startNormal = value;
            }
        }

        public string EndNormal
        {
            get
            {
                return endNormal;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }
                endNormal = value;
            }
        }

        public string Delay
        {
            get
            {
                return delay;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }

                delay = value;

                if (!Delay.Equals("Unknown"))
                {
                    Regex regex = new Regex(@"(([0-9]+) minutes)|(([0-9]+)( and |)(three quarters|a half|a quarter|) hours)");
                    Match match = regex.Match(Delay);

                    if (match.Success)
                    {
                        if (match.Groups[2].Success)
                        {
                            try
                            {
                                DelayedMinutes = int.Parse(match.Groups[2].Value);
                            }
                            catch (FormatException)
                            {
                                DelayedMinutes = 0;
                            }
                        }
                        else if (match.Groups[4].Success && match.Groups[6].Success)
                        {
                            int minutes = 0;

                            try
                            {
                                minutes = int.Parse(match.Groups[4].Value) * 60;
                            }
                            catch (FormatException)
                            {
                                minutes = 0;
                            }

                            if (match.Groups[6].Value.Equals("three quarters"))
                            {
                                minutes += 45;
                            }
                            else if (match.Groups[6].Value.Equals("a half"))
                            {
                                minutes += 30;
                            }
                            else if (match.Groups[6].Value.Equals("a quarter"))
                            {
                                minutes += 15;
                            }

                            DelayedMinutes = minutes;
                        }
                    }
                    else
                    {
                        DelayedMinutes = 0;
                    }
                }
            }
        }

        public int DelayedMinutes
        {
            get
            {
                return delayedMinutes;
            }

            set
            {
                delayedMinutes = value;
            }
        }

        public string Direction
        {
            get
            {
                return direction;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }

                direction = value;
            }
        }

        public string AreaEffected
        {
            get
            {
                return areaEffected;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }

                areaEffected = value;
            }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Event other = obj as Event;

            if (!Road.Equals(other.Road))
            {
                return false;
            }
            if (!Location.Equals(other.Location))
            {
                return false;
            }
            if (!Status.Equals(other.Status))
            {
                return false;
            }
            if (!TimeToClear.Equals(other.TimeToClear))
            {
                return false;
            }
            if (!StartClear.Equals(other.StartClear))
            {
                return false;
            }
            if (!EndClear.Equals(other.EndClear))
            {
                return false;
            }
            if (!ReturnToNormal.Equals(other.ReturnToNormal))
            {
                return false;
            }
            if (!StartNormal.Equals(other.StartNormal))
            {
                return false;
            }
            if (!EndNormal.Equals(other.EndNormal))
            {
                return false;
            }
            if (!LanesClosed.Equals(other.LanesClosed))
            {
                return false;
            }
            if (!Reason.Equals(other.Reason))
            {
                return false;
            }
            if (!Delay.Equals(other.Delay))
            {
                return false;
            }
            if (DelayedMinutes != other.DelayedMinutes)
            {
                return false;
            }
            if (!Direction.Equals(other.Direction))
            {
                return false;
            }
            if (!AreaEffected.Equals(other.AreaEffected))
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