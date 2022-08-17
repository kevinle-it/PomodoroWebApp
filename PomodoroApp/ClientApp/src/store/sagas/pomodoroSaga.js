import { call, put, takeLeading } from 'redux-saga/effects';
import {
  requestCreateNewPomodoroTask,
  requestGetAllPomodoroTasks,
  requestGetPomodoroConfigs,
  requestOnCompleteCurrentPomodoro,
  requestOnCompleteCurrentShortBreak,
  requestUpdatePomodoroConfigs,
} from '../../services/pomodoroService';
import {
  requestCreateNewPomodoroTaskFailure,
  requestCreateNewPomodoroTaskSuccess,
  requestGetAllPomodoroTasksFailure,
  requestGetAllPomodoroTasksSuccess,
  requestGetPomodoroConfigsError,
  requestGetPomodoroConfigsSuccess,
  requestOnCompleteCurrentPomodoroError,
  requestOnCompleteCurrentPomodoroSuccess,
  requestOnCompleteCurrentShortBreakError,
  requestOnCompleteCurrentShortBreakSuccess,
  requestUpdatePomodoroConfigsError,
  requestUpdatePomodoroConfigsSuccess,
} from '../slices/pomodoroSlice';

function* createNewPomodoroTask(action) {
  const { taskName, numEstimatedPoms } = action.payload;
  try {
    const response = yield call(requestCreateNewPomodoroTask, taskName, numEstimatedPoms);
    if (response?.status === 201) {
      const pomodoroTask = response?.data;
      yield put(requestCreateNewPomodoroTaskSuccess(pomodoroTask));
    } else {
      yield put(requestCreateNewPomodoroTaskFailure());
    }
  } catch (e) {
    yield put(requestCreateNewPomodoroTaskFailure());
  }
}

function* getAllPomodoroTasks() {
  try {
    const response = yield call(requestGetAllPomodoroTasks);
    if (response?.status === 200) {
      const listTasks = response?.data;
      yield put(requestGetAllPomodoroTasksSuccess(listTasks));
    } else {
      yield put(requestGetAllPomodoroTasksFailure());
    }
  } catch (e) {
    yield put(requestGetAllPomodoroTasksFailure());
  }
}

function* onCompleteCurrentPomodoro(action) {
  const taskId = action.payload;
  try {
    const response = yield call(requestOnCompleteCurrentPomodoro, taskId);
    if (response?.status === 200) {
      const pomodoroTask = response?.data;
      yield put(requestOnCompleteCurrentPomodoroSuccess(pomodoroTask));
    } else {
      yield put(requestOnCompleteCurrentPomodoroError());
    }
  } catch (e) {
    yield put(requestOnCompleteCurrentPomodoroError());
  }
}

function* onCompleteCurrentShortBreak(action) {
  const taskId = action.payload;
  try {
    const response = yield call(requestOnCompleteCurrentShortBreak, taskId);
    if (response?.status === 200) {
      const pomodoroTask = response?.data;
      yield put(requestOnCompleteCurrentShortBreakSuccess(pomodoroTask));
    } else {
      yield put(requestOnCompleteCurrentShortBreakError());
    }
  } catch (e) {
    yield put(requestOnCompleteCurrentShortBreakError());
  }
}

function* getPomodoroConfigs() {
  try {
    const response = yield call(requestGetPomodoroConfigs);
    if (response?.status === 200) {
      const configs = response?.data;
      delete configs?.id;
      yield put(requestGetPomodoroConfigsSuccess(configs));
    } else {
      yield put(requestGetPomodoroConfigsError());
    }
  } catch (e) {
    yield put(requestGetPomodoroConfigsError());
  }
}

function* updatePomodoroConfigs(action) {
  const configs = action.payload;
  try {
    const response = yield call(requestUpdatePomodoroConfigs, configs);
    if (response?.status === 200) {
      const configs = response?.data;
      delete configs?.id;
      yield put(requestUpdatePomodoroConfigsSuccess(configs));
    } else {
      yield put(requestUpdatePomodoroConfigsError());
    }
  } catch (e) {
    yield put(requestUpdatePomodoroConfigsError());
  }
}

function* pomodoroSaga() {
  // takeLatest won't work as expected because after dispatching 1st action, the yield call() has been invoked already
  // Then after the duplicated 2nd action is dispatched, the 1st saga function will be cancelled
  // => only the subsequent code after yield call() won't be executed (the yield put() line)
  // but the axios request executed by yield call() of 1st saga is still in progress
  // Then the 2nd yield call() still gets invoked as normal (because the 2nd dispatched action)
  // => Making another duplicated axios request.
  // => USE takeLeading instead, only run saga for 1st dispatched action, drop every other following actions.
  // Want to keep using takeLatest and do the axios cancellation in saga generator function
  // => REFER HERE: https://stackoverflow.com/a/65654254
  yield takeLeading('pomodoro/requestCreateNewPomodoroTask', createNewPomodoroTask);
  yield takeLeading('pomodoro/requestGetAllPomodoroTasks', getAllPomodoroTasks);
  yield takeLeading('pomodoro/requestOnCompleteCurrentPomodoro', onCompleteCurrentPomodoro);
  yield takeLeading('pomodoro/requestOnCompleteCurrentShortBreak', onCompleteCurrentShortBreak);
  yield takeLeading('pomodoro/requestGetPomodoroConfigs', getPomodoroConfigs);
  yield takeLeading('pomodoro/requestUpdatePomodoroConfigs', updatePomodoroConfigs);
}

export default pomodoroSaga;
