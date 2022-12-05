## Error Codes

### Web API
Web API is a `FMFT.Web.Server` project.  
The following are the error codes returned by controllers with a content type: `application/problem+json`

Error Code | Message 
--- | ---
ERR001 | Account not authenticated
ERR002 | Account not authorized
ERR003 | External login not found
ERR004 | Register with login validation problem
ERR005 | Register with password validation problem
ERR006 | User with this email already exists
ERR007 | User with this external login already exists
ERR008 | User not found
ERR009 | Invalid credentials
ERR010 | User already has this role set
ERR011 | User already has this culture set
ERR012 | Add Show validation problem
ERR013 | Update Show validation problem
ERR014 | Show not found
ERR015 | Show auditorium does not exist
ERR016 | Seat not found
ERR017 | Reservation not found
ERR018 | Seat is already reserved
ERR019 | User already created a reservation
ERR020 | Auditorium not found
ERR021 | This user has already confirmed email
ERR022 | Secret key and user id do not match 
ERR023 | This user does not use password authentication
ERR024 | Change user password validation problem
ERR025 | Reset password validation problem
ERR026 | This reset password request has already been used
ERR027 | This reset password request has expired
ERR028 | You have reached the limit of reset password requests. Try again later
ERR029 | Reset password request not found
ERR030 | Media not found
ERR031 | The media file is too large
ERR032 | The media file is required
ERR033 | The seats for the reservation were not provided
ERR034 | Reservation seat not found