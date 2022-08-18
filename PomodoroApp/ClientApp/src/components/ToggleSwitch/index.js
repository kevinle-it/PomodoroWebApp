import React, { useCallback } from 'react';
import './styles.scss';

const ToggleSwitch = (props) => {
  const { name, onChange = () => {} } = props;

  const onCheckedChange = useCallback((e) => {
    onChange(e.target.name, e.target.checked);
  }, [onChange]);

  return (
    <label className="toggle-switch__wrapper">
      <input type="checkbox"
             name={name}
             onChange={onCheckedChange}
             hidden />
      <div className="toggle-switch__slider--rounded" />
    </label>
  );
};

export default ToggleSwitch;
