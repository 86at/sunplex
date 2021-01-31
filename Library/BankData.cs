using System;

namespace Library
{
    public class BankData
    {
        public int Id { get; set; }
        public int Pin { get; set; }
        public int Cabin { get; set; }
        public string Model { get; set; }
        public int StartCount { get; set; } = 0;
        public int SkipCount { get; set; } = 0;
        public int AbortCount { get; set; } = 0;
        public int CleanCount { get; set; } = 0;
        public int RunCount { get; set; } = 0;
        public BankState State { get; set; } = BankState.OFF;
        public DateTime Created { get; set; }
        public TimeSpan Total { get; set; } = TimeSpan.FromMinutes(0);
        public TimeSpan Delay { get; set; } = TimeSpan.FromMinutes(5);
        public TimeSpan DelayMin { get; set; } = TimeSpan.FromMinutes(1);
        public TimeSpan DelayMax { get; set; } = TimeSpan.FromMinutes(15);
        public TimeSpan DelayAct { get; set; } = TimeSpan.FromMinutes(0);
        public TimeSpan Running { get; set; } = TimeSpan.FromMinutes(10);
        public TimeSpan RunningMin { get; set; } = TimeSpan.FromMinutes(5);
        public TimeSpan RunningMax { get; set; } = TimeSpan.FromMinutes(30);
        public TimeSpan RunningAct { get; set; } = TimeSpan.FromMinutes(0);
        public TimeSpan Cooling { get; set; } = TimeSpan.FromMinutes(3);
        public TimeSpan CoolingAct { get; set; } = TimeSpan.FromMinutes(0);
        public bool IsRunning => State != BankState.OFF && State != BankState.DIRTY;
        public DateTime DelayStarted { get; set; } = DateTime.MinValue;
        public DateTime RunningStarted { get; set; } = DateTime.MinValue;
        public DateTime CoolingStarted { get; set; } = DateTime.MinValue;
    }
}