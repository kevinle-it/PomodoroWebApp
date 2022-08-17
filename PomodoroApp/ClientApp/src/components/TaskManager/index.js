import React, { useCallback, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { selectListTasks } from '../../store/selectors/pomodoroSelector';
import { requestCreateNewPomodoroTask, requestGetAllPomodoroTasks, selectTask } from '../../store/slices/pomodoroSlice';
import PomodoroTask from '../PomodoroTask';
import { ReactComponent as PlusIcon } from './../../assets/ic_plus.svg';
import { ReactComponent as TriangleDownIcon } from './../../assets/ic_triangle_down.svg';
import { ReactComponent as TriangleUpIcon } from './../../assets/ic_triangle_up.svg';
import './styles.scss';

const TaskManager = () => {
  const dispatch = useDispatch();
  const listTasks = useSelector(selectListTasks);
  const [listTasksToShow, setListTasksToShow] = useState([]);

  useEffect(() => {
    if (listTasks?.length > 0) {
      setListTasksToShow(listTasks);
    }
  }, [listTasks]);

  useEffect(() => {
    dispatch(requestGetAllPomodoroTasks());
  }, [dispatch]);

  const [showAddTaskBox, setShowAddTaskBox] = useState(false);
  const [numEstimatedPoms, setNumEstimatedPoms] = useState(1);
  const [submitDisabled, setSubmitDisabled] = useState(true);

  const onAddTaskClick = useCallback(() => {
    setShowAddTaskBox(true);
  }, []);

  const onTaskNameInputChange = useCallback((e) => {
    if (e.target.value) {
      setSubmitDisabled(false);
    } else {
      setSubmitDisabled(true);
    }
  }, []);

  const onIncreaseNumEstimatedPomsClick = useCallback(() => {
    setNumEstimatedPoms(numEstimatedPoms + 1);
  }, [numEstimatedPoms]);

  const onDecreaseNumEstimatedPomsClick = useCallback(() => {
    setNumEstimatedPoms(
      numEstimatedPoms - 1 >= 0
        ? numEstimatedPoms - 1
        : 0,
    );
  }, [numEstimatedPoms]);

  const onCancelAddTaskClick = useCallback(() => {
    setShowAddTaskBox(false);
  }, []);

  const handleSubmit = useCallback((e) => {
    setShowAddTaskBox(false);
    const taskName = e.target.taskName.value;
    const numEstimatedPoms = e.target.numEstimatedPoms.value;
    dispatch(requestCreateNewPomodoroTask({
      taskName,
      numEstimatedPoms,
    }));
    e.preventDefault();
  }, [dispatch]);

  const onTaskClick = useCallback((selectedIndex) => {
    dispatch(selectTask(listTasksToShow?.at(selectedIndex)));
    setListTasksToShow(listTasksToShow.map((task, index) => {
      if (task.isSelected && index !== selectedIndex) {
        // Reset selected
        return {
          ...task,
          isSelected: false,
        };
      } else if (index === selectedIndex) {
        // Set new selected
        return {
          ...task,
          isSelected: true,
        };
      }
      return { ...task };
    }));
  }, [dispatch, listTasksToShow]);

  return (
    <div className="task-manager__wrapper">
      <div className="task-manager__title">Tasks</div>
      {listTasksToShow && listTasksToShow.map((task, index) => (
        <PomodoroTask
          key={String(index)}
          name={task?.name}
          numCompletedPoms={task?.numCompletedPoms}
          numEstimatedPoms={task?.numEstimatedPoms}
          isSelected={task?.isSelected}
          onClick={() => onTaskClick(index)} />
      ))}
      {showAddTaskBox ? (
        <form className="task-manager__add-task-form__wrapper" onSubmit={handleSubmit}>
          <div className="task-manager__add-task-form__wrapper__inner">
            <input className="task-manager__add-task-form__task-name-input"
                   name="taskName"
                   autoFocus
                   type="text"
                   placeholder="What are you working on?"
                   onChange={onTaskNameInputChange} />
            <div className="task-manager__add-task-form__estimated-poms__title">
              Estimate Pomodoros
            </div>
            <div className="task-manager__add-task-form__estimated-poms__input__wrapper">
              <input name="numEstimatedPoms" type="number" value={numEstimatedPoms} disabled />
              <button className="task-manager__add-task-form__estimated-poms__input__button"
                      type="button"
                      onClick={onIncreaseNumEstimatedPomsClick}>
                <TriangleUpIcon />
              </button>
              <button className="task-manager__add-task-form__estimated-poms__input__button"
                      type="button"
                      onClick={onDecreaseNumEstimatedPomsClick}>
                <TriangleDownIcon />
              </button>
            </div>
          </div>

          <div className="task-manager__add-task-form__submit__wrapper">
            <button type="button"
                    onClick={onCancelAddTaskClick}>
              Cancel
            </button>
            <input type="submit"
                   value="Save"
                   disabled={submitDisabled || numEstimatedPoms === 0} />
          </div>
        </form>
      ) : (
        <button className="task-manager__add-task-button" onClick={onAddTaskClick}>
          <PlusIcon />
          <div>Add Task</div>
        </button>
      )}
    </div>
  );
};

export default TaskManager;
