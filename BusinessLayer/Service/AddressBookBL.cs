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
        private readonly IMapper _mapper;

        public AddressBookBL(IAddressBookRL addressBookRL, IMapper mapper)
        {
            _addressBookRL = addressBookRL;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ContactResponseModel<ContactRequestModel>>> GetContact()
        {
            var contacts = await _addressBookRL.GetContact();
            return contacts.Select(contact => new ContactResponseModel<ContactRequestModel>
            {
                Success = "true",
                Message = "Contact List",
                Data = contact
            });
        }

        public async Task<ContactResponseModel<ContactRequestModel>> GetContactById(int id)
        {
            var contact = await _addressBookRL.GetContactById(id);
            if (contact != null)
            {
                return new ContactResponseModel<ContactRequestModel>
                {
                    Success = "true",
                    Message = "Contact Found",
                    Data = contact
                };
            }
            return new ContactResponseModel<ContactRequestModel>
            {
                Success = "false",
                Message = "Contact Not Found",
                Data = null
            };
        }

        public async Task<ContactResponseModel<ContactRequestModel>> AddContact(ContactRequestModel contact)
        {

            // Pass the correctly named variable
            var addedContact = await _addressBookRL.AddContact(contact);

            // Ensure addedContact is not null before mapping
            if (addedContact == null)
            {
                return new ContactResponseModel<ContactRequestModel>
                {
                    Success = "false",
                    Message = "Failed to add contact",
                    Data = null
                };
            }

            // Return response with mapped ContactRequestModel
            return new ContactResponseModel<ContactRequestModel>
            {
                Success = "true",
                Message = "Contact Added",
                Data = _mapper.Map<ContactRequestModel>(addedContact)
            };
        }



        public async Task<ContactResponseModel<ContactRequestModel>> UpdateContact(int id, ContactRequestModel contact)
        {
            var updatedContact = await _addressBookRL.UpdateContact(id, contact);
            if (updatedContact != null)
            {
                return new ContactResponseModel<ContactRequestModel>
                {
                    Success = "true",
                    Message = "Contact Updated",
                    Data = _mapper.Map<ContactRequestModel>(updatedContact)
                };
            }
            return new ContactResponseModel<ContactRequestModel>
            {
                Success = "false",
                Message = "Contact Not Found",
                Data = null
            };
        }

        public async Task<ContactResponseModel<ContactRequestModel>> DeleteContact(int id)
        {
            var contact = await _addressBookRL.DeleteContact(id);
            if (contact != null)
            {
                return new ContactResponseModel<ContactRequestModel>
                {
                    Success = "true",
                    Message = "Contact Deleted",
                    Data = _mapper.Map<ContactRequestModel>(contact)
                };
            }
            return new ContactResponseModel<ContactRequestModel>
            {
                Success = "false",
                Message = "Contact Not Found",
                Data = null
            };
        }
    }
}
