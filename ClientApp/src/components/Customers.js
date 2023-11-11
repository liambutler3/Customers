import React, { useState, useEffect } from 'react';

export function Customers() {
  const [loading, setLoading] = useState(true);
  const [customers, setCustomers] = useState([]);

  useEffect(async () => {
    const response = await fetch('/api/Customers');
    const data = await response.json();

    setCustomers(data)
    setLoading(false);
  }, []);

  const renderCustomersTable = (customerList) => {
    return (
      <table className='table table-striped' aria-labelledby="tableLabel">
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
    );
  }

  const contents = loading
    ? <p><em>Loading...</em></p>
    : renderCustomersTable(customers);


  return (
    <div>
      <h1 id="tableLabel">Customers</h1>
      {contents}
    </div>
  );
}
