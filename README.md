# PaymentGateway

The payment gateway, is an API based application that will allow a merchant to offer a way for their shoppers to pay for their product.

The two key functionnalites of this sample Gateway are :
1. A merchant should be able to process a payment through the payment gateway and receive either a successful or unsuccessful response 

2. A merchant should be able to retrieve the details of a previously made payment

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

### List of endpoints

### DataFlows




##Install

##Use
