    using System.Collections.Generic;

namespace EPM.WMS.Dto.Integration
{
    public class ContactsModel
    {
        public ContactModel Contact { get; set; }
        public List<PhoneModel> Phones { get; set; }
        public List<EmailModel> Emails { get; set; }
        public List<AddressModel> Addresses { get; set; }
    }
}
