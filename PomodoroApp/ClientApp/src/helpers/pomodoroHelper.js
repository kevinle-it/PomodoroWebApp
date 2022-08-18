export const POMODORO_MODES = Object.freeze({
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

export const getCurrentPomodoroMode = (
  numCompletedPoms,
  numCompletedShortBreaks,
  numCompletedLongBreaks,
  longBreakInterval,
) => {
  if (numCompletedPoms === 0) {
    return POMODORO_MODES.POMODORO.type;
  }
  if (numCompletedPoms % longBreakInterval !== 0) {
    if (numCompletedShortBreaks + numCompletedLongBreaks < numCompletedPoms) {
      return POMODORO_MODES.SHORT_BREAK.type;
    }
    return POMODORO_MODES.POMODORO.type;
  } else if (numCompletedPoms / longBreakInterval === numCompletedLongBreaks) {
    return POMODORO_MODES.POMODORO.type;
  }
  return POMODORO_MODES.LONG_BREAK.type;
};

export const getNextPomodoroMode = (
  currentMode,
  numCompletedPoms,
  numCompletedShortBreaks,
  longBreakInterval,
) => {
  switch (currentMode) {
    case POMODORO_MODES.POMODORO.type:
      if ((numCompletedPoms + 1) % longBreakInterval !== 0) {
        if (numCompletedShortBreaks < (numCompletedPoms + 1)) {
          return POMODORO_MODES.SHORT_BREAK.type;
        }
        return POMODORO_MODES.POMODORO.type;
      }
      return POMODORO_MODES.LONG_BREAK.type;
    case POMODORO_MODES.SHORT_BREAK.type:
    case POMODORO_MODES.LONG_BREAK.type:
    default:
      return POMODORO_MODES.POMODORO.type;
  }
};
