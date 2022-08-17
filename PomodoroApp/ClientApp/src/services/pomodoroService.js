import axios from 'axios';

export const requestCreateNewPomodoroTask = async(taskName, numEstimatedPoms) => {
  // Return the current pomodoroTask object
  return await axios.post(`/api/pomodoro`, {
    name: taskName,
    numEstimatedPoms,
  });
};

export const requestOnCompleteCurrentPomodoro = async(taskId) => {
  // Return the current pomodoroTask object
  return await axios.put(`/api/pomodoro/${taskId}`);
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
