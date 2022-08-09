import { call, put, takeLeading } from 'redux-saga/effects';
import { requestOnCompleteCurrentPomodoro } from '../../services/pomodoroService';
import {
  requestOnCompleteCurrentPomodoroError,
  requestOnCompleteCurrentPomodoroSuccess,
} from '../slices/pomodoroSlice';

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
  yield takeLeading('pomodoro/requestOnCompleteCurrentPomodoro', onCompleteCurrentPomodoro);
}

export default pomodoroSaga;
