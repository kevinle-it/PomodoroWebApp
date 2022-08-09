import classnames from 'classnames';
import React, { useCallback, useMemo } from 'react';
import './styles.scss';
import useCountdown from './useCountdown';

const POMODORO_MODES = Object.freeze({
  POMODORO: {
    type: Symbol('POMODORO'),
    time: {
      minutes: 25,
      seconds: 0,
    },
  },
  SHORT_BREAK: {
    type: Symbol('SHORT_BREAK'),
    time: {
      minutes: 5,
      seconds: 0,
    },
  },
  LONG_BREAK: {
    type: Symbol('LONG_BREAK'),
    time: {
      minutes: 15,
      seconds: 0,
    },
  },
});

const BASE_DIGIT_WIDTH = 74; // 74px

const CountdownTimer = () => {
  const timerType = POMODORO_MODES.POMODORO.type;

  const { minutes, seconds, shouldRun, setShouldRun } = useCountdown({
    initialMinutes: POMODORO_MODES.POMODORO.time.minutes,
    initialSeconds: POMODORO_MODES.POMODORO.time.seconds,
    shouldStartImmediately: false,
  });

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

  return (
    <div className="countdown-timer__wrapper">
      <div className="countdown-timer__type-wrapper">
        <button className={classnames(
          'countdown-timer__type',
          { 'countdown-timer__type--activated': timerType === POMODORO_MODES.POMODORO.type },
        )}>
          Pomodoro
        </button>
        <button className={classnames(
          'countdown-timer__type',
          { 'countdown-timer__type--activated': timerType === POMODORO_MODES.SHORT_BREAK.type },
        )}>
          Short Break
        </button>
        <button className={classnames(
          'countdown-timer__type',
          { 'countdown-timer__type--activated': timerType === POMODORO_MODES.LONG_BREAK.type },
        )}>
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
