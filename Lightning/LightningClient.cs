using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Lightning
{
    public class LightningClient
    {
        readonly UnixDomainSocketEndPoint unixDomainSocketEndPoint = new UnixDomainSocketEndPoint(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
            "/.lightning/lightning-rpc"
        );

        public LightningClient()
        {
            string result = SocketSendReceive("getinfo");
            Log.Information(result);
        }
        public string SocketSendReceive(string request)
        {
            // Convert request to the proper format
            request = CreateRequest(request);

            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            try
            {
                // Create a Unix domain socket.
                Socket s = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

                // Connect the socket to the endpoint. Catch any errors.
                try
                {
                    s.Connect(unixDomainSocketEndPoint);
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

            return null;
        }

        private string CreateRequest(string cmd)
        {
            JObject o = new JObject()
            {
                { "method", cmd },
                { "id", "Bitar-API"},
                { "params", "[]" }
            };
            return o.ToString();
        }
    }
}