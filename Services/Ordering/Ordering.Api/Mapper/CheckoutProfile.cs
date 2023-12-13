using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CreateOrder;

namespace Ordering.Api.Mapping;

public class CheckoutProfile : Profile
{
    public CheckoutProfile()
    {
        CreateMap<BasketCheckoutEvent, CreateOrderCommand>().ReverseMap();
    }
}
