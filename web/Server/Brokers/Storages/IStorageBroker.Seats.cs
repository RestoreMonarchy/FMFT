﻿using FMFT.Web.Server.Models.Seats;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Seat> SelectSeatByIdAsync(int seatId);
        ValueTask<IEnumerable<Seat>> SelectAllSeatsAsync();
    }
}
