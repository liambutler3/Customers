import React, { useState, useEffect, Link } from 'react';

export function Customers()
{
  const [loading, setLoading] = useState(true);
  const [customers, setCustomers] = useState([]);
  const [onLoadcustomers, setOnLoadCustomers] = useState([]);

  // on load, get all customers
  useEffect(async () => {
    const response = await fetch('/api/Customers/Get');
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
              <em>Note: Id is a generated index of the customers collection, using as a unqiue reference for each listed customer</em>

          <table className='table align-middle table-striped' aria-labelledby="tableLabel">
            <thead>
              <tr>
                <th>Id</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Phone</th>
                <th>Email</th>
                <th>Edit</th>
              </tr>
            </thead>
            <tbody>
              {customerList.map((customer, index) =>
                  <tr key={customer.email}>
                      <td>{index}</td>
                      <td>{customer.first_name}</td>
                      <td>{customer.last_name}</td>
                      <td>{customer.phone}</td>
                      <td>{customer.email}</td>
                      <td>
                      <a href={"/edit/" + index } class="btn btn-primary">Edit</a>
                      </td>
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
