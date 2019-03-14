﻿using System;
using System.Collections.Generic;

namespace ElipModel.Entity
{
    [Serializable]
    public class Lab
    {
        public string LabName { get; set; }
        public Dictionary<int, string> TaskList { get; set; }

        public Lab()
        {
            TaskList = new Dictionary<int, string>();
        }
    }
}