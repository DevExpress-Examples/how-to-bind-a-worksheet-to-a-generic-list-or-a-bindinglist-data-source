using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataBindingToListExample {

    public class MyFish {
        [DisplayName("ID")]
        public int ID { get; set; }
        [DisplayName("Fish Category")]
        public string Category { get; set; }
        [DisplayName("Fish Common Name")]
        public string CommonName { get; set; }
        [DisplayName("Fish Species Name")]
        public string SpeciesName { get; set; }
        [DisplayName("Hyperlink")]
        public string Hyperlink { get; set; }
        [DisplayName("TimeStamp")]
        public DateTime TimeStamp { get; set; }
        public MyFish(int id, string category, string commonName, string speciesName, string hyperlink, DateTime timeStamp) {
            this.ID = id;
            this.Category = category;
            this.CommonName = commonName;
            this.SpeciesName = speciesName;
            this.Hyperlink = hyperlink;
            this.TimeStamp = timeStamp;
        }
    }
    public class Fish {
        [DisplayName("ID")]
        public int ID { get; set; }
        [DisplayName("Category")]
        public string Category { get; set; }
        [DisplayName("Common Name")]
        public string CommonName { get; set; }
        [DisplayName("Notes")]
        public string Notes { get; set; }
        [DisplayName("SpeciesName")]
        public string SpeciesName
        {
            get
            {
                return this.ScientificClassification.Species;
            }
            set
            {
                this.ScientificClassification.Species = value;
            }
        }
        [DisplayName("Hyperlink")]
        public string Hyperlink
        {
            get
            {
                return this.ScientificClassification.Hyperlink;
            }
            set
            {
                this.ScientificClassification.Hyperlink = value;
            }
        }
        public ScientificClassification ScientificClassification { get; set; }
    }
    public class ScientificClassification {
        [XmlElement("Reference")]
        public string Hyperlink { get; set; }
        public string Kingdom { get; set; }
        public string Phylum { get; set; }
        [XmlElement("Class")]
        [DisplayName("Class")]
        public string _Class { get; set; }
        public string Order { get; set; }
        public string Family { get; set; }
        public string Genus { get; set; }
        public string Species { get; set; }
    }
    public static class MyFishesSource {
        static List<MyFish> data;
        public static System.Collections.ObjectModel.ReadOnlyCollection<MyFish> Data
        {
            get
            {
                Random rnd1 = new Random();
                if (data == null) {
                    List<Fish> fLIst = GetDataSource();
                    data = new List<MyFish>();
                    foreach (Fish f in fLIst) {
                        data.Add(new MyFish(f.ID, f.Category, f.CommonName, f.SpeciesName, f.Hyperlink, DateTime.Now.AddHours(-24 * rnd1.NextDouble())));
                    }
                }
                return data.AsReadOnly();
            }
        }
        static List<Fish> GetDataSource() {
            return DataSourceHelper.GetDataSouresFromXml<Fish>("fishes.xml", "Fishes");
        }
    }
}
