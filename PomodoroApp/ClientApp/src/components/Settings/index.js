import React, { useCallback, useEffect, useRef, useState } from 'react';
import { useSelector } from 'react-redux';
import { selectConfigs } from '../../store/selectors/pomodoroSelector';
import { initialState } from '../../store/slices/pomodoroSlice';
import ToggleSwitch from '../ToggleSwitch';
import './styles.scss';

const INPUT_NAMES = Object.freeze({
  TIME_POMODORO: 'time-pomodoro',
  TIME_SHORT_BREAK: 'time-short-break',
  TIME_LONG_BREAK: 'time-long-break',
  AUTO_START_POMODOROS: 'auto-start-pomodoros',
  AUTO_START_BREAKS: 'auto-start-breaks',
  LONG_BREAK_INTERVAL: 'long-break-interval',
});

const Settings = () => {
  const form = useRef();
  const [disabled, setDisabled] = useState(true);
  const configs = useSelector(selectConfigs);

  const setFormData = useCallback((configs) => {
    if (form?.current?.elements) {
      const formElements = form.current.elements;
      const defaultConfigs = initialState.configs;
      formElements[INPUT_NAMES.TIME_POMODORO].value
        = configs.pomodoroLength || defaultConfigs.pomodoroLength;
      formElements[INPUT_NAMES.TIME_SHORT_BREAK].value
        = configs.shortBreakLength || defaultConfigs.shortBreakLength;
      formElements[INPUT_NAMES.TIME_LONG_BREAK].value
        = configs.longBreakLength || defaultConfigs.longBreakLength;
      formElements[INPUT_NAMES.AUTO_START_POMODOROS].checked
        = configs.autoStartPom || defaultConfigs.autoStartPom;
      formElements[INPUT_NAMES.AUTO_START_BREAKS].checked
        = configs.autoStartBreak || defaultConfigs.autoStartBreak;
      formElements[INPUT_NAMES.LONG_BREAK_INTERVAL].value
        = configs.longBreakInterval || defaultConfigs.longBreakInterval;
    }
  }, []);

  useEffect(() => {
    configs && setFormData(configs);
  }, [configs, setFormData]);

  const handleSubmit = useCallback((e) => {
    const timePomodoro = e.target[INPUT_NAMES.TIME_POMODORO].value;
    const timeShortBreak = e.target[INPUT_NAMES.TIME_SHORT_BREAK].value;
    const timeLongBreak = e.target[INPUT_NAMES.TIME_LONG_BREAK].value;
    const autoStartPomodoros = e.target[INPUT_NAMES.AUTO_START_POMODOROS].checked;
    const autoStartBreaks = e.target[INPUT_NAMES.AUTO_START_BREAKS].checked;
    const longBreakInterval = e.target[INPUT_NAMES.LONG_BREAK_INTERVAL].value;

    e.preventDefault();
  }, []);

  const onFormChange = useCallback((_) => {
    // Validate if all number inputs are greater than 0
    let isValid = true;
    for (const name of Object.values(INPUT_NAMES)) {
      const value = form?.current?.elements[name]?.value;
      if (name !== INPUT_NAMES.AUTO_START_POMODOROS
          && name !== INPUT_NAMES.AUTO_START_BREAKS) {
        if (!value || value < 1) {
          isValid = false;
          break;
        }
      }
    }
    // Validate if any value is updated and different from the server data
    let isDifferent = false;
    for (const name of Object.values(INPUT_NAMES)) {
      if (name === INPUT_NAMES.TIME_POMODORO) {
        isDifferent = form?.current?.elements[name]?.value !== configs.pomodoroLength.toString(10);
      } else if (name === INPUT_NAMES.TIME_SHORT_BREAK) {
        isDifferent = form?.current?.elements[name]?.value !== configs.shortBreakLength.toString(10);
      } else if (name === INPUT_NAMES.TIME_LONG_BREAK) {
        isDifferent = form?.current?.elements[name]?.value !== configs.longBreakLength.toString(10);
      } else if (name === INPUT_NAMES.AUTO_START_POMODOROS) {
        isDifferent = form?.current?.elements[name]?.checked !== configs.autoStartPom;
      } else if (name === INPUT_NAMES.AUTO_START_BREAKS) {
        isDifferent = form?.current?.elements[name]?.checked !== configs.autoStartBreak;
      } else if (name === INPUT_NAMES.LONG_BREAK_INTERVAL) {
        isDifferent = form?.current?.elements[name]?.value !== configs.longBreakInterval.toString(10);
      }
      if (isDifferent) {
        break;
      }
    }
    if (isValid && isDifferent) {
      setDisabled(false);
    } else {
      setDisabled(true);
    }
  }, [configs]);

  return (
    <form ref={form}
          className="settings__wrapper"
          onSubmit={handleSubmit}
          onChange={onFormChange}>
      <div className="settings__title">Timer Settings</div>
      <div className="settings__divider--horizontal" />
      <div className="settings__section__title">Time (minutes)</div>
      <div className="settings__section__time__wrapper">
        <div>Pomodoro</div>
        <input type="number"
               name={INPUT_NAMES.TIME_POMODORO}
               min={1} />
      </div>
      <div className="settings__section__time__wrapper">
        <div>Short Break</div>
        <input type="number"
               name={INPUT_NAMES.TIME_SHORT_BREAK}
               min={1} />
      </div>
      <div className="settings__section__time__wrapper">
        <div>Long Break</div>
        <input type="number"
               name={INPUT_NAMES.TIME_LONG_BREAK}
               min={1} />
      </div>
      <div className="settings__divider--horizontal" />
      <div className="settings__section__title">Auto start</div>
      <div className="settings__section__auto__wrapper">
        <div>Pomodoros</div>
        <ToggleSwitch name={INPUT_NAMES.AUTO_START_POMODOROS} />
      </div>
      <div className="settings__section__auto__wrapper">
        <div>Breaks</div>
        <ToggleSwitch name={INPUT_NAMES.AUTO_START_BREAKS} />
      </div>
      <div className="settings__divider--horizontal" />
      <div className="settings__section__interval__wrapper">
        <div className="settings__section__title">Long Break interval</div>
        <input type="number"
               name={INPUT_NAMES.LONG_BREAK_INTERVAL}
               min={1} />
      </div>
      <div className="settings__section__save__wrapper">
        <input type="submit"
               value="Save"
               disabled={disabled} />
      </div>
    </form>
  );
};

export default Settings;
