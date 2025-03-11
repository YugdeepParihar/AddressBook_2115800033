using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IAddressBookRL
    {
        Task<IEnumerable<ContactRequestModel>> GetContact();

        Task<ContactRequestModel> GetContactById(int id);
        Task<ContactEntity> AddContact(ContactRequestModel contact);
        Task<ContactRequestModel> UpdateContact(int id, ContactRequestModel contact);
        Task<ContactRequestModel> DeleteContact(int id);
    }
}
