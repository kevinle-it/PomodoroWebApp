import React from 'react';
import { useSelector } from 'react-redux';
import { selectListTasks } from '../../store/selectors/pomodoroSelector';
import PomodoroTask from '../PomodoroTask';
import { ReactComponent as PlusIcon } from './../../assets/ic_plus.svg';
import './styles.scss';

const TaskManager = () => {
  const listTasks = useSelector(selectListTasks);

  return (
    <div className="task-manager__wrapper">
      <div className="task-manager__title">Tasks</div>
      {listTasks && listTasks.map(task => (
        <PomodoroTask
          name={task?.name}
          numCompletedPoms={task?.numCompletedPoms}
          numEstimatedPoms={task?.numEstimatedPoms} />
      ))}
      <button className="task-manager__add-task-button">
        <PlusIcon />
        <div>Add Task</div>
      </button>
    </div>
  );
};

export default TaskManager;
