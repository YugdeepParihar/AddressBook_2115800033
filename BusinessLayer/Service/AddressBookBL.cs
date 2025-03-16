using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace BusinessLayer.Service
{
    public class AddressBookBL
    {
        private readonly AddressBookRL _addressBookRL;

        public AddressBookBL() { }
        public AddressBookBL(AddressBookRL addressBookRL) 
        {
            _addressBookRL = addressBookRL;
        }

        public virtual async Task<List<AddressBookEntry>> GetAllContactsAsync()
        {
            return await _addressBookRL.GetAllContact();
        }

        public virtual async Task<AddressBookEntry?> GetContactByIdAsync(int id)
        {
            return await _addressBookRL.GetContactById(id);
        }

        public virtual async Task AddNewContactAsync(AddressBookEntry entry)
        {
            await _addressBookRL.AddNewContact(entry);
        }

        public virtual async Task UpdateContactAsync(AddressBookEntry entry)
        {
            await _addressBookRL.UpdateContact(entry);
        }

        public virtual async Task DeleteContactByIdAsync(int id)
        {
            await _addressBookRL.DeleteContactById(id);
        }


        public virtual async Task<List<AddressBookEntry>> GetAddressBookEntries(int id)
        {
            return await _addressBookRL.GetAddressBookEntries(id);
        }


    }
}
