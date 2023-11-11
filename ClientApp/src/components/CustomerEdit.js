import React, { useState, useEffect } from 'react';

export function CustomerEdit()
{
    const [loading, setLoading] = useState(true);
    const [customer, setCustomer] = useState([]);
    const [index, setIndex] = useState();

    // on load, get customer
    useEffect(async () => {

        // get the index from the url
        var index = parseInt(window.location.pathname.split('/')[2]);
        console.log(index);

        setIndex(index);

        console.log(index);

        const response = await fetch('/Customers/GetCustomer/' + index);
        const data = await response.json();

        setCustomer(data)
        setLoading(false);

        console.log(data);
    }, []);

    function submitForm(e) {
        e.preventDefault();

        var form = document.getElementById("myform");

        var data = new FormData(form);

        data.append("id", index);
        data.append("first_name", document.getElementById("first_name").value);
        data.append("last_name", document.getElementById("last_name").value);
        data.append("email", document.getElementById("email").value);
        data.append("phone", document.getElementById("phone").value);


        fetch('/Customers/Edit', {
            method: 'POST',
            body: data
        })
        /*         .then(function (response) {
            console.log(response)
            return response.json();
        })*/
        .then(function (data) {
            // redirect to the customers page
            window.location.href = "/";
        }).catch(function (err) {
            console.log(err)
        });
    }

    const renderEditCustomerForm = (customer) => {
        return (
            <form id="myform">
                <hidden id="id" value={index} />
                <div class="mb-3">
                    <label for="first_name" class="form-label">First Name</label>
                    <input type="text" class="form-control" id="first_name" defaultValue={customer.first_name} />
                </div>
                <div class="mb-3">
                    <label for="last_name" class="form-label">Last Name</label>
                    <input type="text" class="form-control" id="last_name" defaultValue={customer.last_name} />
                </div>
                <div class="mb-3">
                    <label for="email" class="form-label">Email</label>
                    <input type="text" class="form-control" id="email" defaultValue={customer.email} />
                </div>
                <div class="mb-3">
                    <label for="phone" class="form-label">Phone</label>
                    <input type="text" class="form-control" id="phone" defaultValue={customer.phone} />
                </div>
                <button onClick={submitForm} class="btn btn-primary">Save</button>
            </form>
        );
    }

    const contents = loading
        ? <p><em>Loading...</em></p>
        :
        <>
            {renderEditCustomerForm(customer[0])}
        </>

  return (
    <div>
      <h1 id="tableLabel">Edit Customers</h1>
          {contents}
    </div>
  );
}
