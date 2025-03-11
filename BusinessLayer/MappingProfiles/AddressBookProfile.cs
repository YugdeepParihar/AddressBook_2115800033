using AutoMapper;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MappingProfiles
{
    public class AddressBookProfile : Profile
    {
        public AddressBookProfile()
        {
            CreateMap<ContactRequestModel, ContactEntity>().ReverseMap();

            CreateMap<ContactEntity, ContactResponseModel<ContactRequestModel>>();
        }
    }
}
