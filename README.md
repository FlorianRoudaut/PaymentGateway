# PaymentGateway

The payment gateway, is an API based application that will allow a merchant to offer a way for their shoppers to pay for their product.

The two key functionnalites of this sample Gateway are :
1. A merchant should be able to process a payment through the payment gateway and receive either a successful or unsuccessful response 

2. A merchant should be able to retrieve the details of a previously made payment

The system can be tried here http://ec2-35-180-201-10.eu-west-3.compute.amazonaws.com/GatewayClient/html/login.html with login Diseny and password 1234

## Assumptions

The first assumption is that the gateway could link to multiple Acquiring Banks, in this example we will focus on a fictionnal bank called "Piggy Bank"

The second assumption is that each user or merchant, have signed a contract with an Acquiring Bank. So we will store a mapping between the merchant and the acquiring bank. https://securionpay.com/blog/acquirer/


## Design and Architecture

The Gateway should expose an API, a REST API is a common and highly compatible way to do it. 

The key technical requirements are the availability and high-message throughput. Indeed, the users cannot wait to process a payment and since e-commerce is internationnaly growing the system should handle millions of message. 
So it should be highly scallable. 
A good way to handle these requirements is to adpot a containerized microservices approach. Multiple micorservices with the same API can be run with redundancy, so if one fails others are still running wich ensures the high availability. Besides, when microservices are containerized and managed by an orchestrator, if a service is becoming a performance bottleneck, this functionnality can be duplicated easily by launching new containers with this service.
Microservices have other advantages, like they can be easily maintained and it is easy to make evolutions.

.Net Core is offering an easy to implement this architecture, it is explained on microsoft ebook https://dotnet.microsoft.com/download/e-book/microservices-architecture/pdf
An example of implementation can be found here https://github.com/dotnet-architecture/eShopOnContainers
.Net Core can be ran on any platform and can be put on GitHub

To exchange between services, asynchronously to lesser the dependencies, RabbitMQ mesasing bus will be used. 
MongoDb will be used for data storage because it is a high availability and performant DBMS.

The overall services architecture can be found in the folder specs/Backend Architecture.png

### Process a payment
The payment gateway provide merchants with a way to process a payment. To do this, the merchant should be able to submit a request to the payment gateway. A payment request should include appropriate fields such as the card number, expiry month/date, amount, currency, and cvv.
The PiggyBank behavior is simulated in the PiggyBank.Api, for simplicity the API is just a dll import not a web API.

*HTTP API*
A post API is available at the endpoint POST http://host/api/process with a json file like {"merchantname":"Disney","cardnumber":"1234","Cvv":"123","expirymonth":12,"expiryyear":31,"amount":42,"currency":"eur","cardholdername":"Santa Claus"}

*DataFlow*
1) The merchant sends a query to the API Gateway to post a payment
2) The API Gateway sends a ProccessPayment message on the MQ bus
3) The Processing service catches the payment request message, if there is an Acquirer mapped to the merchant it sends a message to the service reponsible for this Acquirer on the bus. Here we handle on messages for "Piggy Bank"
4) The PiggyConnector Service catches the message, connects to the acquiring bank API to validate the payment. Then it sends a Payment Processed.
5) The PaymentHistory Service catches the PaymentProcessed event saves an entry in db
6) The API gateway cache saves a summary of this Payment

An image can be found in specs/Process Payment DataFlow.png

### Retrieving a payment’s details
Past payments are saved in the API Gateway cache, to optimize the time to retrieve data. 

*HTTP API*
A get API is available at the endpoint GET http://host/api/history it returns a json file like [
{"id": "5e3c6936-403a-4861-aadc-08ec5fb25f04","merchantId": "f36411c9-a6c4-4873-a0da-52e20238c0f7","cardHolderName": "M. Santa Claus","amount": 314,"currency": "gbp","createdAt": "2019-12-19T12:51:41.725Z","processedAt": "2019-12-19T12:51:42.565Z","processed": true,"acquirerStatus": "Authorised","errorCode": null},{"id": "7b447579-2a84-4d8b-a480-d04ad7022592","merchantId": "f36411c9-a6c4-4873-a0da-52e20238c0f7","cardHolderName": "M. Reindeer", "amount": 42,"currency": "usd","createdAt": "2019-12-19T12:52:47.942Z","processedAt": "2019-12-19T12:52:47.984Z","processed": true,"acquirerStatus": "Authorised","errorCode": null},]

*DataFlow*
1) The merchant sends a query to the API Gateway to get the history of all his payments
2) The API Gateway retrieves it from its cache and returns it in a Json file

