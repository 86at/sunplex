using System;
using System.Timers;

namespace Library
{
    public class Bank : BankData
    {
        private Timer _timer;
        public Bank() : base()
        {
            Init();
        }

        public Bank(int pin, int cabin, string model) : base()
        {
            Pin = pin;
            Cabin = cabin;
            Model = model;
            Init();
        }

        private void Init()
        {
            _timer = new Timer {Interval = 1000, AutoReset = true};
            _timer.Elapsed += Step;
        }
        

        private void Step(object sender, ElapsedEventArgs e)
        {
            switch (State)
            {
                case BankState.DELAY:
                    DelayAct = DateTime.Now - DelayStarted;
                    if (DelayAct >= Delay)
                    {
                        RunningStarted = DateTime.Now;
                        State = BankState.RUNNING;
                        DelayAct = Delay;
                    }

                    break;
                case BankState.RUNNING:
                    RunningAct = DateTime.Now - RunningStarted;
                    if (RunningAct >= Running)
                    {
                        CoolingStarted = DateTime.Now;
                        State = BankState.COOLING;
                        RunningAct = Running;
                        Total += Running;
                    }

                    break;
                case BankState.COOLING:
                    CoolingAct = DateTime.Now - CoolingStarted;
                    if (CoolingAct >= Cooling)
                    {
                        _timer.Stop();
                        RunCount++;
                        DelayAct = RunningAct = CoolingAct = TimeSpan.Zero;
                        State = BankState.OFF;
                    }

                    break;
            }
        }

        public bool Start()
        {
            if (State != BankState.OFF) return false;
            DelayStarted = DateTime.Now;
            State = BankState.DELAY;
            StartCount++;
            _timer.Start();
            return true;
        }

        public bool Skip()
        {
            if (State != BankState.DELAY) return false;
            RunningStarted = DateTime.Now;
            DelayAct = Delay;
            State = BankState.RUNNING;
            SkipCount++;
            return true;
        }

        public bool Abort()
        {
            if (!IsRunning) return false;
            switch (State)
            {
                case BankState.DELAY:
                    State = BankState.OFF;
                    break;
                case BankState.RUNNING:
                    State = BankState.DIRTY;
                    Total += RunningAct;
                    break;
                case BankState.COOLING:
                    State = BankState.DIRTY;
                    break;
            }

            AbortCount++;
            DelayAct = RunningAct = CoolingAct = TimeSpan.Zero;
            return true;
        }

        public bool Clean()
        {
            if (State == BankState.DIRTY)
            {
                CleanCount++;
                State = BankState.OFF;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetDelay(TimeSpan delay)
        {
            if (IsRunning)
            {
                return false;
            }

            Delay = delay;
            return true;
        }

        public bool SetRunning(TimeSpan delay)
        {
            if (IsRunning)
            {
                return false;
            }

            Running = delay;
            return true;
        }
    }
}