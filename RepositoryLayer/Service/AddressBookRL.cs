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
        private readonly AddressBookDbContext _context;
        private readonly IMapper _mapper;

        public AddressBookRL(AddressBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactRequestModel>> GetContact()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return _mapper.Map<IEnumerable<ContactRequestModel>>(contacts);
        }

        public async Task<ContactRequestModel> GetContactById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return null;  
                // Return null if contact is not found
            }

            return _mapper.Map<ContactRequestModel>(contact);  
            // Convert ContactEntity to ContactRequestModel
        }

        public async Task<ContactEntity> AddContact(ContactRequestModel contact)
        {
            var newContact = _mapper.Map<ContactEntity>(contact);  
            // AutoMapper does the conversion

            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();

            return newContact;
        }

        public async Task<ContactRequestModel> UpdateContact(int id, ContactRequestModel contact)
        {
            var contactToUpdate = await _context.Contacts.FindAsync(id);

            if (contactToUpdate == null)
            {
                return null; 
            }

            // Use AutoMapper to map updated fields
            _mapper.Map(contact, contactToUpdate);

            _context.Contacts.Update(contactToUpdate);
            await _context.SaveChangesAsync();

            return _mapper.Map<ContactRequestModel>(contactToUpdate);
        }


        public async Task<ContactRequestModel> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return null;  
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return _mapper.Map<ContactRequestModel>(contact);
            // Convert ContactEntity → ContactRequestModel\
            //CRUD operations Done
        }
    }
}
