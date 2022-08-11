import { createSlice } from '@reduxjs/toolkit';

const taskReducers = {
  requestCreateNewPomodoroTask: (state) => {
    state.isLoading = true;
  },
  requestCreateNewPomodoroTaskSuccess: (state, action) => {
    const pomodoroTask = action.payload;
    state.isLoading = false;
    state.listTasks.push(pomodoroTask);
  },
  requestCreateNewPomodoroTaskFailure: (state) => {
    state.isLoading = false;
  },
};

const pomodoroReducers = {
  requestOnCompleteCurrentPomodoro: (state) => {
    state.isLoading = true;
  },
  requestOnCompleteCurrentPomodoroSuccess: (state, action) => {
    const pomodoroTask = action.payload;
    state.isLoading = false;
    state.currentTaskId = pomodoroTask?.taskId;
    state.currentTaskName = pomodoroTask?.name;
    state.numEstimatedPoms = pomodoroTask?.numEstimatedPoms;
    state.numCompletedPoms = pomodoroTask?.numCompletedPoms;
    state.numCompletedShortBreaks = pomodoroTask?.numCompletedShortBreaks;
    state.isCompletedLongBreak = pomodoroTask?.isCompletedLongBreak;

    const foundIndex = state.listTasks.findIndex(task => task.taskId === pomodoroTask?.taskId);
    state.listTasks[foundIndex].numCompletedPoms = pomodoroTask?.numCompletedPoms;
  },
  requestOnCompleteCurrentPomodoroError: (state) => {
    state.isLoading = false;
  },
};

export const pomodoroSlice = createSlice({
  name: 'pomodoro',
  initialState: {
    isLoading: false,
    currentTaskId: undefined,
    currentTaskName: '',
    numEstimatedPoms: 4,
    numCompletedPoms: 0,
    numCompletedShortBreaks: 0,
    isCompletedLongBreak: false,
    listTasks: [],
  },
  reducers: {
    ...pomodoroReducers,
    ...taskReducers,
  },
});

export const {
  requestCreateNewPomodoroTask,
  requestCreateNewPomodoroTaskSuccess,
  requestCreateNewPomodoroTaskFailure,
  requestOnCompleteCurrentPomodoro,
  requestOnCompleteCurrentPomodoroSuccess,
  requestOnCompleteCurrentPomodoroError,
} = pomodoroSlice.actions;

export default pomodoroSlice.reducer;
