import { createSlice } from '@reduxjs/toolkit';

export const initialState = {
  isLoading: false,
  currentTaskId: undefined,
  currentTaskName: '',
  numEstimatedPoms: 4,
  numCompletedPoms: 0,
  numCompletedShortBreaks: 0,
  numCompletedLongBreaks: 0,
  listTasks: [],
  configs: {
    pomodoroLength: 25,
    shortBreakLength: 5,
    longBreakLength: 15,
    autoStartPom: false,
    autoStartBreak: false,
    longBreakInterval: 4,
  },
};

const taskReducers = {
  selectTask: (state, action) => {
    const selectedTaskIndex = action.payload;
    if (selectedTaskIndex !== undefined
        && selectedTaskIndex !== null
        && selectedTaskIndex >= 0) {
      const selectedTask = state.listTasks[selectedTaskIndex];
      state.currentTaskId = selectedTask?.taskId;
      state.currentTaskName = selectedTask?.name;
      state.numEstimatedPoms = selectedTask?.numEstimatedPoms;
      state.numCompletedPoms = selectedTask?.numCompletedPoms;
      state.numCompletedShortBreaks = selectedTask?.numCompletedShortBreaks;
      state.numCompletedLongBreaks = selectedTask?.numCompletedLongBreaks;
    }
  },
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
  requestGetAllPomodoroTasks: (state) => {
    state.isLoading = true;
  },
  requestGetAllPomodoroTasksSuccess: (state, action) => {
    const listTasks = action.payload;
    state.isLoading = false;
    state.listTasks = listTasks;
  },
  requestGetAllPomodoroTasksFailure: (state) => {
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
    state.numCompletedLongBreaks = pomodoroTask?.numCompletedLongBreaks;

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
    state.numCompletedLongBreaks = pomodoroTask?.numCompletedLongBreaks;

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
  requestOnCompleteCurrentLongBreak: (state) => {
    state.isLoading = true;
  },
  requestOnCompleteCurrentLongBreakSuccess: (state, action) => {
    const pomodoroTask = action.payload;
    state.isLoading = false;
    state.currentTaskId = pomodoroTask?.taskId;
    state.currentTaskName = pomodoroTask?.name;
    state.numEstimatedPoms = pomodoroTask?.numEstimatedPoms;
    state.numCompletedPoms = pomodoroTask?.numCompletedPoms;
    state.numCompletedShortBreaks = pomodoroTask?.numCompletedShortBreaks;
    state.numCompletedLongBreaks = pomodoroTask?.numCompletedLongBreaks;

    const foundIndex = state.listTasks.findIndex(task => task.taskId === pomodoroTask?.taskId);
    if (foundIndex !== -1) {
      state.listTasks[foundIndex].numCompletedLongBreaks = pomodoroTask?.numCompletedLongBreaks;
    } else {
      state.listTasks.push(pomodoroTask);
    }
  },
  requestOnCompleteCurrentLongBreakError: (state) => {
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
  initialState,
  reducers: {
    ...taskReducers,
    ...pomodoroReducers,
    ...configsReducers,
  },
});

export const {
  selectTask,
  requestCreateNewPomodoroTask,
  requestCreateNewPomodoroTaskSuccess,
  requestCreateNewPomodoroTaskFailure,
  requestGetAllPomodoroTasks,
  requestGetAllPomodoroTasksSuccess,
  requestGetAllPomodoroTasksFailure,
  requestOnCompleteCurrentPomodoro,
  requestOnCompleteCurrentPomodoroSuccess,
  requestOnCompleteCurrentPomodoroError,
  requestOnCompleteCurrentShortBreak,
  requestOnCompleteCurrentShortBreakSuccess,
  requestOnCompleteCurrentShortBreakError,
  requestOnCompleteCurrentLongBreak,
  requestOnCompleteCurrentLongBreakSuccess,
  requestOnCompleteCurrentLongBreakError,
  requestGetPomodoroConfigs,
  requestGetPomodoroConfigsSuccess,
  requestGetPomodoroConfigsError,
  requestUpdatePomodoroConfigs,
  requestUpdatePomodoroConfigsSuccess,
  requestUpdatePomodoroConfigsError,
} = pomodoroSlice.actions;

export default pomodoroSlice.reducer;
