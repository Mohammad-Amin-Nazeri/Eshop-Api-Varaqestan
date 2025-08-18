using Common.Application;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.RemoveItem
{
    public record RemoveOrderItemCommand(long userId , long itemId):IBaseCommand;
}
