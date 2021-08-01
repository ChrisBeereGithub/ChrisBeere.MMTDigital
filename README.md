---------------
Getting Started
---------------

Clone the repo and open the solution in Visual Studio. Build it.

Hit Start and the Api will fire up and default to :

http://localhost:50609/swagger/index.html 

Here you can browse the Api endpoint definition using Swagger


To make requests to the Api use the endpoint :

http://localhost:50609/api/v1/orders

(See the Swagger definition for Json payload structures.)

---------
IMPORTANT
---------

The SSE_Test Sql Server cold starts frequently (possibly a pay as you go cloud service that hibernates to save money).

To mitigate this EnableRetryOnFailure has been enabled with the default execution policy. 

The result of this is that the first request made against the Api can often be slow as it performs retries whilst waiting for the server to spin up.

-------------------------------------------
Solution Structure and Project Descriptions
-------------------------------------------
ChrisBeere.MMTDigital - Solution 

ChrisBeere.MMTDigital.WebApi - This is the main project that defines the endpoints and configuration for the Web Api.

ChrisBeere.MMTDigital.WebApi.Data - Contains Sql Server related repositories, contexts and models.

ChrisBeere.MMTDigital.WebApi.Services - This is the resource communication layer.

ChrisBeere.MMTDigital.WebApi.ExternalApiServices - This is the external Api resource communication layer.

ChrisBeere.MMTDigital.WebApi.Tests - Unit tests. Also contains a document of integration tests with test evidence from Postman.

------------------------------------------------------------------------------------
List of outstanding tasks that might need to be considered before production release
------------------------------------------------------------------------------------

Full unit test coverage. 

Enable SSL and setup server certificate.

Addition of token authentication middleware and endpoint annotations to secure Api.

Addition of CorrelationId guid to request for logging.

Setup deployment pipeline and configure it to replace the app settings with production targets.


Chris Beere 2021
