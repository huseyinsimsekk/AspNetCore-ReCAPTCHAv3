# Example of Google ReCAPTCHA V3 Usage In Asp.Net Core MVC 

* First, record to google recaptcha with your google account. Register your site there and get your secret and site key.
* Then edit clientside integration and serverside integration.

If get to you score is less than `0.5` from Google API, return error page. If score is greater that `0.5` and username-password pair is correct, you are login.  

> I had been upgraded to Asp.Net Core MVC 3.1. In this version, you just change `Sitekey` and `Secretkey` in `appsettings.json`.

 > Devto Post: [Post](https://dev.to/huseyinsimsek/usage-of-recaptcha-v3-in-asp-net-core-mvc-project-1gnh "Devto Post")
