import React, { Component } from 'react';
import { Route } from 'react-router';

import './App.scss';
import CountdownTimer from './components/CountdownTimer';
import { Counter } from './components/Counter';
import { FetchData } from './components/FetchData';
import { Layout } from './components/Layout';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={CountdownTimer} />
        <Route path="/counter" component={Counter} />
        <Route path="/fetch-data" component={FetchData} />
      </Layout>
    );
  }
}
