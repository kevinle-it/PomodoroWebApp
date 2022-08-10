import { createSelector } from '@reduxjs/toolkit';
import React from 'react';
import { useSelector } from 'react-redux';
import { selectCurrentTaskId, selectCurrentTaskName } from '../../store/selectors/pomodoroSelector';
import CountdownTimer from '../CountdownTimer';
import TaskManager from '../TaskManager';
import './styles.scss';

const Home = () => {
  const { currentTaskId = 1, currentTaskName } = useSelector(
    createSelector(
      selectCurrentTaskId,
      selectCurrentTaskName,
      (currentTaskId, currentTaskName) => ({
        currentTaskId,
        currentTaskName,
      })
  ));

  return (
    <div className="home__wrapper">
      <CountdownTimer />
      <div style={{ marginBottom: 10 }} />
      <div className="home__task-id">#{currentTaskId}</div>
      <div className="home__current-task-name">
        {currentTaskName || 'Time to focus!'}
      </div>
      <div style={{ marginBottom: 15 }} />
      <TaskManager />
    </div>
  );
};

export default Home;
