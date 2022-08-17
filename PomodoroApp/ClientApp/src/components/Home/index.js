import { createSelector } from '@reduxjs/toolkit';
import React, { useMemo } from 'react';
import { useSelector } from 'react-redux';
import { getCurrentPomodoroMode, POMODORO_MODES } from '../../helpers/pomodoroHelper';
import {
  selectConfigs,
  selectCurrentTaskId,
  selectCurrentTaskName,
  selectNumCompletedPoms,
  selectNumCompletedShortBreaks,
} from '../../store/selectors/pomodoroSelector';
import CountdownTimer from '../CountdownTimer';
import TaskManager from '../TaskManager';
import './styles.scss';

const Home = () => {
  const {
    currentTaskId = 1,
    currentTaskName,
    numCompletedPoms,
    numCompletedShortBreaks,
    configs = {},
  } = useSelector(
    createSelector(
      selectCurrentTaskId,
      selectCurrentTaskName,
      selectNumCompletedPoms,
      selectNumCompletedShortBreaks,
      selectConfigs,
      (
        currentTaskId,
        currentTaskName,
        numCompletedPoms,
        numCompletedShortBreaks,
        configs,
      ) => ({
        currentTaskId,
        currentTaskName,
        numCompletedPoms,
        numCompletedShortBreaks,
        configs,
      }),
    ));

  const currentMode = useMemo(() => getCurrentPomodoroMode(
    numCompletedPoms,
    numCompletedShortBreaks,
    configs.longBreakInterval,
  ), [configs, numCompletedPoms, numCompletedShortBreaks]);

  const modeMessage = useMemo(() => {
    switch (currentMode) {
      case POMODORO_MODES.POMODORO.type:
        return 'Time to focus!';
      case POMODORO_MODES.SHORT_BREAK.type:
      case POMODORO_MODES.LONG_BREAK.type:
        return 'Time for a break!';
      default:
        return 'Time to focus!';
    }
  }, [currentMode]);

  return (
    <div className="home__wrapper">
      <CountdownTimer />
      <div style={{ marginBottom: 10 }} />
      {/*<div className="home__task-id">#{currentTaskId}</div>*/}
      <div className="home__current-task-name">
        {currentTaskName || modeMessage}
      </div>
      <div style={{ marginBottom: 15 }} />
      <TaskManager />
    </div>
  );
};

export default Home;
