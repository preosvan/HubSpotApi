
namespace HubSpotApi.Models.DTO
{
    public class ContactDTO
    {
        //Contact
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string LifeCycleStage { get; set; }
        //Company
        public string Company_Id { get; set; }        
        public string Company_Name { get; set; }
        public string Company_Website { get; set; }
        public string Company_City { get; set; }
        public string Company_State { get; set; }
        public string Company_Zip { get; set; }
        public string Company_NumberPhone { get; set; }
    }
}