##Improvements
Besides the points discussed below the following improvements could be made :
- Use Elastic search for logs (ELK stack)
- Improve the Security wtih secrets management and encryption
- Add databse consistency Consistency checks, between the Users and merchants db and between the API Gateway db and the history service db
- Use other types of database management systems. For instance, Redis could be used for API Gateway cache. Redis is storage system that is very efficient for cached data. Since there is a microservice architecture, each service can have a different type of db. In this solution, MongoDB was used everywhere for simplicity.

##Application Logging
For monoliths applications, the log is usually in a log file, but since this solution is distributed, it is better for system monitoring to have all logs in one place. The approach implemented here is to have a service that will receive all the logs and store them in a db.

Log cases : https://docs.microsoft.com/fr-fr/azure/architecture/microservices/logging-monitoring
Log Service : https://livebook.manning.com/book/microservices-in-net-core/chapter-9/16

A potential improvement could be to reduce the priority of logs messages on the bus so that the log messages do not jam the queue. https://www.rabbitmq.com/priority.html

##Application metrics
Each service has a simple GET endpoint, this allow to add a mechanism that will poll the services to see if they are still alive. 

Improvements could be to have metrics for each service, like the number of pending messages, the number of messages processed over the last x minutes, the number of errors over the last x minutes, the size of the database.

Since RabbitMQ is the link between each microservice, it is key to monitor its performances. A first approach can be to look at the RabbitMQ web console here http://localhost:15672/#/

##Containerization
Currently this solution is self hosted. Since the solution is written in .Net Core it can be conainerized, for instance in Docker. All the services can be ran together using the Docker Compose functionnality. Moving this kind of solution to Docker is explained here https://dotnet.microsoft.com/download/e-book/microservices-architecture/pdf

##Authentication
Authentication has been implemented in this solution using the JWT framework. https://jwt.io/

First of all, for each merchant needs to create a user. They can create a user using endpoint POST http://gateway.authentication/createuser with a json file like this {"login": "Disney", "name": "Disney","password": "1234","acquiringbank" : "PiggyBank"}
The user is saved in the Users DB with the password hashed

*Authentication Flow*
1) The client send a API query to the endpoint POST http://gateway.authentication/login with a json {"login":"Disney","password":"1234"}
2) The service hashes the password sent and checks if it is the same than the hash saved in the users db. If it is, the service sends back a Jason Web Token (JWT)
3) The client calls the API Gateway and puts the JWT in the header of any the HTTP query. If the JWT is valid, the API Gateway process the qery, if not it returns an error HTTP 401
4) The rest of the business workflow unfolds.

##API Client
A Proof Of Concept API Client has been implemented in the folder ClientJs. The client is done using html and JavaScript. It queries the Gateway API using AJAX queries. The goal of the client is just to prove that the backend system is working and can be queried using other technologies than .Net Core. However no UI design/CSS has been done yet.

The client is based on three pages : a login page, a page to see the past payments of the user and a page to process a new payment.

It can be launched by opening the file ClientJs/hmtl/login.html

##Deployment
The whole system has been deployed on the server ec2-35-180-201-10.eu-west-3.compute.amazonaws.com 
MongoDb and RabbitMQ are installed on the server. One instance of each service is launched in a self-hosted mode. The web client is hosted on IIS.
Since this is a free tier server from Amazon, it has only one core and 1Go of RAM, so it can be a bit slow when hosting 6 microservices and rabbitmq and mongodb. Each component can run on the different server or could be hosted on a Amazon ECS https://aws.amazon.com/getting-started/tutorials/deploy-docker-containers/

##Build Script/CI
A build script is located in scripts/build.bat . This script builds the whole solution. 
There is also a script called run-tests.bat that runs Automated tests from the Gateway.Api.Tests project, based on NUnit or other tests to come.
Based on that, a CI Tool like TravisCI https://travis-ci.com/ or Jenkins https://jenkins.io/ or GitLab  https://about.gitlab.com/ or any other can be plugged on github hooks and rebuild, check the automated tests and deploy the soltuion.

##Performance Testing
No performance testing has been done yet. The key performance metric is the throughput. To test this metric, a client that sends x messages per seconds can be developped. 

##Install and Use
###Prerequisites
.Net core must be installed on the machine, along with MongoDB and RabbitMQ.

###Instructions
Clone the repository
Build the solution using Visual Studio or the batch script scripts/build.bat
Run the solution using Visual Studio or the launch script scripts/launch-services.bat. This will launch the 6 services in a self hosted mode. 
Then you can launch the client from CLientJS (do not forget to update he js files with the correct hostname)


All the queries can be imported in postman using the file specs/postman api queries/PaymentGateway.postman_collection.json . It can be used to test the system
