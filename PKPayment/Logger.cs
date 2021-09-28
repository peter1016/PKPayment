using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace PKPayment
{
    public class Logger
    {
        private bool enableDebug = false;

        public static Logger defaultLogger = new Logger();

        public Logger()
        {
        }

        public void AddToFile(string contents, string type = "PKLog")
        {
            AddToFile(contents, string.Empty, type);
        }

        public void AddToFile(string contents, string storeid, string type)
        {
            string strPath = string.Empty;
            string strSubfolder = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "log");
            
            //Check if log sub-folder does exist
            if (!System.IO.Directory.Exists(strSubfolder))
            {
                System.IO.Directory.CreateDirectory(strSubfolder);
            }

            try
            {
                string strLogTarget = ConfigurationManager.AppSettings["LogTarget"];
                if (string.Equals(strLogTarget, "DB"))
                {
                    Add2DB(contents, storeid);
                }
                else
                {
                    strPath = strSubfolder + "\\" + type + storeid + DateTime.Now.Date.ToString("yyyyMMdd") + ".txt";
                    Add2File(contents, strPath);
                }
            }
            catch (Exception ex)
            {
                strPath = strSubfolder + "\\" + type + "EX" + storeid + DateTime.Now.Date.ToString("yyyyMMdd") + ".txt";
                    try
                    {
                        Add2File(ex.Message, strPath);
                        Add2File(contents, strPath);
                    }
                    catch (Exception)
                    {

                    }
            }
        }

        private void Add2DB(string contents, string strStoreID)
        {
            if (!enableDebug) return;

            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["SystemLogConnectionString"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "insert into PKSystemLogs (TimeStamp, LogContent, LocationID) values (@TimeStamp, @LogContent, @LocationID)";
                    cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    if (contents.Length > 500)
                    {
                        contents = contents.Substring(0, 500);
                    }
                    cmd.Parameters.AddWithValue("@LogContent", contents);
                    cmd.Parameters.AddWithValue("@LocationID", strStoreID);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }

        private void Add2File(string contents, string strPath)
        {
            //set up a filestream   
            FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);

            //set up a streamwriter for adding text
            StreamWriter sw = new StreamWriter(fs);

            //find the end of the underlying filestream
            sw.BaseStream.Seek(0, SeekOrigin.End);

            //add the text 
            sw.WriteLine(DateTime.Now.ToString() + ": " + contents);
            //add the text to the underlying filestream

            sw.Flush();
            //close the writer
            sw.Close();
        }

        public void AddToFile(Task<HttpResponseMessage> asyncResult)
        {
            AddToFile("debug: point a");
            if (asyncResult != null)
            {
                string msg = "debug: asyncResult";
                try
                {
                    msg += "\n    AsyncState:  " + (asyncResult.AsyncState == null ? "null" : asyncResult.AsyncState);
                }
                catch (Exception ex)
                {
                    AddToFile(ex.Message);
                }
                try
                {
                    msg += "\n    CreationOptions:  " + (asyncResult.CreationOptions.ToString());
                }
                catch (Exception ex)
                {
                    AddToFile(ex.Message);
                }
                try
                {
                    msg += "\n    Exception:  " + (asyncResult.Exception == null ? "null" : asyncResult.Exception.Message);
                }
                catch (Exception ex)
                {
                    AddToFile(ex.Message);
                }
                try
                {
                    msg += "\n    Id:  " + (asyncResult.Id.ToString());
                }
                catch (Exception ex)
                {
                    AddToFile(ex.Message);
                }
                try
                {
                    msg += "\n    IsCanceled:  " + (asyncResult.IsCanceled.ToString());
                }
                catch (Exception ex)
                {
                    AddToFile(ex.Message);
                }
                try
                {
                    msg += "\n    IsCompleted:  " + (asyncResult.IsCompleted.ToString());
                }
                catch (Exception ex)
                {
                    AddToFile(ex.Message);
                }
                try
                {
                    msg += "\n    IsFaulted:  " + (asyncResult.IsFaulted.ToString());
                }
                catch (Exception ex)
                {
                    AddToFile(ex.Message);
                }
                try
                {
                    msg += "\n    Status:  " + (asyncResult.Status.ToString());
                }
                catch (Exception ex)
                {
                    AddToFile(ex.Message);
                }
                try
                {
                    msg += "\n    Result:  " + (asyncResult.Result == null ? "null" : asyncResult.Result.ToString());
                }
                catch (Exception ex)
                {
                    AddToFile(ex.Message);
                }
                AddToFile(msg);
            }
            else
                AddToFile("debug: null");
        }
    }
}

