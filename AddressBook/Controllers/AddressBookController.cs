using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace AddressBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookBL _addressBookBL;

        public AddressBookController(IAddressBookBL addressBookBL)
        {
            _addressBookBL = addressBookBL;
        }

        /// <summary>
        /// This method is used to get all contacts from the database
        /// </summary>
        /// <returns>return json data with all contacts</returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactResponseModel<ContactRequestModel>>>> GetContacts()
        {
            return Ok(await _addressBookBL.GetContact());
        }

        /// <summary>
        /// This method is used to get contact by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns json data with contact found with id in database</returns>
        [HttpGet("{id}")]

        public async Task<ActionResult<ContactResponseModel<ContactRequestModel>>> GetContactById(int id)
        {
            var contact = await _addressBookBL.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        /// <summary>
        /// This method is used to add contact to the database
        /// </summary>
        /// <param name="contactRequestModel"></param>
        /// <returns>returns the added contact in form of json data</returns>
        [HttpPost]

        public async Task<ActionResult<ContactResponseModel<ContactRequestModel>>> AddContact(ContactRequestModel contactRequestModel)
        {
            var contact = await _addressBookBL.AddContact(contactRequestModel);
            return CreatedAtAction(nameof(AddContact), contact);
        }


        /// <summary>
        /// This method is used to update contact in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contactRequestModel"></param>
        /// <returns>returns the updated data in json format based on id</returns>
        [HttpPut("{id}")]

        public async Task<ActionResult<ContactResponseModel<ContactRequestModel>>> UpdateContact(int id, ContactRequestModel contactRequestModel)
        {
            var contact = await _addressBookBL.UpdateContact(id, contactRequestModel);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        /// <summary>
        /// This method is used to delete contact from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns json data of deleted contact based on id</returns>
        [HttpDelete("{id}")]

        public async Task<ActionResult<ContactResponseModel<ContactRequestModel>>> DeleteContact(int id)
        {
            var contact = await _addressBookBL.DeleteContact(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

    }
}
