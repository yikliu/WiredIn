﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Analyzer;
namespace WiredIn.UserActivity
{
    public class ShutDown : Activity
    {
        public ShutDown(DateTime time)
            : base(time)
        {
        }

        public override String What()
        {
            return "ShutDown";
        }

        public override String getPreviousACDuration()
        {
            TimeSpan span = this.When() - WindowChangeActivity.LAST_WINDOW_CHANGE;
            return ", , , " + span.TotalSeconds.ToString();
        }

        public override void Accept(Worker j)
        {
            j.CatchShutDownActivity(this);
        }
    }
}