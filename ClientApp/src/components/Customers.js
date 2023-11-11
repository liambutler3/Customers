import React, { useState, useEffect } from 'react';

export function Customers()
{
  const [loading, setLoading] = useState(true);
  const [customers, setCustomers] = useState([]);
  const [onLoadcustomers, setOnLoadCustomers] = useState([]);

  // on load, get all customers
  useEffect(async () => {
    const response = await fetch('/api/Customers');
    const data = await response.json();

    // on load customers
    setOnLoadCustomers(data);

    setCustomers(data)
    setLoading(false);
  }, []);


  function onKeyPressed(e)
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
              <input type="text" class="form-control" placeholder="Search" aria-label="Search" aria-describedby="button-addon2" onChange={onKeyPressed} />
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
                <th>First Name</th>
                <th>Last Name</th>
                <th>Phone</th>
                <th>Email</th>
              </tr>
            </thead>
            <tbody>
              {customerList.map(customer =>
                <tr key={customer.email}>
                      <td>{customer.first_name}</td>
                      <td>{customer.last_name}</td>
                      <td>{customer.phone}</td>
                      <td>{customer.email}</td>
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
