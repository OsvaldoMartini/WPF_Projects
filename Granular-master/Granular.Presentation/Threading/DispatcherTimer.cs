﻿using System;
using System.Collections.Generic;
using System.Linq;
using Granular.Extensions;

namespace System.Windows.Threading
{
    public class DispatcherTimer
    {
        public event EventHandler Tick;

        private DispatcherPriority priority;
        public DispatcherPriority Priority
        {
            get { return priority; }
            set
            {
                if (IsEnabled)
                {
                    throw new Granular.Exception("Can't change an active DispatcherTimer priority");
                }

                priority = value;
            }
        }

        private TimeSpan interval;
        public TimeSpan Interval
        {
            get { return interval; }
            set
            {
                if (IsEnabled)
                {
                    throw new Granular.Exception("Can't change an active DispatcherTimer interval");
                }

                interval = value;
            }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (isEnabled == value)
                {
                    return;
                }

                isEnabled = value;
                if (isEnabled)
                {
                    ScheduleTask();
                }
                else
                {
                    CancelTask();
                }
            }
        }

        private Dispatcher dispatcher;
        private ITaskScheduler scheduler;
        private IDisposable scheduledTask;

        public DispatcherTimer() :
            this(Dispatcher.CurrentDispatcher, ApplicationHost.Current.TaskScheduler, TimeSpan.FromSeconds(1), DispatcherPriority.Normal)
        {
            //
        }

        public DispatcherTimer(Dispatcher dispatcher, ITaskScheduler scheduler, TimeSpan interval, DispatcherPriority priority)
        {
            this.dispatcher = dispatcher;
            this.scheduler = scheduler;
            this.Interval = interval;
            this.Priority = priority;
        }

        public void Start()
        {
            IsEnabled = true;
        }

        public void Stop()
        {
            IsEnabled = false;
        }

        private void ScheduleTask()
        {
            if (!IsEnabled)
            {
                return;
            }

            scheduledTask = scheduler.ScheduleTask(Interval, () =>
            {
                dispatcher.InvokeAsync(() => Tick.Raise(this), Priority);
                ScheduleTask();
            });
        }

        private void CancelTask()
        {
            if (scheduledTask != null)
            {
                scheduledTask.Dispose();
                scheduledTask = null;
            }
        }
    }
}
