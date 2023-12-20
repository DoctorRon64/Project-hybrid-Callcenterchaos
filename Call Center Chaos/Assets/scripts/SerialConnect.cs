using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class SerialConnect : MonoBehaviour
{
    public Dropdown PortSelector;
    private List<string> ports;
    private SerialPort activePort;

    private void Start()
    {
        RefreshPortsDropdown();
    }

    public void SendByte(byte data)
    {
        if(activePort == null) { return; }

        byte[] buffer = new byte[1];
        buffer[0] = data;
        activePort.Write(buffer, 0, 1);
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

        activePort.Open();
        activePort.DataReceived += ReceiveData;
        Debug.Log($"Connected to {portName}");

    }

    public void Disconnect()
    {
        if (activePort != null)
        {
            if(activePort.IsOpen) {  activePort.Close(); }

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

    }

    private void OnDestroy()
    {
        Disconnect();
    }
}
