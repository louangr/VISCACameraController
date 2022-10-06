using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using VISCACameraController.Core;
using VISCACameraController.Models;
using VISCACameraController.Strings;
using VISCACameraController.Utils;

namespace VISCACameraController.Views
{
    public class ControllerPageViewModel : CorePageViewModel
    {
        #region Privates fields

        private const int CAMERA_TIME_TO_START = 15000;
        private const int CAMERA_TIME_TO_STOP = 3000;

        private bool isConnected;
        private List<string> comPorts;
        private string selectedComPort;
        private SerialPort serial;
        private RelayCommand connectionCommand;
        private bool isCameraChangingPowerState;
        private ViscaCommands viscaCommands;

        #endregion

        public ControllerPageViewModel()
        {
            Initialize();
        }

        #region Properties

        public bool IsConnected
        {
            get => isConnected;
            set
            {
                SetProperty(ref isConnected, value);
                OnPropertyChanged(nameof(ConnectionButtonContent));
                OnPropertyChanged(nameof(IsCameraPowerToogleSwitchEnabled));
            }
        }

        public List<string> ComPorts
        {
            get => comPorts;
            set
            {
                SetProperty(ref comPorts, value);
            }
        }

        public string SelectedComPort
        {
            get => selectedComPort;
            set
            {
                SetProperty(ref selectedComPort, value);
            }
        }

        public bool IsCameraChangingPowerState
        {
            get => isCameraChangingPowerState;
            set
            {
                SetProperty(ref isCameraChangingPowerState, value);
                OnPropertyChanged(nameof(IsCameraPowerToogleSwitchEnabled));
            }
        }

        public string ConnectionButtonContent => LocalizedStrings.GetString(IsConnected ? "ConnectionButton_DisconnectionLabel" : "ConnectionButton_ConnectionLabel");

        public bool IsCameraPowerToogleSwitchEnabled => IsConnected && !IsCameraChangingPowerState;

        public RelayCommand ConnectionCommand
            => connectionCommand ?? (connectionCommand = new RelayCommand(() => Connect()));

        #endregion

        #region Publics methods

        public async void CameraPowerToogleSwitchToggled(bool isCameraPowerToogleSwitchOn)
        {
            SendCommandOnSerialPort(isCameraPowerToogleSwitchOn ? viscaCommands.SwitchOnCamera : viscaCommands.SwitchOffCamera);

            IsCameraChangingPowerState = true;
            await Task.Delay(isCameraPowerToogleSwitchOn ? CAMERA_TIME_TO_START : CAMERA_TIME_TO_STOP);
            IsCameraChangingPowerState = false;
        }

        #endregion

        #region Privates methods

        private void Initialize()
        {
            isConnected = false;
            comPorts = new List<string>(SerialPort.GetPortNames());
            selectedComPort = comPorts.Count > 0 ? comPorts.First() : string.Empty;
            serial = new SerialPort();
            isCameraChangingPowerState = false;

            InitializeViscaCommands();
        }

        private void InitializeViscaCommands()
        {
            string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "viscaCommands.json");
            using (StreamReader r = new StreamReader(jsonFile))
            {
                string jsonString = r.ReadToEnd();
                viscaCommands = JsonConvert.DeserializeObject<ViscaCommands>(jsonString);
            }
        }

        private void Connect()
        {
            try
            {
                if (!serial.IsOpen)
                {
                    serial.PortName = SelectedComPort;
                    serial.BaudRate = 9600;
                    serial.Handshake = Handshake.None;
                    serial.Parity = Parity.None;
                    serial.DataBits = 8;
                    serial.StopBits = StopBits.Two;
                    serial.ReadTimeout = 200;
                    serial.WriteTimeout = 50;
                    serial.Open();

                    IsConnected = true;
                }
                else
                {
                    serial.Close();
                    IsConnected = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void SendCommandOnSerialPort(string command)
        {
            try
            {
                byte[] hexstring = HexaConverter.ConvertHexaStringToByteArray(command);
                serial.Write(hexstring, 0, hexstring.Length);
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
