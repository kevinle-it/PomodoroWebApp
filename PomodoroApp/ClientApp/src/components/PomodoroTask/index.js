import React from 'react';
import { ReactComponent as CheckmarkIcon } from './../../assets/ic_checkmark.svg';
import { ReactComponent as ThreeDotVerticalIcon } from './../../assets/ic_three_dot_vertical.svg';
import './styles.scss';

const PomodoroTask = (props) => {
  const { name = 'Dummy Task', numCompletedPoms = 0, numEstimatedPoms = 4 } = props;

  return (
    <div className="pomodoro-task__wrapper">
      <div className="pomodoro-task__info--left">
        <CheckmarkIcon />
        <div className="pomodoro-task__name">{name}</div>
      </div>
      <div className="pomodoro-task__info--right">
        <div className="pomodoro-task__summary"><span>{numCompletedPoms}</span> / {numEstimatedPoms}</div>
        <button className="pomodoro-task__button"><ThreeDotVerticalIcon /></button>
      </div>
    </div>
  );
};

export default PomodoroTask;
