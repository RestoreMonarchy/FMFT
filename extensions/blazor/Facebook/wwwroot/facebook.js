window.fbAsyncInit = function (appId) {
    window.FB.init({
        appId: appId,
        autoLogAppEvents: true,
        version: 'v15.0'
    });
};

window.fbLogin = function (dotnetHelper) {
    FB.login(function (response) {

        dotnetHelper.invokeMethodAsync('HandleFacebookLoginCallbackAsync', response);

    }, { scope: 'public_profile, email' });
}