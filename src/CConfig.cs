using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace SQLMonitor
{
    /// <summary>
    /// Class definition to hold the applications config information
    /// This allows me to let the user modify threshold data and other setting
    /// This class reads from and writes to a config file as required.
    /// The rest of the app will read it's data from this class.
    /// Decided to use a class here because that many globals in the main form because unmanagable and messy.
    /// Still using public vars rather than accessors for speed though.
    /// </summary>
    public class CConfig
    {
        // Make the variables public to save on calls and improve speed
        public bool PAL, LogData, CaptureBlocking, TSleep, pre2005, denali, is2014;
        public string Path, PalFormat;
        public int thresCPU, thresCPUQueue, thresProcCache, thresBufferCache, thresDiskIdle, thresPageLife, thresMemPagesSec, Palsi;
        string filename;
        public NameValueCollection SavedServers;
        byte[] Key;
        byte[] IV;
        CryptoStream cStream;

        /// <summary>
        /// The constructor for the class
        /// </summary>
        public CConfig()
        {
            Path = Directory.GetCurrentDirectory();
            LogData = false;
            PAL = false;
            filename = Path + @"\\thresholds.cfg";
            SavedServers = null;
            denali = false;

            // Set it up
            GetConfig();

            // init the Key and iv with some data
            // and create the Key and IV byte arrays
            Key = new ASCIIEncoding().GetBytes("h48dm38sl3h6ia1n");
            IV = new ASCIIEncoding().GetBytes("j7hg5h8jf4an0e7c");
        }

        /// <summary>
        /// Function to save the list of servers back to a config file
        /// </summary>
        public void SaveServers()
        {
            // If the file exists - remove the file and create a new one
            // now open a file
            if (File.Exists(Path + @"\\eServers.cfg"))
            {
                File.Delete(Path + @"\\eServers.cfg");
            }
            
            FileStream fs = new FileStream(Path + @"\\eServers.cfg", FileMode.Create, FileAccess.Write);

            string ToEncrypt = "";

            // Create a long string wiht all our data in it
            foreach (string key in SavedServers.AllKeys)
            {
                ToEncrypt += key + "=" + SavedServers.Get(key) + "\n";
            }

            // Now Encrypt it
            MemoryStream Enc = EncryptData(ToEncrypt);

            // Now write the stream to a file
            Enc.WriteTo(fs);

            // Clean-up
            cStream.Close();
            Enc.Close();
            Enc.Dispose();
            cStream.Dispose();
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        /// <summary>
        /// Function to get the list of saved servers from a local config file
        /// </summary>
        public void GetServers()
        {
            // handle previous file format as required
            if (File.Exists(Path + @"\\Servers.cfg"))
            {
                Form1.Log.WriteLine(DateTime.Now + " - Old Server config file found");

                SavedServers = new NameValueCollection();

                // read the file into an NVP - and then save it back to an encrypted format
                // then delete the file
                FileStream fs = new FileStream(Path + @"\\Servers.cfg", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                Form1.Log.WriteLine(DateTime.Now + " - Parsing .........");
                while (!sr.EndOfStream)
                {
                    string data = sr.ReadLine();

                    // split each line based on the =
                    string[] kvp = data.Split('=');

                    // now update the collection
                    SavedServers.Add(kvp[0], kvp[1]);
                }

                // close the stream and the file
                sr.Close();
                sr.Dispose();
                fs.Close();
                fs.Dispose();

                // now save it in the new format
                Form1.Log.WriteLine(DateTime.Now + " - Encrypting ..........");
                SaveServers();

                // Now remove the old file
                Form1.Log.WriteLine(DateTime.Now + " - Removing old file ........");
                File.Delete(Path + @"\\Servers.cfg");

                Form1.Log.WriteLine(DateTime.Now + " - Done");

                // can return here since the NVP has been created
                return;
            }

            // only do it if the file exists
            if (File.Exists(Path + @"\\eServers.cfg"))
            {
                SavedServers = new NameValueCollection();
                
                // Open the File
                FileStream fs = new FileStream(Path + @"\\eServers.cfg", FileMode.Open, FileAccess.Read);

                BinaryReader br = new BinaryReader(fs);

                // read the data
                byte[] tData = br.ReadBytes((int)fs.Length);

                // create a memory stream based on the byte array
                MemoryStream Dec = new MemoryStream(tData);

                // Decrypt it
                string Decrypted = DecryptData(Dec);
                
                // now I need to remove the annoying \0 padding from the encryption
                char[] mt = Decrypted.ToCharArray();

                // reset the string
                Decrypted = "";

                // now loop through and don't add the \0
                // dirty fix really, but the encryption process will pad with nulls as required
                // and all other methods of removing the padding lead to unexpected results.
                // This isn't a wonderful solution - but it works every time.
                foreach (int c in mt)
                {
                    if (c != 0)
                    {
                        Decrypted += (char)c;
                    }
                }
               
                // split it on the = value
                string[] values = Decrypted.Split('\n');

                // loop through the file
                foreach(string index in values)
                {
                    if (index.Length > 1)
                    {
                        // split each line based on the =
                        string[] kvp = index.Split('=');

                        // now update the collection
                        SavedServers.Add(kvp[0], kvp[1]);
                    }
                }  
              
                // Now close off the file and the stream.
                fs.Close();
                fs.Dispose();
                br.Close();
                Dec.Close();
                Dec.Dispose();
            }
        }

        /// <summary>
        /// Saves the config dat to an xml file
        /// </summary>
        public void SaveConfig()
        {
            // Stick 'em in here first
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("CPU", thresCPU.ToString());
            nvc.Add("CPU QUEUE", thresCPUQueue.ToString());
            nvc.Add("PROCEDURE CACHE", thresProcCache.ToString());
            nvc.Add("BUFFER CACHE", thresBufferCache.ToString());
            nvc.Add("DISK IDLE", thresDiskIdle.ToString());
            nvc.Add("PAGE LIFE", thresPageLife.ToString());
            nvc.Add("PAGES SEC", thresMemPagesSec.ToString());

            // now open a file
            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            // now write out the data
            foreach(string key in nvc.AllKeys)
            {
                sw.WriteLine(key + "=" + nvc.Get(key));
            }

            // now cose it all off
            sw.Flush();
            fs.Flush();
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();

            nvc.Clear();
        }

        /// <summary>
        /// Reads a config file if it exists - otherwise it will set default values
        /// </summary>
        public void GetConfig()
        {
            // if the config file exists - read it
            if (File.Exists(filename))
            {
                // first we create the basic structure key n value
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("CPU", "");
                nvc.Add("CPU QUEUE", "");
                nvc.Add("PROCEDURE CACHE", "");
                nvc.Add("BUFFER CACHE", "");
                nvc.Add("DISK IDLE", "");
                nvc.Add("PAGE LIFE", "");
                nvc.Add("PAGES SEC", "");

                StreamReader sw = new StreamReader(filename);

                // loop through the file
                while (!sw.EndOfStream)
                {
                    // split each line based on the =
                    string[] kvp = sw.ReadLine().Split('=');

                    // now update the collection
                    nvc.Set(kvp[0], kvp[1]);
                }

                // close the file as we are done with it
                sw.Close();

                // now assign the data to the variables
                thresCPU = Convert.ToInt32(nvc.Get("CPU"));
                thresCPUQueue = Convert.ToInt32(nvc.Get("CPU QUEUE"));
                thresProcCache = Convert.ToInt32(nvc.Get("PROCEDURE CACHE"));
                thresBufferCache = Convert.ToInt32(nvc.Get("BUFFER CACHE"));
                thresDiskIdle = Convert.ToInt32(nvc.Get("DISK IDLE"));
                thresPageLife = Convert.ToInt32(nvc.Get("PAGE LIFE"));
                thresMemPagesSec = Convert.ToInt32(nvc.Get("PAGES SEC"));

                // now get rid of the nvc
                nvc.Clear();
            }
            else
            {
                // otherwise just set the default values
                thresCPU = 75; 
                thresCPUQueue = 2; 
                thresProcCache = 80; 
                thresBufferCache = 90; 
                thresDiskIdle = 50;
                thresPageLife = 300;
                thresMemPagesSec = 20;

               
            }
        }

        
        /// <summary>
        /// Function to encrypt what we are passed in.
        /// At the moment this will be a SQL Password that will be stored or logged
        /// // Code basically taken from MSDN and adapted
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MemoryStream EncryptData(string data)
        {
            try
            {
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                byte[] ToEncrypt = new ASCIIEncoding().GetBytes(data);

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                cStream = new CryptoStream(mStream, new TripleDESCryptoServiceProvider().CreateEncryptor(Key, IV), CryptoStreamMode.Write);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(ToEncrypt, 0, ToEncrypt.Length);
                cStream.FlushFinalBlock();

                // Note we don't close the stream at this point otherwise we can't write it out later
                // so the stream is closed by another process once this call has returned.

                return (mStream);
            }

            catch (Exception e)
            {
                Form1.Log.WriteLine(DateTime.Now + "ERROR Decrypting data [ " + e.Message + "]");
            }

            return (null);
        }

        /// <summary>
        /// Decrypts the data we give it
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string DecryptData(MemoryStream data)
        {
            try
            {
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();
                //MemoryStream mStream = new MemoryStream(new UTF8Encoding().GetBytes(data));
                
                // then stick it in the memory stream
                //MemoryStream mStream = new MemoryStream(tt);

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream dStream = new CryptoStream(data, new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV), CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[data.Length];


                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                dStream.Read(fromEncrypt, 0, fromEncrypt.Length);

                 // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data - and put it into a string
                string RetVal = new ASCIIEncoding().GetString(fromEncrypt);

                // Close the streams.
                data.Close();
                dStream.Close();
                
                return (RetVal);
            }

            catch (Exception e)
            {
                Form1.Log.WriteLine(DateTime.Now + "ERROR Decrypting data [ " + e.Message + "]");
            }

            return (null);
        }
    }
}
