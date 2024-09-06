Item Catalog Service - this microservice purpose is to give the client the ability to create/read/update/delete items
Order Service - functionality for making orders
When a change happens on the item catalog service, the changes then are sent through the service bus(MassTransit with RabbitMQ) to the Order service, where the items table is synced with the one on the item catalog service.
When an order happens a message is sent to the item catalog service to verify that the quantity is the right amount and then another message is sent back to the order service that contains info if the order is verified or not. In that way 
clients can create orders even if the item catalog service or the service bus is down. It will just be verified when all is up and running.
