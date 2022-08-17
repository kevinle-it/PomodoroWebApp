import { useEffect, useState } from 'react';

const useCountdown = (props) => {
  const { initialMinutes = 25, initialSeconds = 0, shouldStartImmediately = false } = props;

  const [minutes, setMinutes] = useState(initialMinutes);
  const [seconds, setSeconds] = useState(initialSeconds);
  const [shouldRun, setShouldRun] = useState(shouldStartImmediately);

  useEffect(() => {
    let timer = setTimeout(() => {
      if (seconds > 0) {
        setSeconds(seconds - 1);
      } else if (minutes > 0) {
        setSeconds(59);
        setMinutes(minutes - 1);
      }
      if (minutes === 0 && seconds - 1 === 0) {
        setShouldRun(!shouldRun);
      }
    }, shouldRun ? 1000 : 0);
    if (!shouldRun) {
      clearTimeout(timer);
    }
    return () => {
      clearTimeout(timer);
    };
  }, [minutes, seconds, shouldRun]);

  return {
    minutes, setMinutes,
    seconds, setSeconds,
    shouldRun, setShouldRun,
  };
};

export default useCountdown;
