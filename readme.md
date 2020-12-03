# Google ReCAPTCHA V3 Usage In Asp.Net Core MVC 

![SS](https://github.com/huseyinsimsekk/Asp.NetCore-ReCAPTCHAv3/blob/master/RecaptchaV3/Screenshot_1.png)

## How To Use
You can use this repository two ways:
* You can clone or fork this repo and than you can change code what you want
* You can look over code and than implement your code.
Note that this is an example repository. 

## Explanation
* First, record to google recaptcha with your google account. Register your site there and get your secret and site key.
* Then edit GoogleRecaptcha configuration in `applicationsettings.json`. Also you can change Threshold value.  

```json
"GoogleRecaptcha": {
    "VefiyAPIAddress": "https://www.google.com/recaptcha/api/siteverify",
    "Sitekey": "YOUR-GOOGLE-SITEKEY-HERE",
    "Secretkey": "YOUR-GOOGLE-SECRETKEY-HERE"
  },
"RecaptchaThreshold": "0,5"
```

* Then you can add your UserService. 

I set 0.5 for this example. If get to you score is less than `0.5` from Google API, return error page. If score is greater that `0.5` and username-password pair is correct, you are login. Devto Post: [Post](https://dev.to/huseyinsimsek/usage-of-recaptcha-v3-in-asp-net-core-mvc-project-1gnh "Devto Post")

## Contribute
Please feel free to send PR or issue if you think there is a wrong, mistake, or enhancement.
