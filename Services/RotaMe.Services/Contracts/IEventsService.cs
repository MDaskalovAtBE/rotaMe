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
        Task<bool> CreateNeed(EventNeedCreateServiceModel eventNeedCreateServiceModel);
        EventDetailsServiceModel GetEventDetails(int eventId);
        Task<bool> Edit(EventEditServiceModel eventEditServiceModel);
        Task<bool> NeedEdit(EventNeedEditServiceModel eventNeedEditServiceModel);

        Task<bool> Delete(int id);
        Task<bool> NeedDelete(int id);
    }
}
