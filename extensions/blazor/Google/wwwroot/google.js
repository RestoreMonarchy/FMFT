window.googleAsyncInit = function (clientId, dotnetHelper) {
    console.log("google client id: " + clientId);
    google.accounts.id.initialize({
        client_id: clientId,
        callback: (response) => handleCredentialResponse(response, dotnetHelper)
    });

    console.log(google.accounts.id);

    //google.accounts.id.renderButton(document.getElementById("googleButton"),
    //    {
    //        type: 'standard',
    //        theme: 'filled_blue',
    //        width: '100%'
    //    });        

};

function handleCredentialResponse(response, dotnetHelper) {
    console.log(response);
    dotnetHelper.invokeMethodAsync('HandleGoogleLoginCallbackAsync', response);
}

window.googleLogin = function () {
    delete_cookie("g_state");
    google.accounts.id.prompt();
    
}

function delete_cookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}