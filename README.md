### Get Started
EslamiSepehr.reCAPTCHA.Core can be installed using the Nuget package manager or the `dotnet` CLI.

#### Nuget:
```
Install-Package EslamiSepehr.reCAPTCHA.Core
```

#### CLI:
```
dotnet add package EslamiSepehr.reCAPTCHA.Core
```

### Usage

### appsettings.json:

```
"ReCAPTCHA": {
    "SecretKey": "SecretKey Value",
    "SiteKey": "SiteKey Value"
}
```

#### Startup.cs

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddEsReCAPTCHA(options =>
    {
        options.SecretKey = Configuration["ReCAPTCHA:SecretKey"];;
        options.SiteKey = Configuration["ReCAPTCHA:SiteKey"];
    });
}
```

#### Form.cshtml:

```
@using Microsoft.Extensions.Options
@using EslamiSepehr.reCAPTCHA.Core.Options
@inject IOptions<reCAPTCHAOptions> reCAPTCHAOptions

<form method="post">
    <div class="g-recaptcha" data-callback="" data-sitekey="@reCAPTCHAOptions?.Value.SiteKey"></div>
    <input type="submit" value="Submit" />
</form>

<script src='https://www.google.com/recaptcha/api.js'></script>
```

#### Form.cshtml.cs:

```
private readonly IReCaptchaService reCaptchaService;

public FormModel(IReCaptchaService reCaptchaService)
{
    this.reCaptchaService = reCaptchaService;
}

public async Task<IActionResult> OnPostAsync(string returnUrl = null)
{
    if (!await reCaptchaService.IsValidAsync(Request))
        return RedirectToPage();

    return Page();
}
```