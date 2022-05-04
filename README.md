# AspNetCoreReactDemo

This is a sample app that demostrate the use of ASP.net core 3.1 WebAPI and hosting a ReactJS frontend using Typescript. The whole project can be packaged and deployed using Docker. The main part of the frontend serves as a playground to perform calls against an api observe the various behaviour of the API. You can then authenticated against the server for a bearer token to perform further API calls for authenticated users only. If necessary, you can create your own account for testing as well.

<img src="/ReadMeAssets/Demo Application Diagram.svg"/>

### Auhorization Filter
This projects also demostrate the use of Authorization filter. The easiest way to explain the request life cycle can be demostrated using the image from the documentation. With Authorization Filters you can check for permission before it evers hits the Action filters, effectively short circuiting the request chain. This behaves much like adding [Authorize] against a controller or its method. The implemented Authorization filter can be seen decorated against DemoFilterController with [Authorize("DenyUnlessLoggedIn")]. However, a caveat to this filter intreprets content body, which essential ties it down to a certain known implementation. The other observation is that for every API decorated with this attribute, tt perform two deserialization, once for the filter and another for the controller.

<img src="https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters/_static/filter-pipeline-2.png?view=aspnetcore-3.1" />

Further details can be found here.
https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1

See example implementation here.
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-6.0
