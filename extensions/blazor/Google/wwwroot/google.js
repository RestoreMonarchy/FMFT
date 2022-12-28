window.googleAsyncInit = function (clientId, dotnetHelper) {
    console.log("google client id: " + clientId);
    google.accounts.id.initialize({
        client_id: clientId,
        callback: (response) => handleCredentialResponse(response, dotnetHelper)
    });
};

function handleCredentialResponse(response, dotnetHelper) {
    console.log(response);
    dotnetHelper.invokeMethodAsync('HandleGoogleLoginCallbackAsync', response);
}

window.googleLogin = function () {
    google.accounts.id.prompt();
}