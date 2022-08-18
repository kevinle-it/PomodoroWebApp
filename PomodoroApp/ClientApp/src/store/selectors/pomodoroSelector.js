export const selectIsLoading = (state) => state.pomodoro.isLoading;

// Current task
export const selectCurrentTaskId = (state) => state.pomodoro.currentTaskId;
export const selectCurrentTaskName = (state) => state.pomodoro.currentTaskName;
export const selectNumEstimatedPoms = (state) => state.pomodoro.numEstimatedPoms;
export const selectNumCompletedPoms = (state) => state.pomodoro.numCompletedPoms;
export const selectNumCompletedShortBreaks = (state) => state.pomodoro.numCompletedShortBreaks;
export const selectNumCompletedLongBreaks = (state) => state.pomodoro.numCompletedLongBreaks;

// List tasks
export const selectListTasks = (state) => state.pomodoro.listTasks;

// Pomodoro configs
export const selectConfigs = (state) => state.pomodoro.configs;
