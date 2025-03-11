using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class AddressBookBL : IAddressBookBL
    {
        private readonly IAddressBookRL _addressBookRL;

        public AddressBookBL(IAddressBookRL addressBookRL)
        {
            _addressBookRL = addressBookRL;
        }

        public async Task<IEnumerable<ContactResponseModel<ContactRequestModel>>> GetContact()
        {
            var contacts = await _addressBookRL.GetContact();
            return contacts.Select(contact => new ContactResponseModel<ContactRequestModel> { Data = contact });
        }

        public async Task<ContactResponseModel<ContactRequestModel>> GetContactById(int id)
        {
            var contact = await _addressBookRL.GetContactById(id);
            return new ContactResponseModel<ContactRequestModel> { Data = contact };
        }

        public async Task<ContactResponseModel<ContactRequestModel>> AddContact(ContactRequestModel contactRequestModel)
        {
            var contact = await _addressBookRL.AddContact(contactRequestModel);
            return new ContactResponseModel<ContactRequestModel> { Data = contactRequestModel };
        }

        public async Task<ContactResponseModel<ContactRequestModel>> UpdateContact(int id, ContactRequestModel contactRequestModel)
        {
            var contact = await _addressBookRL.UpdateContact(id, contactRequestModel);
            return new ContactResponseModel<ContactRequestModel> { Data = contactRequestModel };
        }

        public async Task<ContactResponseModel<ContactRequestModel>> DeleteContact(int id)
        {
            var contact = await _addressBookRL.DeleteContact(id);
            return new ContactResponseModel<ContactRequestModel> { Data = contact };
        }
    }
}
