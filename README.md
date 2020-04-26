# SamlResponseFactory
Generates SAML Response with signed Assertion

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
