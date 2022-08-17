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
    if (foundIndex !== -1) {
      state.listTasks[foundIndex].numCompletedPoms = pomodoroTask?.numCompletedPoms;
    } else {
      state.listTasks.push(pomodoroTask);
    }
  },
  requestOnCompleteCurrentPomodoroError: (state) => {
    state.isLoading = false;
  },
  requestOnCompleteCurrentShortBreak: (state) => {
    state.isLoading = true;
  },
  requestOnCompleteCurrentShortBreakSuccess: (state, action) => {
    const pomodoroTask = action.payload;
    state.isLoading = false;
    state.currentTaskId = pomodoroTask?.taskId;
    state.currentTaskName = pomodoroTask?.name;
    state.numEstimatedPoms = pomodoroTask?.numEstimatedPoms;
    state.numCompletedPoms = pomodoroTask?.numCompletedPoms;
    state.numCompletedShortBreaks = pomodoroTask?.numCompletedShortBreaks;
    state.isCompletedLongBreak = pomodoroTask?.isCompletedLongBreak;

    const foundIndex = state.listTasks.findIndex(task => task.taskId === pomodoroTask?.taskId);
    if (foundIndex !== -1) {
      state.listTasks[foundIndex].numCompletedShortBreaks = pomodoroTask?.numCompletedShortBreaks;
    } else {
      state.listTasks.push(pomodoroTask);
    }
  },
  requestOnCompleteCurrentShortBreakError: (state) => {
    state.isLoading = false;
  },
};

const configsReducers = {
  requestGetPomodoroConfigs: (state) => {
    state.isLoading = true;
  },
  requestGetPomodoroConfigsSuccess: (state, action) => {
    state.isLoading = false;
    state.configs = action.payload;
  },
  requestGetPomodoroConfigsError: (state) => {
    state.isLoading = false;
  },
  requestUpdatePomodoroConfigs: (state) => {
    state.isLoading = true;
  },
  requestUpdatePomodoroConfigsSuccess: (state, action) => {
    state.isLoading = false;
    state.configs = action.payload;
  },
  requestUpdatePomodoroConfigsError: (state) => {
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
    configs: {
      pomodoroLength: 25,
      shortBreakLength: 5,
      longBreakLength: 15,
      autoStartPom: false,
      autoStartBreak: false,
      longBreakInterval: 4,
    },
  },
  reducers: {
    ...taskReducers,
    ...pomodoroReducers,
    ...configsReducers,
  },
});

export const {
  requestCreateNewPomodoroTask,
  requestCreateNewPomodoroTaskSuccess,
  requestCreateNewPomodoroTaskFailure,
  requestOnCompleteCurrentPomodoro,
  requestOnCompleteCurrentPomodoroSuccess,
  requestOnCompleteCurrentPomodoroError,
  requestOnCompleteCurrentShortBreak,
  requestOnCompleteCurrentShortBreakSuccess,
  requestOnCompleteCurrentShortBreakError,
  requestGetPomodoroConfigs,
  requestGetPomodoroConfigsSuccess,
  requestGetPomodoroConfigsError,
  requestUpdatePomodoroConfigs,
  requestUpdatePomodoroConfigsSuccess,
  requestUpdatePomodoroConfigsError,
} = pomodoroSlice.actions;

export default pomodoroSlice.reducer;
