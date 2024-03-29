﻿import classnames from 'classnames';
import React, { useCallback, useEffect, useMemo, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { getCurrentPomodoroMode, getNextPomodoroMode, POMODORO_MODES } from '../../helpers/pomodoroHelper';
import {
  selectConfigs,
  selectCurrentTaskId,
  selectNumCompletedLongBreaks,
  selectNumCompletedPoms,
  selectNumCompletedShortBreaks,
} from '../../store/selectors/pomodoroSelector';
import {
  requestOnCompleteCurrentLongBreak,
  requestOnCompleteCurrentPomodoro,
  requestOnCompleteCurrentShortBreak,
} from '../../store/slices/pomodoroSlice';
import './styles.scss';
import useCountdown from './useCountdown';

const BASE_DIGIT_WIDTH = 74; // 74px

const CountdownTimer = () => {
  const dispatch = useDispatch();

  const {
    currentTaskId,
    numCompletedPoms,
    numCompletedShortBreaks,
    numCompletedLongBreaks,
    configs,
  } = useSelector((state) => ({
    currentTaskId: selectCurrentTaskId(state),
    numCompletedPoms: selectNumCompletedPoms(state),
    numCompletedShortBreaks: selectNumCompletedShortBreaks(state),
    numCompletedLongBreaks: selectNumCompletedLongBreaks(state),
    configs: selectConfigs(state),
  }));

  const {
    minutes, setMinutes,
    seconds,
    shouldRun, setShouldRun,
  } = useCountdown({
    initialMinutes: configs.pomodoroLength,
    shouldStartImmediately: false,
  });

  const [currentMode, setCurrentMode] = useState(() => getCurrentPomodoroMode(
    numCompletedPoms,
    numCompletedShortBreaks,
    numCompletedLongBreaks,
    configs.longBreakInterval,
  ));

  useEffect(() => {
    setCurrentMode(getCurrentPomodoroMode(
      numCompletedPoms,
      numCompletedShortBreaks,
      numCompletedLongBreaks,
      configs.longBreakInterval,
    ));
  }, [
    configs.longBreakInterval,
    currentTaskId,
    numCompletedLongBreaks,
    numCompletedPoms,
    numCompletedShortBreaks,
  ]);

  useEffect(() => {
    switch (currentMode) {
      case POMODORO_MODES.POMODORO.type:
        setMinutes(configs.pomodoroLength);
        break;
      case POMODORO_MODES.SHORT_BREAK.type:
        setMinutes(configs.shortBreakLength);
        break;
      case POMODORO_MODES.LONG_BREAK.type:
        setMinutes(configs.longBreakLength);
        break;
      default:
        setMinutes(configs.pomodoroLength);
        break;
    }
  }, [
    configs.longBreakLength,
    configs.pomodoroLength,
    configs.shortBreakLength,
    currentMode,
    setMinutes,
  ]);

  // const onChangePomodoroMode = useCallback(() => {
  //   if (currentMode !== POMODORO_MODES.POMODORO.type) {
  //     setCurrentMode(POMODORO_MODES.POMODORO.type);
  //   }
  // }, [currentMode]);
  //
  // const onChangeShortBreakMode = useCallback(() => {
  //   if (currentMode !== POMODORO_MODES.SHORT_BREAK.type) {
  //     setCurrentMode(POMODORO_MODES.SHORT_BREAK.type);
  //   }
  // }, [currentMode]);
  //
  // const onChangeLongBreakMode = useCallback(() => {
  //   if (currentMode !== POMODORO_MODES.LONG_BREAK.type) {
  //     setCurrentMode(POMODORO_MODES.LONG_BREAK.type);
  //   }
  // }, [currentMode]);

  const onButtonClick = useCallback(() => {
    setShouldRun(!shouldRun);
  }, [setShouldRun, shouldRun]);

  const getLeftDigitOfTwoDigitsNumber = useCallback((number) => {
    return Math.floor(number / 10);
  }, []);

  const getRightDigitOfTwoDigitsNumber = useCallback((number) => {
    return number % 10;
  }, []);

  const getLeftDigitsAndStyle = useCallback((positiveNum) => {
    // Number may be greater than 99
    // => more than 2 digits (100, 9999,...)
    // => drop right most digit, get all others
    let leftDigits = getLeftDigitOfTwoDigitsNumber(positiveNum);
    // If leftDigits > 0, start counting number of digits from 0, else leftDigits = 0 => numDigits = 1
    let numDigits = leftDigits > 0 ? 0 : 1;
    let leftDigitsArray = [];
    while (leftDigits > 0) {
      leftDigitsArray.push(leftDigits % 10);
      leftDigits = Math.floor(leftDigits / 10);
      ++numDigits;
    }
    return {
      digitsArray: leftDigitsArray.reverse(),
      style: {
        width: BASE_DIGIT_WIDTH * numDigits,
      },
    };
  }, [getLeftDigitOfTwoDigitsNumber]);

  const minutesLeftDigitsArrayAndStyle = useMemo(() => {
    return getLeftDigitsAndStyle(minutes);
  }, [getLeftDigitsAndStyle, minutes]);

  useEffect(() => {
    if (minutes === 0 && seconds === 0 &&
        currentTaskId && typeof currentTaskId === 'number' && currentTaskId > 0) {

      switch (currentMode) {
        case POMODORO_MODES.POMODORO.type:
          dispatch(requestOnCompleteCurrentPomodoro(currentTaskId));
          break;
        case POMODORO_MODES.SHORT_BREAK.type:
          dispatch(requestOnCompleteCurrentShortBreak(currentTaskId));
          break;
        case POMODORO_MODES.LONG_BREAK.type:
          dispatch(requestOnCompleteCurrentLongBreak(currentTaskId));
          break;
        default:
          dispatch(requestOnCompleteCurrentPomodoro(currentTaskId));
          break;
      }

      const nextPomodoroMode = getNextPomodoroMode(
        currentMode,
        numCompletedPoms,
        numCompletedShortBreaks,
        configs?.longBreakInterval,
      );
      setCurrentMode(nextPomodoroMode);
      switch (nextPomodoroMode) {
        case POMODORO_MODES.POMODORO.type:
          setMinutes(configs?.pomodoroLength || POMODORO_MODES.POMODORO.time.minutes);
          if (configs?.autoStartPom) {
            setShouldRun(true);
          }
          break;
        case POMODORO_MODES.SHORT_BREAK.type:
          setMinutes(configs?.shortBreakLength || POMODORO_MODES.SHORT_BREAK.time.minutes);
          if (configs?.autoStartBreak) {
            setShouldRun(true);
          }
          break;
        case POMODORO_MODES.LONG_BREAK.type:
          setMinutes(configs?.longBreakLength || POMODORO_MODES.LONG_BREAK.time.minutes);
          if (configs?.autoStartBreak) {
            setShouldRun(true);
          }
          break;
        default:
          setMinutes(configs?.pomodoroLength || POMODORO_MODES.POMODORO.time.minutes);
          if (configs?.autoStartPom) {
            setShouldRun(true);
          }
          break;
      }
    }
  }, [
    configs.autoStartBreak,
    configs.autoStartPom,
    configs.longBreakInterval,
    configs.longBreakLength,
    configs.pomodoroLength,
    configs.shortBreakLength,
    currentMode,
    currentTaskId,
    dispatch,
    minutes,
    numCompletedPoms,
    numCompletedShortBreaks,
    seconds,
    setMinutes,
    setShouldRun,
  ]);

  return (
    <div className="countdown-timer__wrapper">
      <div className="countdown-timer__type-wrapper">
        <button
          className={classnames(
            'countdown-timer__type',
            { 'countdown-timer__type--activated': currentMode === POMODORO_MODES.POMODORO.type },
          )}
          // onClick={onChangePomodoroMode}
        >
          Pomodoro
        </button>
        <button
          className={classnames(
            'countdown-timer__type',
            { 'countdown-timer__type--activated': currentMode === POMODORO_MODES.SHORT_BREAK.type },
          )}
          // onClick={onChangeShortBreakMode}
        >
          Short Break
        </button>
        <button
          className={classnames(
            'countdown-timer__type',
            { 'countdown-timer__type--activated': currentMode === POMODORO_MODES.LONG_BREAK.type },
          )}
          // onClick={onChangeLongBreakMode}
        >
          Long Break
        </button>
      </div>
      <div className="countdown-timer__timer">
        {minutes < 100 ? (
          <div className="countdown-timer__timer__digit">{getLeftDigitOfTwoDigitsNumber(minutes)}</div>
        ) : (
          <div className="countdown-timer__timer__digit__minute--left"
               style={minutesLeftDigitsArrayAndStyle.style}>
            {minutesLeftDigitsArrayAndStyle.digitsArray.map((digit, index) => (
              <div key={String(index)} style={{ width: BASE_DIGIT_WIDTH }}>{digit}</div>
            ))}
          </div>
        )}
        <div className="countdown-timer__timer__digit">{getRightDigitOfTwoDigitsNumber(minutes)}</div>
        <div className="countdown-timer__timer__colon">:</div>
        <div className="countdown-timer__timer__digit">{getLeftDigitOfTwoDigitsNumber(seconds)}</div>
        <div className="countdown-timer__timer__digit">{getRightDigitOfTwoDigitsNumber(seconds)}</div>
      </div>
      <div className="countdown-timer__button">
        <button onClick={onButtonClick}>{!shouldRun ? 'Start' : 'Stop'}</button>
      </div>
    </div>
  );
};

export default CountdownTimer;
