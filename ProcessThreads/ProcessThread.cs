﻿using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace AZI.ProcessThreads
{
    /// <summary>
    /// Process Thread information
    /// </summary>
    public class ProcessThread
    {
        /// <summary>
        /// Process object in which thread is started
        /// </summary>
        public readonly Process Process;

        /// <summary>
        /// Communication pipe. Can be null.
        /// </summary>
        public readonly NamedPipeServerStream Pipe;

        /// <summary>
        /// Task object for this Process Thread
        /// </summary>
        public readonly Task Task;

        /// <summary>
        /// Waitable cancellation event. Set to cancel.
        /// </summary>
        public readonly EventWaitHandle CancellationHandle;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessThread" /> class.
        /// </summary>
        /// <param name="process">Process object for Process Thread</param>
        /// <param name="pipe">Optional server-sided named pipe</param>
        /// <param name="task">Task for Process Thread</param>
        /// <param name="cancellationHandle">Handle to send cancel signal to Process Thread</param>
        internal ProcessThread(Process process, Task task, EventWaitHandle cancellationHandle, NamedPipeServerStream pipe = null)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));

            Process = process;
            Pipe = pipe;
            Task = task;
            CancellationHandle = cancellationHandle;
        }

        /// <summary>
        /// Sends cancellation to this Process Thread
        /// </summary>
        public void Cancel()
        {
            CancellationHandle.Set();
        }
    }
}