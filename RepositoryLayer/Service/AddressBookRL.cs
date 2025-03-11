using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Service
{
    public class AddressBookRL : IAddressBookRL
    {
        private readonly AddressBookDbContext _addressBookContext;

        public AddressBookRL(AddressBookDbContext addressBookContext)
        {
            _addressBookContext = addressBookContext;
        }

        public async Task<ContactEntity> AddContact(ContactRequestModel contact)
        {
            var contactEntity = new ContactEntity
            {
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email
            };

            await _addressBookContext.Contacts.AddAsync(contactEntity);
            await _addressBookContext.SaveChangesAsync();
            return contactEntity;
        }

        public async Task<IEnumerable<ContactRequestModel>> GetContact()
        {
            return await _addressBookContext.Contacts
                .Select(c => new ContactRequestModel
                {
                    Name = c.Name,
                    Phone = c.Phone,
                    Email = c.Email
                }).ToListAsync();
        }

        public async Task<ContactRequestModel> GetContactById(int id)
        {
            var contact = await _addressBookContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return null;
            }

            return new ContactRequestModel
            {
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email
            };
        }

        public async Task<ContactRequestModel> UpdateContact(int id, ContactRequestModel contact)
        {
            var contactEntity = await _addressBookContext.Contacts.FindAsync(id);
            if (contactEntity == null)
            {
                return null;
            }

            contactEntity.Name = contact.Name;
            contactEntity.Phone = contact.Phone;
            contactEntity.Email = contact.Email;

            _addressBookContext.Contacts.Update(contactEntity);
            await _addressBookContext.SaveChangesAsync();

            return contact;
        }

        public async Task<ContactRequestModel> DeleteContact(int id)
        {
            var contactEntity = await _addressBookContext.Contacts.FindAsync(id);
            if (contactEntity == null)
            {
                return null;
            }

            _addressBookContext.Contacts.Remove(contactEntity);
            await _addressBookContext.SaveChangesAsync();

            return new ContactRequestModel
            {
                Name = contactEntity.Name,
                Phone = contactEntity.Phone,
                Email = contactEntity.Email
            };
        }
    }
}
