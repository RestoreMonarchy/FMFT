window.googleAsyncInit = function (clientId, dotnetHelper) {
    google.accounts.id.initialize({
        client_id: clientId,
        callback: (response) => handleCredentialResponse(response, dotnetHelper)
    });
};

function handleCredentialResponse(response, dotnetHelper) {
    dotnetHelper.invokeMethodAsync('HandleGoogleLoginCallbackAsync', response);
}

window.googleLogin = function () {
    delete_cookie("g_state");
    google.accounts.id.prompt();
}

function delete_cookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}