using RotaMe.Sevices.Models.Owner.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RotaMe.Services.Contracts
{
    public interface IEventsService
    {
        Task<bool> Create(EventCreateServiceModel eventCreateServiceModel);
    }
}
