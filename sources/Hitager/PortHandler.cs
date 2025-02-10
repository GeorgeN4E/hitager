using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Hitager
{
    public class PortHandler
    {
        static SerialPort _serialPort;
        private string portName = "";
        bool initDone = false;
        TextBox debugTextBox;

        public event EventHandler debugUpdated;

        /// <summary>
        /// Sets the port name.
        /// </summary>
        /// <param name="port">The port name to set.</param>
        public void setPort(string port)
        {
            portName = port;
        }

        /// <summary>
        /// Opens the serial port and performs initialization if not already done.
        /// </summary>
        /// <returns>True if the port is open; otherwise, false.</returns>
        private bool portOpen()
        {
            if (_serialPort == null)
            {
                _serialPort = new SerialPort();
            }

            if (!_serialPort.IsOpen)
            {
                _serialPort.BaudRate = 115200;
                _serialPort.PortName = portName;
                try
                {
                    _serialPort.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                _serialPort.NewLine = "\n";
                _serialPort.ReadTimeout = 2000;

                // Temporary buffer (retained from original code)
                byte[] tmpBuffer = new byte[1000];

                if (!initDone)
                {
                    handleDebug("Tag reader communication init...");

                    _serialPort.DiscardInBuffer();
                    handleDebug("Discarded in buffer");
                    _serialPort.DiscardOutBuffer();
                    handleDebug("Discarded out buffer");

                    _serialPort.Write("f");
                    Thread.Sleep(2000);

                    // Loop to read any pending data
                    for (int i = 0; i < 20; i++)
                    {
                        handleDebug("i= " + i);
                        string indata = _serialPort.ReadExisting();
                        // Uncomment the following line if you wish to log indata:
                        // handleDebug(indata);
                        Thread.Sleep(50);
                    }

                    _serialPort.Write("f");
                    Thread.Sleep(500);

                    string indata3 = _serialPort.ReadExisting();
                    handleDebug(indata3);

                    initDone = true;
                    if (portWR("v").Contains("TIMEOUT"))
                    {
                        initDone = false;
                        return false;
                    }
                    initDone = true;
                    handleDebug("Done");
                }
            }
            return true;
        }

        /// <summary>
        /// Raises the debugUpdated event.
        /// </summary>
        /// <param name="debug">The debug message.</param>
        private void DebugUpdateRaiseEvent(string debug)
        {
            DebugmessageEventArgs e = new DebugmessageEventArgs
            {
                Message = debug
            };

            // Using the null-conditional operator to safely invoke the event
            debugUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Sends a debug message to be logged.
        /// </summary>
        /// <param name="text">The text to log.</param>
        private void handleDebug(String text)
        {
            DebugUpdateRaiseEvent(text + Environment.NewLine);
        }

        /// <summary>
        /// Sets the TextBox used for debugging.
        /// </summary>
        /// <param name="textbox">Reference to the TextBox.</param>
        public void setDebug(ref TextBox textbox)
        {
            debugTextBox = textbox;
        }

        /// <summary>
        /// Closes the serial port.
        /// </summary>
        private void portClose()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        /// <summary>
        /// Writes a command to the serial port and reads the response.
        /// </summary>
        /// <param name="cmd">The command to send.</param>
        /// <returns>The response data, or an error message.</returns>
        public string portWR(String cmd)
        {
            handleDebug("Sending: " + cmd);
            String data = "";
            if (!portOpen())
                return "ERROR";

            // Loop runs once as in the original code.
            for (int i = 0; i < 1; i++)
            {
                String received = "";
                _serialPort.Write(cmd);

                // Keep reading until "EOF" is encountered.
                while (!received.Contains("EOF"))
                {
                    try
                    {
                        received = _serialPort.ReadLine();
                    }
                    catch (Exception)
                    {
                        handleDebug("Please reset reader!");
                        portClose();
                        return "TIMEOUT";
                    }
                    handleDebug(received);

                    if (received.Contains("RESP") && (!received.Contains("ERROR")) && (!received.Contains("NORESP")))
                    {
                        data = received.Substring(5);
                        // Break the outer loop by setting i to a value >= 1.
                        i = 5;
                    }
                    else if (received.Contains("RFON"))
                    {
                        Thread.Sleep(500);
                        return "OK";
                    }
                    else if (received.Contains("RFOFF"))
                    {
                        portClose();
                        return "OK";
                    }
                }
            }
            return data;
        }
    }
}
