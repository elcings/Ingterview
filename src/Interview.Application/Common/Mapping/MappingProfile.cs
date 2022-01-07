using AutoMapper;
using Interview.Application.CarError.Command;
using Interview.Application.Common.Models;
using Interview.Application.Distances.Command;
using Interview.Application.Fuel.Command;
using Interview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Mapping
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;
            CreateMap<FuelLevel, CreateFuelLevelCommand>().ReverseMap();
            CreateMap<Error, CreateErrorCommand>().ReverseMap();
            CreateMap<TodoItem, ToDoItemDTO>().ReverseMap();
            CreateMap<Distance, CreateDistanceCommand>().ForMember(x => x.Distance, opt => opt.MapFrom(src => src.distance)).ForMember(a=>a.Colour,opt=>opt.MapFrom(src=>src.Colour.ToString())).ReverseMap();
        }
       
    }


}
