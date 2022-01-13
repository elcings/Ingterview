using AutoMapper;
using Interview.Application.Common.Models;
using Interview.Application.OrderFutures.Command;
using Interview.Domain.AggregateModels.Orders;
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

           
            CreateMap<Order, OrderDetailViewModel>()
                .ForMember(dest=>dest.City,opt=>opt.MapFrom(src=>src.Address.City))
                .ForMember(dest=>dest.Country,opt=>opt.MapFrom(src=>src.Address.Country))
                .ForMember(dest=>dest.ZipCode,opt=>opt.MapFrom(src=>src.Address.ZipCode))
                .ForMember(dest=>dest.Street,opt=>opt.MapFrom(src=>src.Address.Street))
                .ForMember(dest=>dest.City,opt=>opt.MapFrom(src=>src.Address.City))
                .ForMember(dest=>dest.Status,opt=>opt.MapFrom(src=>src.OrderStatus.Name))
                .ForMember(dest=>dest.Date,opt=>opt.MapFrom(src=>src.Created))
                ;
            CreateMap<OrderItem, OrderItemModel>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.GetPictureUri()))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.GetOrderItemProductName()))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.GetUnits()))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.GetUnitPrice()));
                
        }
       
    }


}
