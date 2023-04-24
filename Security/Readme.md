### How to use
to get started on Metika.Security you need to do the following steps :

1. Create appsetting section for Security Options :
```json
  "SecurityOptions": {
    "PrivateKeyFilePath": "keys/private.xml",
    "PublicKeyFilePath": "keys/public.xml"
  }
```
this location is used to save public key and private key inside hard drive.
2. you need to add project dependency and this line of code on top of program.cs file :
```c#
    using Metika.Security.Extensions;
```
3. then you need to add dependencies by calling :
```c#
    builder.Services.AddRsaJwt(builder.Configuration);
```
4. also you need to make sure that pipeline is configured
```c#
    app.UseAuthentication();
    app.UseAuthorization();
```
5. you can also add security middleware for jwt by adding this code :
```c#
    app.UseSecurityFeatures();
```
this step is optional and it adds some headers to response