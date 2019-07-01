namespace Helsi.DataAccess.Models
{
    public partial class AdditionalContact
    {
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public int PatientId { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
