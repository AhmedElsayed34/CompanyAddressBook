using AutoMapper;
using CompanyAddressBook.Core.Entities;
using CompanyAddressBook.Web.Models;

namespace CompanyAddressBook.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyViewModel>().ReverseMap();
            CreateMap<CompanyContact, ContactViewModel>().ReverseMap();
        }
    }
}