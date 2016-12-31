using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;

namespace TrafficApp.Model.Problem
{
    public class TrafficProblem
    {
        private string reason;
        private static ICollection<TrafficProblem> problems;

        private static string filename = "problems.xml";

        public TrafficProblem(string reason)
        {
            Reason = reason;
        }

        public static TrafficProblem NewRoad(string reasonName)
        {
            TrafficProblem problem = new TrafficProblem(reasonName);

            Problems.Add(problem);
            Save();

            return problem;
        }

        public static TrafficProblem GetProblem(string problemName)
        {
            foreach (TrafficProblem problem in Problems)
            {
                if (problem.Reason.Equals(problemName))
                {
                    return problem;
                }
            }

            return NewRoad(problemName);
        }

        public new string ToString
        {
            get
            {
                return Reason;
            }
        }

        public string Reason
        {
            get
            {
                return reason;
            }

            set
            {
                if (value == null || value.Equals(""))
                {
                    value = "Unknown";
                }

                reason = value;
            }
        }

        public static ICollection<TrafficProblem> Problems
        {
            get
            {
                if (problems == null)
                {
                    problems = new List<TrafficProblem>();
                }
                return problems;
            }

            set
            {
                problems = value;
            }
        }

        public static void Load()
        {
            Load(filename);
        }

        private static void ProcessReader(XmlReader reader)
        {
            if (reader.Read())
            {
                switch (reader.Name)
                {
                    case "Reason":
                        if (reader.Read())
                        {
                            Problems.Add(new TrafficProblem(reader.Value.Trim()));
                        }
                        break;
                }
            }
        }

        public static void Load(string filename)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.Read())
                    {
                        // Only detect start elements.
                        if (reader.IsStartElement())
                        {
                            // Get element name and switch on it.
                            switch (reader.Name)
                            {
                                case "Problem":
                                    ProcessReader(reader);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {

            }
        }

        public static void Save()
        {
            //Save(filename);
        }

        public static async void Save(string filename)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            using (IRandomAccessStream writeStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                Stream s = writeStream.AsStreamForWrite();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Async = true;
                using (XmlWriter writer = XmlWriter.Create(s, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Problems");

                    foreach (TrafficProblem problem in Problems)
                    {
                        writer.WriteStartElement("Problem");

                        writer.WriteElementString("Reason", problem.Reason);

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    await writer.FlushAsync();
                }
            }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TrafficProblem other = obj as TrafficProblem;

            if (other == null)
            {
                return false;
            }

            if (!Reason.Equals(other.Reason))
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
