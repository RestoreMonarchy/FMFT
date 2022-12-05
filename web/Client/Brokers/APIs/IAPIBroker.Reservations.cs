﻿using FMFT.Web.Client.Models;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse<Reservation>> GetReservationByIdAsync(string reservationId);
        ValueTask<APIResponse<List<Reservation>>> GetAllReservationsAsync();
        ValueTask<APIResponse<Reservation>> CreateReservationAsync(CreateReservationRequest request);
        ValueTask<APIResponse<Reservation>> CancelReservationAsync(CancelReservationRequest request);
        ValueTask<APIResponse<List<Reservation>>> GetUserReservationsAsync(int userId);
        ValueTask<APIResponse<QRCodeImage>> GetReservationQRCodeImageByIdAsync(string reservationId);
        ValueTask<APIResponse<QRCodeImage>> GetReservationQRCodeImageBySeatIdAsync(string reservationId, int seatId);
    }
}
