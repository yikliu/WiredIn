﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Constants;

namespace WiredIn.Analyzer
{
    /// <summary>
    /// Judge determines whether subject is on or off task
    /// </summary>
    public class Judge
    {
        //private bool onTask = false;
        
        /// <summary>
        /// determines whether subject is on or off task
        /// If current window title is on white list, return on;
        /// if not, process name is on white list, return on;
        /// if not, return false
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="winTitle"></param>
        /// <returns></returns>
        public bool checkOnTask(String procName, String winTitle)
        {
            //System.Console.WriteLine("proc:" + procName + " title" + winTitle);
            if (CheckWinTitle(winTitle))
            {
                //onTask = true;
                return true;
            }

            if(Constants.Config.WHITE_PROC.Contains(procName.ToLower()))
            {
                //onTask = true;
                return true;
            }
            //onTask = false;
            return false;
        }

        private bool CheckWinTitle(String title)
        {
            title = title.ToLowerInvariant();
            String[] words = title.Split(' ');
            
            IEnumerable<String> both = Constants.Config.WHITE_WIN.Intersect(words);

            int count = 0;
            foreach(String s in both){
                count++;
                return true;
            }
            return false;
        }
    }

}