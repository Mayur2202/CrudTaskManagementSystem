using Newtonsoft.Json;

namespace CrudTaskManagementSystem.Entity
{
    public class MandatoryField
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UID { get; set; }

        [JsonProperty(PropertyName = "Developers", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }

        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdByName", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedByName { get; set; }

        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedByName", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedByName { get; set; }

        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { set; get; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "archieved", NullValueHandling = NullValueHandling.Ignore)]
        public bool Archieved { get; set; }

    }
    public class DevelopersEntity:MandatoryField

    {
       [JsonProperty(PropertyName ="developerId",NullValueHandling =NullValueHandling.Ignore)]
       public string DeveloperId { get; set; }

       [JsonProperty(PropertyName = "developerName", NullValueHandling = NullValueHandling.Ignore)]
       public string DeveloperName { get; set; }

       [JsonProperty(PropertyName = "developerRoll", NullValueHandling = NullValueHandling.Ignore)]
       public string DeveloperRoll { get; set; }
    }
}
