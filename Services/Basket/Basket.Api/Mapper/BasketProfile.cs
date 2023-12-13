using AutoMapper;
using Basket.Api.Entities;
using EventBus.Messages.Events;

namespace Basket.Api.Mapper;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
    }
}
