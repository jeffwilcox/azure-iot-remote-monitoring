using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.DeviceSchema;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Models;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Factory
{
    public static class SampleDeviceFactory
    {
        public const string OBJECT_TYPE_DEVICE_INFO = "DeviceInfo";

        public const string VERSION_1_0 = "1.0";

        private const int MAX_COMMANDS_SUPPORTED = 6;

        private const bool IS_SIMULATED_DEVICE = true;

        private static List<string> DefaultDeviceNames = new List<string>{
            "N1172FJ-1", 
            "N1172FJ-2", 
            "N2172FJ-1", 
            "N2172FJ-2"
        };

        public static dynamic GetSampleSimulatedDevice(string deviceId, string key)
        {
            dynamic device = DeviceSchemaHelper.BuildDeviceStructure(deviceId, true);

            AssignDeviceProperties(deviceId, device);
            device.ObjectType = OBJECT_TYPE_DEVICE_INFO;
            device.Version = VERSION_1_0;
            device.IsSimulatedDevice = IS_SIMULATED_DEVICE;

            AssignCommands(device);

            return device;
        }

        private static void AssignDeviceProperties(string deviceId, dynamic device)
        {
            dynamic deviceProperties = DeviceSchemaHelper.GetDeviceProperties(device);
            deviceProperties.HubEnabledState = true;
            deviceProperties.Manufacturer = "Contoso Inc.";
            deviceProperties.ModelNumber = "MD-" + GetIntBasedOnString(deviceId + "ModelNumber", 1000);
            deviceProperties.SerialNumber = "SER" + GetIntBasedOnString(deviceId + "SerialNumber", 10000);
            deviceProperties.FirmwareVersion = "1." + GetIntBasedOnString(deviceId + "FirmwareVersion", 100);
            deviceProperties.Platform = "Plat-" + GetIntBasedOnString(deviceId + "Platform", 100);
            deviceProperties.Processor = "i3-" + GetIntBasedOnString(deviceId + "Processor", 10000);
            deviceProperties.InstalledRAM = GetIntBasedOnString(deviceId + "InstalledRAM", 100) + " MB";
        }

        private static int GetIntBasedOnString(string input, int maxValueExclusive)
        {
            int hash = input.GetHashCode();

            //Keep the result positive
            if(hash < 0)
            {
                hash = -hash;
            }

            return hash % maxValueExclusive;
        }

        private static void AssignCommands(dynamic device)
        {
            dynamic command = CommandSchemaHelper.CreateNewCommand("PingDevice");
            CommandSchemaHelper.AddCommandToDevice(device, command);
            
            command = CommandSchemaHelper.CreateNewCommand("StartTelemetry");
            CommandSchemaHelper.AddCommandToDevice(device, command);
            
            command = CommandSchemaHelper.CreateNewCommand("StopTelemetry");
            CommandSchemaHelper.AddCommandToDevice(device, command);
            
            command = CommandSchemaHelper.CreateNewCommand("ChangeDeviceState");
            CommandSchemaHelper.DefineNewParameterOnCommand(command, "DeviceState", "string");
            CommandSchemaHelper.AddCommandToDevice(device, command);
        }

        public static List<string> GetDefaultDeviceNames()
        {
            return DefaultDeviceNames;
        }
    }
}
