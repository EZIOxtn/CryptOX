using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading;

namespace CryptOX
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;
        string data;
        private TcpClient C;
        public Form1()
        {
            InitializeComponent();
            InitializeServer();
        }
        private void InitializeServer()
        {


            // Start TCP listener on a separate thread
            Task.Run(() => StartListener());
        }
        private void StartListener()
        {
            try
            {
                C = new TcpClient();


                // Connect to the remote host
                C.Connect(Properties.Settings.Default.ipx, Properties.Settings.Default.porrt); // Assuming P is a string representing the port number

                // Connection successful
                try
                {
                    

                    Thread receiveThread = new Thread(ReceiveMessages);
                    Console.WriteLine("thread ready");
                    receiveThread.Start();
                    Console.WriteLine("thread started");


                }
                catch (Exception ex){
                    Console.WriteLine("error " + ex.Message);

                }

                

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:listen " + ex.Message);
            }
        }
        private void HandleClient()
        {
            try
            {
                using (NetworkStream stream = C.GetStream())
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    byte[] buffer = new byte[8192];
                    int bytesRead;

                    // Read from the stream until a complete message is received
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // Append the received bytes to the StringBuilder
                        stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                        // Check if the received data contains the end of message marker
                        if (stringBuilder.ToString().Contains("}"))
                        {
                            // Complete message received, break the loop
                            break;
                        }
                    }

                    // Process the complete message
                    string data = stringBuilder.ToString().Trim();
                    ProcessMessage(data);
                }
                    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: receive" + ex.Message + data);
            }
            finally
            {
                C.Close();
            }
        }
        private void ProcessMessage(string data)
        {
            try
            {
                // Parse the received JSON data
                dynamic message = JsonConvert.DeserializeObject(data);

                // Extract photo, user, message from the message object
                string photo = message.photo;
                string user = message.user;
                string messageText = message.message;

                // Convert base64 photo to Image
                Image img = Base64ToImage(photo);

                // Update UI on the UI thread
                Invoke(new Action(() =>
                {
                    drakeUIDataGridView1.Rows.Add(img, user, messageText);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing message: " + ex.Message + data);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Prepare message
                string photo = Properties.Settings.Default.photb64 ;
                string user = Properties.Settings.Default.username;
                string message = textBox1.Text; // Assuming a static message for demonstration

                string jsonMessage = $"{{\"photo\":\"{photo}\",\"user\":\"{user}\",\"message\":\"{message}\"}}";

                // Connect to localhost (change IP if necessary) and port
                // Convert the message to bytes
                try
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    byte[] bytes = Encoding.UTF8.GetBytes(jsonMessage);
                    C.GetStream().Write(bytes, 0, bytes.Length);
                    Console.WriteLine("bytes lenth  " + bytes.Length.ToString());
                    string data = stringBuilder.ToString().Trim();
                    ProcessMessage(jsonMessage);
                    if (message.ToLower() == "exit")
                    {
                        C.Close();
                        Environment.Exit(0);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error sending message: ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:send " + ex.Message);
            }
        }
        public static Image Base64ToImage(string base64String)
        {
            try
            {
                // Convert base64 string to byte array
                byte[] imageBytes = Convert.FromBase64String(base64String);

                // Create MemoryStream from byte array
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    // Create image from MemoryStream
                    Image image = Image.FromStream(ms);
                    return image;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show("Error converting base64 to image: " + ex.Message);
                return null;
            }
        }
        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("ready");
                    byte[] bytes = new byte[8192];
                    Console.WriteLine("byted");
                    int totalBytesRead = 0;
                    int bytesRead;

                    // Keep reading until we've received all bytes for a complete message
                    while (totalBytesRead < bytes.Length && (bytesRead = C.GetStream().Read(bytes, totalBytesRead, bytes.Length - totalBytesRead)) > 0)
                    {
                        totalBytesRead += bytesRead;
                    }

                    if (totalBytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(bytes, 0, totalBytesRead);
                        Console.WriteLine("ggg" + message);
                        Console.WriteLine("processing msg");
                        ProcessMessage(message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error receiving message: {e}");
                // Log the error or handle it as needed

                // If an exception occurs, wait before retrying
                Thread.Sleep(1000); // Wait for 1 second before retrying
            }
        }


    }

}
