using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SerialConnect : MonoBehaviour
{
    [SerializeField] private string debugPortName;
    [SerializeField] private bool useDebugPort;
    public int LedCount;
    public Dropdown PortSelector;
    private List<string> ports;
    private SerialPort activePort;
    public static SerialConnect instance;
    public delegate void ButtonEventDelegate(int index, bool state);
    public ButtonEventDelegate ButtonEvent;

    private void Start()
    {
        instance = this;
        RefreshPortsDropdown();

        if (useDebugPort)
        {
            ConnectToPort();
        }

    }

    private void Update()
    {
        if(activePort != null && activePort.BytesToRead > 0)
        {
            HandleData((byte)activePort.ReadByte());
        }
    }

    public void SwitchLed(int index, bool state)
    {
        if (activePort == null || index < LedCount) { return; }

        byte stateInt = 0;
        if (state) { stateInt = 1; }

        int command = index & ~(1 << 7) | (stateInt << (byte)7);

        SendByte((byte)command);

    }

    public void SendByte(byte data)
    {
        if (activePort == null) { return; }

        byte[] buffer = new byte[1];
        buffer[0] = data;
        activePort.Write(buffer, 0, 1);
    }

    public void RefreshPortsDropdown()
    {
        if(PortSelector == null) { return; }
        PortSelector.ClearOptions();

        string[] portNames = SerialPort.GetPortNames();
        ports = portNames.ToList();

        PortSelector.AddOptions(ports);
    }

    public void ConnectToPort()
    {
        string portName;
        if (useDebugPort) { portName = debugPortName; }
        else { portName = ports[PortSelector.value]; }

        activePort = new SerialPort(portName, 9600);

        activePort.Open();
        activePort.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);
        
        Debug.Log($"Connected to {portName}");

    }

    public void Disconnect()
    {
        if (activePort != null)
        {
            if (activePort.IsOpen) { activePort.Close(); }

            activePort.Dispose();
            Debug.Log("Disconnected");
        }
    }

    private void ReceiveData(object sender, SerialDataReceivedEventArgs e)
    {
        Debug.Log("Received Arduino data");
        byte[] data = new byte[1];
        activePort.Read(data, 0, 1);
    }

    private void HandleData(byte data)
    {
        int index = data & ~(1 << 7);
        int stateInt = data >> 7;
        bool state = false;
        if (stateInt == 1) { state = true; }
        ButtonEvent?.Invoke(index, state);
        Debug.Log($"Button {index} state change: {state}");
    }

    private void OnDestroy()
    {
        Disconnect();
    }
}
