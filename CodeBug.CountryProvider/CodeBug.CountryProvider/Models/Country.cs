using System.Runtime.Serialization;

namespace CodeBug.CountryProvider.Models
{  
    [DataContract]
    public class Country
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name= "alpha3Code")]
        public string Code { get; set; }
        [DataMember(Name = "capital")]
        public string Capital { get; set; }
        [DataMember(Name = "population")]
        public int? Population { get; set; }
        [DataMember(Name = "region")]
        public string Region { get; set; }
        [DataMember(Name = "subregion")]
        public string SubRegion { get; set; }
        [DataMember(Name = "area")]
        public decimal? Area { get; set; }
    }
}
