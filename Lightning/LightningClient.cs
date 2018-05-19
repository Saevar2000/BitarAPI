using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Lightning.Models;
using Newtonsoft.Json;
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

        Socket s;

        public LightningClient()
        {
            // Create a Unix domain socket.
            s = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
            Log.Information(s.NoDelay.ToString());
        }

        public bool GetInfo(out Info info)
        {
            info = Send<Info>("getinfo");
            return (info != null);
        }

        public bool ListPeers(out List<Peer> peers)
        {
            peers = Send<List<Peer>>("listpeers");
            return (peers != null);
        }

        public bool ListNodes(out List<Node> nodes)
        {
            nodes = Send<List<Node>>("listnodes");
            return (nodes != null);
        }

        public bool ListInvoices(out List<Invoice> invoices)
        {
            invoices = Send<List<Invoice>>("listinvoices");
            return (invoices != null);
        }

        public bool CreateInvoice(int msatoshi, string label, string description, out Invoice invoice)
        {
            // invoice = new CreateInvoice
            // {
            //     msatoshi = msatoshi,
            //     label = label,
            //     description = description
            // };
            invoice = Send<Invoice>("invoice " + msatoshi + " " + label + " " + description);
            return (invoice != null);
        }

        private T Send<T>(string cmd, object[] parameters = null)
        {
            SocketSendReceive("a");
            return (T)Convert.ChangeType(JsonConvert.DeserializeObject<JsonResponse<T>>(SocketSendReceive(cmd, parameters)).Result, typeof(T));
        }

        private string SocketSendReceive(string request, object[] parameters = null)
        {
            // Convert request to the proper format
            request = CreateRequest(request, parameters);

            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];


            // Send a JSON-RPC command to the socket and receive response.
            try
            {
                if (!s.Connected)
                {
                    // Connect the socket to the endpoint.
                    s.Connect(unixDomainSocketEndPoint);
                    Log.Information("Socket connected to {0}", s.RemoteEndPoint.ToString());
                }

                Log.Information("Socket connection status {0}", s.Connected.ToString());

                // Encode the data string into a byte array.
                byte[] msg = Encoding.UTF8.GetBytes(request);

                // Send the data through the socket.
                int bytesSent = s.Send(msg);

                // Receive the response.
                int bytesRec = s.Receive(bytes);

                string result = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                return result;
            }
            catch (ArgumentNullException ane)
            {
                Log.Information("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                if (se.NativeErrorCode.Equals(10035))
                {
                    Console.WriteLine("Still Connected, but the Send would block");
                }
                else
                {
                    Console.WriteLine("Disconnected: error code {0}!", se.NativeErrorCode);
                }
                Log.Information("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Log.Information("Unexpected exception : {0}", e.ToString());
            }

            return null;
        }

        private string CreateRequest(string cmd, object[] parameters = null)
        {
            parameters = parameters ?? Array.Empty<string>();
            JObject o = new JObject()
            {
                { "id", "0"},
                { "method", cmd },
                { "params", new JArray(parameters) }
            };
            return o.ToString();
        }
    }
}