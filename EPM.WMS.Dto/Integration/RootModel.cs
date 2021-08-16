using System.Collections.Generic;

namespace EPM.WMS.Dto.Integration
{
    public class RootModel
    {
        public PrevailingModel Prevailing { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<ContactsModel> Contacts { get; set; }

    }
}
