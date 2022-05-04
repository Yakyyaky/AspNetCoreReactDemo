# AspNetCoreReactDemo

This is a sample app that demonstrates the use of ASP.net core 3.1 WebAPI and hosting a ReactJS frontend using Typescript. The whole project can be packaged and deployed using Docker. The main part of the frontend serves as a playground to perform calls against an API and observe the various behaviour of the API. You can then authenticate against the server for a bearer token to perform further API calls for authenticated users only. If necessary, you can create your own account for testing as well. 

<img src="/ReadMeAssets/Demo Application Diagram.svg"/>

### Auhorization Filter
This project also demonstrates the use of Authorization filter. The easiest way to explain the request life cycle can be demonstrated using the image from the documentation. With Authorization Filters you can check for permission before it evers hits the Action filters, effectively short circuiting the request chain. This behaves much like adding [Authorize] against a controller or its method. The implemented Authorization filter can be seen decorated against DemoFilterController with [Authorize("DenyUnlessLoggedIn")]. However, a caveat to this filter interprets the content body, which essentially ties it down to a certain known implementation. The other observation is that for every API decorated with this attribute, it performs two deserializations, once for the filter and another for the controller.

<img src="https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters/_static/filter-pipeline-2.png?view=aspnetcore-3.1" />

Further details can be found here. [Filters in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1)

See example implementation here. [Claims-based authorization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-3.1)

### Postman Collection
You can test the various api using postman. An exported collection can be found [here](/ReadMeAssets/Demo.postman_collection.json)
Remember to setup {{serverURL}} to point at the correct host