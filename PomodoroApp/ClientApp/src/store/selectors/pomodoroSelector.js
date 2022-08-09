export const selectIsLoading = (state) => state.pomodoro.isLoading;
export const selectCurrentTaskId = (state) => state.pomodoro.currentTaskId;
export const selectCurrentTaskName = (state) => state.pomodoro.currentTaskName;
export const selectNumEstimatedPoms = (state) => state.pomodoro.numEstimatedPoms;
export const selectNumCompletedPoms = (state) => state.pomodoro.numCompletedPoms;
export const selectNumCompletedShortBreaks = (state) => state.pomodoro.numCompletedShortBreaks;
export const selectIsCompletedLongBreak = (state) => state.pomodoro.isCompletedLongBreak;
export const selectListTasks = (state) => state.pomodoro.listTasks;
