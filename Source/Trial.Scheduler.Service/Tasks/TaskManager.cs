using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Trial.Scheduler.Service.Tasks
{
    public class TaskManager : ITaskManager
    {
        public const int DefaultPeriodMilliseconds = 1000;
        private readonly ReaderWriterLockSlim _lockObj = new ReaderWriterLockSlim();
        private readonly SortedList<DateTime, ITask> _tasks = new SortedList<DateTime, ITask>();
        private Timer _timer;

        public TaskManager()
        {
            Run();
        }

        public void Run()
        {
            if (_timer != null)
                return;

            _lockObj.EnterUpgradeableReadLock();
            try
            {
                if (_timer != null)
                    return;

                _lockObj.EnterWriteLock();
                try
                {
                    _timer = new Timer(OnTimerHit, null, 0, DefaultPeriodMilliseconds);
                }
                finally
                {
                    _lockObj.ExitWriteLock();
                }
            }
            finally
            {
                _lockObj.ExitUpgradeableReadLock();
            }
        }

        public void Stop()
        {
            if (_timer == null)
                return;

            _lockObj.EnterUpgradeableReadLock();
            try
            {
                if (_timer == null)
                    return;

                _lockObj.EnterWriteLock();
                try
                {
                    _timer.Dispose();
                    _timer = null;
                }
                finally
                {
                    _lockObj.ExitWriteLock();
                }
            }
            finally
            {
                _lockObj.ExitUpgradeableReadLock();
            }
        }

        public int GetTaskCount()
        {
            _lockObj.EnterReadLock();
            try
            {
                return _tasks.Count;
            }
            finally
            {
                _lockObj.ExitReadLock();    
            }
        }

        public void Dispose()
        {
            Stop();
        }

        public void AddTask(ITask task)
        {
            _lockObj.EnterUpgradeableReadLock();
            try
            {
                if (_tasks.ContainsValue(task))
                    return;

                _lockObj.EnterWriteLock();
                try
                {
                    _tasks.Add(task.NextTime, task);
                }
                finally
                {
                    _lockObj.ExitWriteLock();
                }
            }
            finally
            {
                _lockObj.ExitUpgradeableReadLock();
            }

            task.NextTimeChanged += OnTaskNextTimeChanged;
        }

        public void RemoveTask(ITask task)
        {
            _lockObj.EnterUpgradeableReadLock();
            try
            {
                var index = _tasks.IndexOfValue(task);
                if (index == -1)
                    return;

                _lockObj.EnterWriteLock();
                _tasks.RemoveAt(index);
                _lockObj.ExitWriteLock();
            }
            finally
            {
                _lockObj.ExitUpgradeableReadLock();
            }

            task.NextTimeChanged -= OnTaskNextTimeChanged;
        }

        public IEnumerable<ITask> GetTasks()
        {
            _lockObj.EnterReadLock();
            try
            {
                return _tasks.Values.ToArray();
            }
            finally
            {
                _lockObj.ExitReadLock();
            }
        }

        private void OnTaskNextTimeChanged(object sender, EventArgs e)
        {
            var task = (ITask)sender;
            bool releaseRead = false;

            if (!_lockObj.IsUpgradeableReadLockHeld && !_lockObj.IsReadLockHeld)
            {
                _lockObj.EnterUpgradeableReadLock();
                releaseRead = true;
            }

            try
            {
                var index = _tasks.IndexOfValue(task);
                if (index == -1)
                    return;

                _lockObj.EnterWriteLock();
                try
                {
                    _tasks.RemoveAt(index);
                    _tasks.Add(task.NextTime, task);
                }
                finally
                {
                    _lockObj.ExitWriteLock();
                }
            }
            finally
            {
                if (releaseRead)
                    _lockObj.ExitUpgradeableReadLock();
            }
        }

        private void OnTimerHit(object state)
        {
            // UpdateNextTime can enter lock in write mode
            _lockObj.EnterUpgradeableReadLock();
            try
            {
                var tasksToDelete = new List<ITask>();

                var currentTime = DateTime.Now;
                for (int i = 0; i < _tasks.Count; i++)
                {
                    var task = _tasks.Values[i];

                    // all other values are later
                    if (task.NextTime > currentTime)
                        break;

                    task.Execute();
                    task.UpdateNextTime();

                    if (task.NextTime < currentTime)
                        tasksToDelete.Add(task);
                }

                if (tasksToDelete.Count > 0)
                    DeleteTasks(tasksToDelete);
            }
            finally
            {
                _lockObj.ExitUpgradeableReadLock();
            }
        }

        private void DeleteTasks(IEnumerable<ITask> tasks)
        {
            _lockObj.EnterWriteLock();
            try
            {
                foreach (var task in tasks)
                {
                    var index = _tasks.IndexOfValue(task);
                    if (index >= 0)
                        _tasks.RemoveAt(index);
                }
            }
            finally
            {
                _lockObj.ExitWriteLock();
            }
        }
    }
}