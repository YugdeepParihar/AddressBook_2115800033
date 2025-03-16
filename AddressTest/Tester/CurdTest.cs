using NUnit.Framework;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.Service;
using ModelLayer.DTO;
using RepositoryLayer.Entity;
using AddressTest;
using Microsoft.AspNetCore.Http;

namespace AddressTest.Tester
{
    [TestFixture]
    public class AddressBookControllerTests
    {
        private Mock<AddressBookBL> _mockService;
        private Mock<IMapper> _mockMapper;
        private AddressBookController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<AddressBookBL>();
            _mockMapper = new Mock<IMapper>();
            _controller = new AddressBookController(_mockService.Object, _mockMapper.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "User")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        // ✅ Test: Get all contacts (Admin)
        [Test]
        public async Task GetAllContacts_WhenUserIsAdmin_ReturnsOkResult()
        {
            var contacts = new List<AddressBookEntry> { new AddressBookEntry { Id = 1, Name = "John Doe", UserId = 1 } };
            _mockService.Setup(s => s.GetAllContactsAsync()).ReturnsAsync(contacts);
            var result = await _controller.GetAllContacts();
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

       
        // ✅ Test: Get contact by ID (Valid)
        [Test]
        public async Task GetContactById_WhenContactExists_ReturnsOkResult()
        {
            var contact = new AddressBookEntry { Id = 1, Name = "John Doe", UserId = 1 };
            _mockService.Setup(s => s.GetContactByIdAsync(1)).ReturnsAsync(contact);
            var result = await _controller.GetContactById(1);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

       

        // ✅ Test: Add contact (Valid Input)
        [Test]
        public async Task AddContact_ValidContact_ReturnsCreatedResult()
        {
            var dto = new AddressBookEntryDTO { Name = "Jane Doe" };
            var entity = new AddressBookEntry { Id = 2, Name = "Jane Doe", UserId = 1 };

            _mockMapper.Setup(m => m.Map<AddressBookEntry>(It.IsAny<AddressBookEntryDTO>())).Returns(entity);
            _mockService.Setup(s => s.AddNewContactAsync(It.IsAny<AddressBookEntry>())).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<AddressBookEntryDTO>(It.IsAny<AddressBookEntry>())).Returns(dto);

            var result = await _controller.AddContact(dto);
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        

       

        // ✅ Test: Delete contact (Valid)
        [Test]
        public async Task DeleteContact_WhenContactExists_ReturnsOkResult()
        {
            var contact = new AddressBookEntry { Id = 1, Name = "John Doe", UserId = 1 };
            _mockService.Setup(s => s.GetContactByIdAsync(1)).ReturnsAsync(contact);
            _mockService.Setup(s => s.DeleteContactByIdAsync(1)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteContact(1);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        // ✅ Test: Delete contact (Not Found)
        [Test]
        public async Task DeleteContact_WhenContactDoesNotExist_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetContactByIdAsync(99)).ReturnsAsync((AddressBookEntry)null);
            var result = await _controller.DeleteContact(99);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        
       
    }
}
