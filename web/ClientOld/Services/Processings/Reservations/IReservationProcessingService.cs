﻿using FMFT.Web.Client.Models.Reservations;
using FMFT.Web.Client.Models.Reservations.Requests;

namespace FMFT.Web.Client.Services.Processings.Reservations
{
    public interface IReservationProcessingService
    {
        ValueTask<Reservation> CreateReservationAsync(CreateReservationRequest request);
        ValueTask<List<Reservation>> RetrieveAllReservationsAsync();
        ValueTask<Reservation> RetrieveReservationByIdAsync(int reservationId);
        ValueTask<List<Reservation>> RetrieveReservationsByUserIdAsync(int userId);
    }
}