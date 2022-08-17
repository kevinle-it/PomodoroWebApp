import axios from 'axios';

export const requestCreateNewPomodoroTask = async(taskName, numEstimatedPoms) => {
  // Return the current pomodoroTask object
  return await axios.post(`/api/pomodoro`, {
    name: taskName,
    numEstimatedPoms,
  });
};

export const requestGetAllPomodoroTasks = async() => {
  // Return the list of pomodoroTask objects
  return await axios.get(`/api/pomodoro`);
};

export const requestOnCompleteCurrentPomodoro = async(taskId) => {
  // Return the current pomodoroTask object
  return await axios.put(`/api/pomodoro/complete/pomodoro/${taskId}`);
};

export const requestOnCompleteCurrentShortBreak = async(taskId) => {
  // Return the current pomodoroTask object
  return await axios.put(`/api/pomodoro/complete/shortbreak/${taskId}`);
};

export const requestGetPomodoroConfigs = async() => {
  // Return the current pomodoro configs
  return await axios.get(`/api/pomodoro/config`);
};

export const requestUpdatePomodoroConfigs = async(configs) => {
  // Return the current pomodoro configs
  return await axios.put(`/api/pomodoro/config`, {
    ...configs,
  });
};
