using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using VISCACameraController.Core;
using VISCACameraController.Models;
using VISCACameraController.Repositories.Interfaces;
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
        private SerialPort serial;
        private RelayCommand connectionCommand;
        private bool isCameraChangingPowerState;
        private ViscaCommands viscaCommands;
        private RelayCommand<string> callPresetCommand;
        private List<FocusMode> focusModes;
        private FocusMode selectedFocusMode;
        private int panAndTiltSpeed;
        private bool isHiddenSetPresetButtonEnabled;
        private RelayCommand setPresetCommand;
        private List<string> adjustablePresets;
        private string selectedAdjustablePreset;
        private ISettingsRepository settingsRepository;

        #endregion

        public ControllerPageViewModel(ISettingsRepository settingsRepository)
        {
            this.settingsRepository = settingsRepository;

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
            get => GetSelectedComPort();
            set
            {
                settingsRepository.DefaultComPort = value;
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

        public List<FocusMode> FocusModes
        {
            get => focusModes;
            set
            {
                SetProperty(ref focusModes, value);
            }
        }

        public FocusMode SelectedFocusMode
        {
            get => selectedFocusMode;
            set
            {
                SetProperty(ref selectedFocusMode, value);
                OnPropertyChanged(nameof(IsAutoFocusModeEnabled));
                SendCommandOnSerialPort(value.Mode == Models.FocusModes.Auto ? viscaCommands.SetAutoFocus : viscaCommands.SetManualFocus);
            }
        }

        public int PanAndTiltSpeed
        {
            get => panAndTiltSpeed;
            set
            {
                SetProperty(ref panAndTiltSpeed, value);
            }
        }

        public bool IsHiddenSetPresetButtonEnabled
        {
            get => isHiddenSetPresetButtonEnabled;
            set
            {
                SetProperty(ref isHiddenSetPresetButtonEnabled, value);
            }
        }

        public List<string> AdjustablePresets
        {
            get => adjustablePresets;
            set
            {
                SetProperty(ref adjustablePresets, value);
            }
        }

        public string SelectedAdjustablePreset
        {
            get => selectedAdjustablePreset;
            set
            {
                SetProperty(ref selectedAdjustablePreset, value);
            }
        }

        public bool IsAutoFocusModeEnabled => SelectedFocusMode.Mode == Models.FocusModes.Auto;

        public string ConnectionButtonContent => LocalizedStrings.GetString(IsConnected ? "ConnectionButton_DisconnectionLabel" : "ConnectionButton_ConnectionLabel");

        public bool IsCameraPowerToogleSwitchEnabled => IsConnected && !IsCameraChangingPowerState;

        public RelayCommand ConnectionCommand
            => connectionCommand ?? (connectionCommand = new RelayCommand(() => Connect()));

        public RelayCommand<string> CallPresetCommand
            => callPresetCommand ?? (callPresetCommand = new RelayCommand<string>((string param) => CallPreset(param)));

        public RelayCommand SetPresetCommand
            => setPresetCommand ?? (setPresetCommand = new RelayCommand(() => SetPreset()));

        #endregion

        #region Publics methods

        public async void CameraPowerToogleSwitchToggled(bool isCameraPowerToogleSwitchOn)
        {
            SendCommandOnSerialPort(isCameraPowerToogleSwitchOn ? viscaCommands.SwitchOnCamera : viscaCommands.SwitchOffCamera);

            IsCameraChangingPowerState = true;
            await Task.Delay(isCameraPowerToogleSwitchOn ? CAMERA_TIME_TO_START : CAMERA_TIME_TO_STOP);
            IsCameraChangingPowerState = false;
        }

        public void ZoomInCommand() => SendCommandOnSerialPort(viscaCommands.ZoomIn);

        public void ZoomOutCommand() => SendCommandOnSerialPort(viscaCommands.ZoomOut);

        public void StopZoomCommand() => SendCommandOnSerialPort(viscaCommands.StopZoomMove);

        public void FocusFurtherCommand() => SendCommandOnSerialPort(viscaCommands.FocusFurther);

        public void FocusNearerCommand() => SendCommandOnSerialPort(viscaCommands.FocusNearer);

        public void StopFocusCommand() => SendCommandOnSerialPort(viscaCommands.StopFocusMove);

        public void MoveRightPanCommand() => SendCommandOnSerialPort(TiltPanViscaCommandBuilder(viscaCommands.RightPanMove));

        public void MoveLeftPanCommand() => SendCommandOnSerialPort(TiltPanViscaCommandBuilder(viscaCommands.LeftPanMove));

        public void MoveBottomTiltCommand() => SendCommandOnSerialPort(TiltPanViscaCommandBuilder(viscaCommands.BottomTiltMove));

        public void MoveTopTiltCommand() => SendCommandOnSerialPort(TiltPanViscaCommandBuilder(viscaCommands.TopTiltMove));

        public void StopTiltPanMoveCommand() => SendCommandOnSerialPort(viscaCommands.StopTiltPanMove);

        #endregion

        #region Privates methods

        private void Initialize()
        {
            isConnected = false;
            comPorts = new List<string>(SerialPort.GetPortNames());
            focusModes = new List<FocusMode>();
            foreach (FocusModes focusMode in Enum.GetValues(typeof(FocusModes)))
            {
                focusModes.Add(new FocusMode() { Mode = focusMode, Label = LocalizedStrings.GetString($"{focusMode}FocusModeLabel") });
            }
            selectedFocusMode = focusModes.Count > 0 ? focusModes.First() : null;
            serial = new SerialPort();
            isCameraChangingPowerState = false;
            panAndTiltSpeed = 1;
            isHiddenSetPresetButtonEnabled = false;
            adjustablePresets = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" };

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

        private void CallPreset(string presetNumber)
        {
            SendCommandOnSerialPort(viscaCommands.GoToPreset.Replace("{P}", presetNumber));
        }

        private void SetPreset()
        {
            if (!String.IsNullOrEmpty(SelectedAdjustablePreset))
            {
                SendCommandOnSerialPort(viscaCommands.SetPreset.Replace("{P}", SelectedAdjustablePreset));
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

        private string TiltPanViscaCommandBuilder(string command) => new StringBuilder(command).Replace("{S}", PanAndTiltSpeed.ToString("D2")).ToString();

        private string GetSelectedComPort()
        {
            var defaultComPort = settingsRepository.DefaultComPort;
            var isDefaultComPortAvailable = !string.IsNullOrEmpty(defaultComPort) && comPorts.IndexOf(defaultComPort) > -1;
            return isDefaultComPortAvailable
                ? defaultComPort
                : comPorts.Count > 0
                    ? comPorts.First()
                    : string.Empty;
        }

        #endregion
    }
}
