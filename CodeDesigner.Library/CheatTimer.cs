using CodeDesigner.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library
{
    public class CheatTimer
    {
        public Cheat Cheat { get; set; }
        private CheatTimerIntervalType _cheatTimerIntervalType { get; set; }
        private DateTime _intervalDateTime { get; set; }
        private DateTime _currentDateTime { get; set; }
        private int _interval { get; set; }

        public CheatTimer(Cheat cheat, CheatTimerIntervalType cheatTimerIntervalType, int interval)
        {
            Cheat = cheat;
            _interval = interval;
            _cheatTimerIntervalType = cheatTimerIntervalType;
            SetCheatTimer();
        }

        public void SetCheatTimer()
        {
            _currentDateTime = DateTime.Now;

            switch (_cheatTimerIntervalType)
            {
                case CheatTimerIntervalType.Ticks:
                    _intervalDateTime = _currentDateTime.AddTicks(_interval);
                    break;
                case CheatTimerIntervalType.Seconds:
                    _intervalDateTime = _currentDateTime.AddSeconds(_interval);
                    break;
                case CheatTimerIntervalType.Minutes:
                    _intervalDateTime = _currentDateTime.AddMinutes(_interval);
                    break;
            }
        }

        public bool IsIntervalCriteriaMet()
        {
            if (_currentDateTime < _intervalDateTime)
            {
                SetCheatTimer();
                return true;
            }
            return false;
        }
    }
}
