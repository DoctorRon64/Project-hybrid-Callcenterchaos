using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SerialConnect : MonoBehaviour
{
    public byte DebugCommand;
    public int LedCount;
    public Dropdown PortSelector;
    private List<string> ports;
    private SerialPort activePort;
    public static SerialConnect instance;
    public delegate void ButtonEventDelegate(int index, bool state);
    public ButtonEventDelegate ButtonEvent;
    private UnityEvent dropdownEvent;

    private void Start()
    {
        instance = this;
        RefreshPortsDropdown();
        PortSelector.onValueChanged.AddListener(delegate { ConnectToPort(); });
        ConnectToPort();
    }

    private void Update()
    {
        if (activePort == null) { return; }

        if (activePort.BytesToRead > 0) 
        {
            byte[] data = new byte[1];
            activePort.Read(data, 0, 1);
            HandleData(data[0]);
        }
    }

    public void SwitchLed(bool state)
    {
        if (activePort == null) { return; }

        char command = '0';
        if (state) { command = '1'; }

        SendByte(command);

    }

    public void SendByte(char data)
    {
        if (activePort == null) { return; }

        char[] buffer = new char[1];
        buffer[0] = data;
        activePort.Write(buffer, 0, buffer.Length);
        Debug.Log($"Byte sent to Arduino: {buffer[0]}");
    }

    public void RefreshPortsDropdown()
    {
        PortSelector.ClearOptions();

        string[] portNames = SerialPort.GetPortNames();
        ports = portNames.ToList();

        PortSelector.AddOptions(ports);
    }

    public void ConnectToPort()
    {
        string portName = ports[PortSelector.value];
        activePort = new SerialPort(portName, 9600);

        try
        {
            activePort.Open();
            activePort.DataReceived += ReceiveData;
            Debug.Log($"Connected to {portName}");
        }
        catch(Exception e)
        {
            Debug.LogException(e);
            Debug.Log($"Couldn't connect to {portName}");
        }

    }

    public void Disconnect()
    {
        if (activePort != null)
        {
            if (activePort.IsOpen) { SwitchLed(false); activePort.Close(); }

            activePort.Dispose();
            activePort = null;
            Debug.Log("Disconnected");
        }
    }

    private void ReceiveData(object sender, SerialDataReceivedEventArgs e)
    {
        byte[] data = new byte[1];
        activePort.Read(data, 0, 1);
        HandleData(data[0]);
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

    [ContextMenu("Test value")]
    public void DebugTest()
    {
        HandleData(DebugCommand);
    }

    private void OnDestroy()
    {
        Disconnect();
    }
}
