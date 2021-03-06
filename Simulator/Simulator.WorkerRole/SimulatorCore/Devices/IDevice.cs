﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Models;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Simulator.WorkerRole.SimulatorCore.Telemetry;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.Simulator.WorkerRole.SimulatorCore.Devices
{
    /// <summary>
    /// Represents a device. Implementors may be written in managed code, or a managed wrapper
    /// around a native (C/C++) core.
    /// </summary>
    public interface IDevice
    {
        string DeviceID { get; set; }

        string HostName { get; set; }

        string PrimaryAuthKey { get; set; }

        dynamic DeviceProperties { get; set; }

        dynamic Commands { get; set; }

        List<ITelemetry> TelemetryEvents { get; }

        bool RepeatEventListForever { get; set; }

        void Init(InitialDeviceConfig config);


        Task SendDeviceInfo();

        dynamic GetDeviceInfo();

        Task StartAsync(CancellationToken token);
    }
}
