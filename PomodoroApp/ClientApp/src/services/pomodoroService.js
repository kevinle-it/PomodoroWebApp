import axios from 'axios';

export const requestOnCompleteCurrentPomodoro = async(taskId) => {
  // Return the current pomodoroTask object
  return await axios.put(`/api/pomodoro/${taskId}`);
};
