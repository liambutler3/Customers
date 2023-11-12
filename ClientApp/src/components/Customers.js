import React, { useState, useEffect, Link } from 'react';

export function Customers()
{
  const [loading, setLoading] = useState(true);
  const [customers, setCustomers] = useState([]);
  const [onLoadcustomers, setOnLoadCustomers] = useState([]);

  // on load, get all customers
  useEffect(async () => {
    const response = await fetch('/api/Customers/GetCustomers');
    const data = await response.json();

    // on load customers
    setOnLoadCustomers(data);

    setCustomers(data)
    setLoading(false);
  }, []);

  function onSearchChange(e)
  {
      setLoading(true);

      // set customers to the search results
      var searchCustomers = customers.filter(customer => customer.first_name.toLowerCase().includes(e.target.value.toLowerCase()));

      console.log(e.target.value);

      if (e.target.value == "" || e.target.value == " ") {
          setLoading(false);
          // set customers to the original list
          setCustomers(onLoadcustomers);
      }
      else {
          setLoading(false);
          setCustomers(searchCustomers);
      }

      console.log(searchCustomers);
  }

  function onDelete(e) {

      console.log(e.target.value);

      var id = e.target.value;
    
        if (window.confirm("Are you sure you want to delete this customer?")) {
             fetch('/api/Customers/DeleteCustomer/' + id, {
                  method: 'Post',
                  headers: {
                    'Content-Type': 'application/json'
                  }
             })
/*                  .then(function (response) {
                    console.log(response)
                    return response.json();
                  })*/
                  .then(function (data) {
                    // redirect to the customers page to refresh the list and regenerate the ids
                    //window.location.href = "/";

                    // remove the customer from the list
                    var newCustomers = customers.filter(customer => customer.id != id);

                    // redo the ids
                    for (var i = 0; i < newCustomers.length; i++) {
                        newCustomers[i].id = i;
                    }

                    setCustomers(newCustomers);

                  }).catch(function (err) {
                    console.log(err)
                  });
        }

  }


  const searchElement = () => {
      return (
          <div class="input-group mb-3">
              <span class="input-group-text" id="button-addon1">Search By First Name</span>
              <input type="text" class="form-control" placeholder="Search" aria-label="Search" aria-describedby="button-addon2" onChange={onSearchChange} />
              {/*<button class="btn btn-outline-secondary" type="button" id="button-addon2">Search</button>*/}
          </div>
      );
  }

  const renderCustomersTable = (customerList) => {
      return (
          <div class="table-responsive">

          <table className='table align-middle table-striped' aria-labelledby="tableLabel">
            <thead>
              <tr>
                <th>Id</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Phone</th>
                <th>Email</th>
                <th>Edit</th>
                <th>Delete</th>
              </tr>
            </thead>
            <tbody>
              {customerList.map(customer =>
                  <tr key={customer.id}>
                      <td>{customer.id}</td>
                      <td>{customer.first_name}</td>
                      <td>{customer.last_name}</td>
                      <td>{customer.phone}</td>
                      <td>{customer.email}</td>
                      <td><a href={"/edit/" + customer.id } class="btn btn-primary">Edit</a></td>
                      <td><button class="btn btn-danger" value={customer.id} onClick={onDelete}>Delete</button></td>
                </tr>
              )}
            </tbody>
        </table>
      </div>
    );
  }

  const contents = loading
    ? <p><em>Loading...</em></p>
      :
      <>
        {searchElement()}
        {renderCustomersTable(customers)}
      </>

  return (
    <div>
      <h1 id="tableLabel">Customers</h1>
      {contents}
    </div>
  );
}
