import { spawn } from 'redux-saga/effects';
import pomodoroSaga from './pomodoroSaga';

export default function* rootSaga() {
  yield spawn(pomodoroSaga);
}
