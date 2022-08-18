import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { Route } from 'react-router';

import './App.scss';
import Home from './components/Home';
import { Layout } from './components/Layout';
import Settings from './components/Settings';
import { requestGetPomodoroConfigs } from './store/slices/pomodoroSlice';

const App = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(requestGetPomodoroConfigs());
  }, [dispatch]);

  return (
    <Layout>
      <Route exact path="/" component={Home} />
      <Route path="/settings" component={Settings} />
    </Layout>
  );
};

export default App;
