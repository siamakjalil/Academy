﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ILog:IDisposable
    {
        Task ErrorLog(string msg, string exp, string path);
    }
}