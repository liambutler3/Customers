import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Customers } from './components/Customers';
import './custom.css'

export default function App() {

  return (
    <Layout>
      <Route exact path='/' component={Customers} />
          {/*<Route path='/path' component={componentName} />*/}
    </Layout>
  );
}
