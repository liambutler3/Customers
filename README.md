## About
This is a customer search application. It is built using .net core 3.1 and react. With API endpoints to return related customer data in json format. Uses react as frontend to display the relevant results.

## Prerequisites
* Install [dotnet core](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* Install [node & npm](https://nodejs.org/en/download/)

## Original Data Source
``` JSON
[
{
"first_name": "David",
"last_name": "Platt",
"phone": "01913478234",
"email": "david.platt@corrie.co.uk"
},
{
"first_name": "Jason",
"last_name": "Grimshaw",
"phone": "01913478123",
"email": "jason.grimshaw@corrie.co.uk"
},
{
"first_name": "Ken",
"last_name": "Barlow",
"phone": "019134784929",
"email": "ken.barlow@corrie.co.uk"
},
{
"first_name": "Rita",
"last_name": "Sullivan",
"phone": "01913478555",
"email": "rita.sullivan@corrie.co.uk"
},
{
"first_name": "Steve",
"last_name": "McDonald",
"phone": "01913478555",
"email": "steve.mcdonald@corrie.co.uk"
}
]
```

## On-load
I've manipulated the above records to have a unique Id that's the index of the record in the array. This is so we can uniquely identify the record that we want to edit or delete them just by the Id.

### For example
```Json
[{"Id":0,"first_name":"David","last_name":"Platt","phone":"01913478234","email":"david.platt@corrie.co.uk"},{"Id":1,"first_name":"Jason","last_name":"Grimshaw","phone":"01913478123","email":"jason.grimshaw@corrie.co.uk"}]
```


## API Endpoints
### GET - Get Customers - /api/customers/GetCustomers
Returns a list of customers
#### Example Request
[https://localhost:44395/api/Customers/GetCustomers](https://localhost:44395/api/Customers/GetCustomers)
#### Example response
``` JSON
[
    {
        "id": 0,
        "first_name": "David",
        "last_name": "Platt",
        "phone": "01913478234",
        "email": "david.platt@corrie.co.uk"
    },
    {
        "id": 1,
        "first_name": "Jason",
        "last_name": "Grimshaw",
        "phone": "01913478123",
        "email": "jason.grimshaw@corrie.co.uk"
    },
    {
        "id": 2,
        "first_name": "Ken",
        "last_name": "Barlow",
        "phone": "019134784929",
        "email": "ken.barlow@corrie.co.uk"
    },
    {
        "id": 3,
        "first_name": "Rita",
        "last_name": "Sullivan",
        "phone": "01913478555",
        "email": "rita.sullivan@corrie.co.uk"
    },
    {
        "id": 4,
        "first_name": "Steve",
        "last_name": "McDonald",
        "phone": "01913478555",
        "email": "steve.mcdonald@corrie.co.uk"
    }
]
```


### GET - Get Customer - /api/customers/GetCustomer?id={id}
Returns a customer in JSON form. The id is the index of the customers array.
#### Example Request
[https://localhost:44395/api/Customers/GetCustomer?id=3](https://localhost:44395/api/Customers/GetCustomer?id=3)
#### Example response
``` JSON
{
    "id": 3,
    "first_name": "Rita",
    "last_name": "Sullivan",
    "phone": "01913478555",
    "email": "rita.sullivan"
}
```


### POST - Edit Customer - /api/customers/EditCustomer
Edits a customer record. Need to supply JSON body with the customer data.
#### Example Request
[https://localhost:44395/api/Customers/EditCustomer](https://localhost:44395/api/Customers/EditCustomer)
##### Body
```Json
{"Id":2,"first_name":"Changed","last_name":"By","phone":"Api","email":"ken.barlow@corrie.co.uk"}
```
##### Content-Type
application/json

### POST - Add Customer - /api/customers/AddCustomer
Adds a new customer record. Need to supply JSON body with the customer data. No need to supply a id.
#### Example Request
[https://localhost:44395/api/Customers/AddCustomer](https://localhost:44395/api/Customers/AddCustomer)
##### Body
```Json
{"first_name":"New","last_name":"Customer","phone":"12345","email":"new@test.co.uk"}
```
##### Content-Type
application/json


### POST - Delete Customer - /api/customers/DeleteCustomer/{id}
Delete a customer record by id.
#### Example Request
[https://localhost:44395/api/Customers/DeleteCustomer?id=1](https://localhost:44395/api/Customers/DeleteCustomer?id=1)

### POST - Delete Customer - /api/customers/DeleteSuppliedCustomer
Delete customer by refering to supplied JSON of the customer data.
#### Example Request
[https://localhost:44395/api/Customers/DeleteSuppliedCustomer](https://localhost:44395/api/Customers/DeleteSuppliedCustomer)
##### Body
```Json
{"Id":2,"first_name":"Ken","last_name":"Barlow","phone":"019134784929","email":"ken.barlow@corrie.co.uk"}
```
##### Content-Type
application/json



## Improvements
* Store the customer data in a database* Make the website more responsive* More search options* Allow inline editing of the customer records. So then don't need the CustomerEdit component and js.* Reload the table dynamically when records change in anyway.* Allow adding of new customers through the front-end too* More logging* More exception handling and try, catches* Add editing a record validation(s) for example email