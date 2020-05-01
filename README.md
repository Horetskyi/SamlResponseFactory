# SamlResponseFactory
Generates SAML Response with signed Assertion

## Nuget
https://www.nuget.org/packages/SamlResponseFactory

## Usage example
```
SamlResponseFactoryService.CreateSamlResponse(new SamlResponseFactoryArgs
{
    Audience = new Audience("http://sp.example.com/demo1/metadata.php"),
    Issuer = new Issuer("http://idp.example.com/metadata.php"),
    Recipient = new Recipient("http://sp.example.com/demo1/index.php?acs"),
    Certificate = new X509Certificate2("zohoSso.pfx"),
    TimeToBeExpired = TimeSpan.FromMinutes(30),
    RequestId = new RequestId("_c635b3bdaa8f4b529368b6dabe01d5d91539326"),
    Email = new Email("userEmail@gmail.com"),
});
```
More info here: https://medium.com/@horetskiy.d.s.7/7bb1b29e08a1

## 
This library depends on https://github.com/i8beef/SAML2. I create some workarounds in this library on top of the SAML2 library. This library goal is to force the SAML2 library to work for a single need, a single method - `SamlResponseFactoryService.CreateSamlResponse(Args)`

## Remark
This solution is far from a clean one. But it is coping with the task good enough.
