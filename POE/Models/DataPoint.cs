using System.Runtime.Serialization;
using System.Xml.Linq;

namespace POE.Models
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint()
        {
            
        }


        //Code attribution
        //Link:https://canvasjs.com/asp-net-mvc-charts/bar-chart/  
        public DataPoint(string label, decimal y)
        {
            this.Label = label;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<decimal> Y = null;

        public string ModCode { get; set; }
    }
}
