using System;
using System.Net.Sockets;
using System.Text;
using Serilog;

namespace Lightning
{
    public class LightningClient
    {

        public LightningClient()
        {
            string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string unixDomainSocketEndPoint = homeDir + "/.lightning/lightning-rpc";

            string request = "{\"method\": \"getinfo\", \"params\": [], \"id\": \"Bitar-API\"}";
            string result = SocketSendReceive(unixDomainSocketEndPoint, request);
            Log.Information(result);
        }
        public string SocketSendReceive(string unixDomainSocketEndPoint, string request)
        {
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            try
            {
                // Create a Unix domain socket.
                Socket s = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

                // Connect the socket to the endpoint. Catch any errors.
                try
                {
                    s.Connect(new UnixDomainSocketEndPoint(unixDomainSocketEndPoint));
                    Log.Information("Socket connected to {0}", s.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(request);

                    // Send the data through the socket.
                    int bytesSent = s.Send(msg);

                    // Receive the response.
                    int bytesRec = s.Receive(bytes);

                    string result = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    // Release the socket.
                    s.Shutdown(SocketShutdown.Both);
                    s.Close();

                    return result;
                }
                catch (ArgumentNullException ane)
                {
                    Log.Information("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Log.Information("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Log.Information("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Log.Information(e.ToString());
            }

            return "Is lightningd down?";
        }
    }
}