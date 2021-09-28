using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PKPayment
{
    public partial class Form1 : Form
    {
        WSBRIDGELib.wsBridgeCtl wBridge = new WSBRIDGELib.wsBridgeCtl();
        string IP = string.Empty;
        string Port = string.Empty;
        bool canClose = false;
        public Form1()
        {
            InitializeComponent();

            string PrinterName = System.Configuration.ConfigurationManager.AppSettings["Printer"];
            PrinterSettings.StringCollection PrinterList = PrinterSettings.InstalledPrinters;
            PrintDocument PrintContent = new PrintDocument();
            btnPrinter.Text = PrintContent.PrinterSettings.PrinterName;
            for (int i = 0; i < PrinterList.Count; i++)
            {
                if (PrinterList[i] == PrinterName)
                {
                    btnPrinter.Text = PrinterName;
                    break;
                }
            }
            IP = System.Configuration.ConfigurationManager.AppSettings["DeviceIP"];
            Port = System.Configuration.ConfigurationManager.AppSettings["DevicePORT"];
            lbIP.Text = "Device IP: " + IP;
            lbPort.Text = "Device Port: " + Port;

            wBridge.ListenIP = System.Configuration.ConfigurationManager.AppSettings["ServerIP"];
            wBridge.ListenPort = short.Parse(System.Configuration.ConfigurationManager.AppSettings["ServerPORT"].ToString());
            lbServerIP.Text = "Server IP: " + wBridge.ListenIP;
            lbServerPort.Text = "Server Port: " + wBridge.ListenPort.ToString();

            wBridge.DataArrival += WBridge_DataArrival;            
        }

        private void WBridge_DataArrival(short index, int lengthTotal, string data)
        {
            //data = @"<?xml version=""1.0""?>
            //<ExternalTransactionRequest time=""2021/09/14 14:39:36:052""><Transaction>
            //<merchantID>merch_test</merchantID>
            //<transactionName>Credit</transactionName>
            //<transactionCode>Sale</transactionCode>
            //<invoiceNo>WS00010000030006</invoiceNo>
            //<operatorID>999999</operatorID>
            //<deviceID>Server</deviceID>
            //<applicationName>wPayment</applicationName>
            //<totalAmount>500.00</totalAmount>
            //</Transaction></ExternalTransactionRequest>";
            Logger.defaultLogger.AddToFile("request: " + data + " ;index" + index.ToString());
            StringReader sr = new StringReader(data);
            XmlSerializer xmlRequest = new XmlSerializer(typeof(ExternalTransactionRequest));
            ExternalTransactionRequest request = (ExternalTransactionRequest)xmlRequest.Deserialize(sr);

            frmPayGate PayGateway = null;
            if (request.Transaction.transactionCode == "Sale")
            {
                PayGateway = new frmPayGate(System.Net.IPAddress.Parse(IP), Convert.ToInt32(Port), request.Transaction.totalAmount, Global.PAYMENT_TYPE_Payment, 1);
            }
            else if (request.Transaction.transactionCode == "Return")
            {
                PayGateway = new frmPayGate(System.Net.IPAddress.Parse(IP), Convert.ToInt32(Port), request.Transaction.totalAmount, Global.PAYMENT_TYPE_Refund, 1);
            }
            else if (request.Transaction.transactionCode == "VoidSale" || request.Transaction.transactionCode == "VoidReturn")
            {
                PayGateway = new frmPayGate(System.Net.IPAddress.Parse(IP), Convert.ToInt32(Port), request.Transaction.totalAmount, "Void", 1);
            }
            else
            {
                return;
            }
            PayGateway.Exec();
            lbMsg.Text = DateTime.Now.ToString() + ": " + PayGateway.lbMsg.Text;

            string PaygateStr = PayGateway.PayGate.ReceiptString; //!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                                  //====================
            //PaygateStr = "9901000010100102200716103115259104128110520010914811122113129001001002030000301Debit302************6213303030503062308A00000027710103098000008000311Interac3120400172325401001402APPROVED 17232540300500129601A76076216027607621700GONG CHA701386 ROBSON ST702VANCOUVER BC V6B 2B2703(778) 379 - 380673000";
            if (PaygateStr.Length > 0)
            {
                string[] responsePacketArray = PaygateStr.Split(new Char[] { Convert.ToChar(28) }, StringSplitOptions.RemoveEmptyEntries);
                responsePacketArray[0] = Regex.Replace(responsePacketArray[0], "[^.0-9]", "");
                MessagePacket ReceiptPacket = frmPayGate.PayGateSocket.SaveandResponse(responsePacketArray);
                PaygateStr = frmPayGate.PayGateSocket.Package2String(ReceiptPacket, true);
            }
            string Memo = string.Empty;
            ExternalTransactionResponse response = new ExternalTransactionResponse();
            if (PayGateway.PayGate.Approved && !string.IsNullOrEmpty(PayGateway.PayGate.PaymentType) && !string.IsNullOrEmpty(PayGateway.PayGate.MPrintString))
            {
                ReceiptPrintUtl ReceiptPrinter = new ReceiptPrintUtl();
                PrintReceipt Pr = new PrintReceipt("1");
                Pr.PrintPaymentGatewayDetail(PaygateStr, ReceiptPrinter, 10, true);
                ReceiptPrinter.ClearPrinterData();
                Pr.PrintPaymentGatewayDetail(PaygateStr, ReceiptPrinter, 10, false);
                response.time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//FindSubItem(PaygateStr, "TransDate:", 11) + " " + FindSubItem(PaygateStr, "TransTime:", 11);
                response.status = "Approved";
                response.description = FindSubItem(PaygateStr, "Response Text:", 15);
                
                response.Trans.merchantID = FindSubItem(PaygateStr, "MID:", 5);
                response.Trans.accountNumber = FindSubItem(PaygateStr, "AccountNo:", 15);
                response.Trans.accountDescription = FindSubItem(PaygateStr, "CardType:", 10);
                response.Trans.transactionName = request.Transaction.applicationName;
                response.Trans.invoiceNo = request.Transaction.invoiceNo;
                response.Trans.operatorID = request.Transaction.operatorID;
                response.Trans.deviceID = request.Transaction.deviceID;
                response.Trans.authorizationCode = FindSubItem(PaygateStr, "Auth#:", 7);
                response.Trans.interfaceName = "GlobalPayment";
                response.Trans.authorizedAmount = FindSubItem(PaygateStr, "Total Amount:", 15);
                response.Trans.gratuity = FindSubItem(PaygateStr, "TipAmount: $", 12);
                if (request.Transaction.transactionCode == "Sale")
                    Memo = "Approved Credit Sale";
                else if (request.Transaction.transactionCode == "Return")
                    Memo = "Approved Return";
                else if (request.Transaction.transactionCode == "VoidSale" || request.Transaction.transactionCode == "VoidReturn")
                    Memo = "Approved " + request.Transaction.transactionCode;
                response.Trans.memo = Memo;
            }
            else if (PaygateStr.Length < 1)
            {
                response.time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                response.status = "Declined";
                response.description = "Declined: " + PayGateway.lbMsg.Text;
                response.Trans.transactionName = request.Transaction.applicationName;
                response.Trans.invoiceNo = request.Transaction.invoiceNo;
                response.Trans.operatorID = request.Transaction.operatorID;
                response.Trans.deviceID = request.Transaction.deviceID;
                if (request.Transaction.transactionCode == "Sale")
                    Memo = "Declined Credit Sale: " + PayGateway.lbMsg.Text;
                else if (request.Transaction.transactionCode == "Return")
                    Memo = "Declined Return: " + PayGateway.lbMsg.Text;
                else if (request.Transaction.transactionCode == "VoidSale" || request.Transaction.transactionCode == "VoidReturn")
                    Memo = "Declined: " + request.Transaction.transactionCode + " " + PayGateway.lbMsg.Text;
                response.Trans.memo = Memo;
            }
            else
            {
                response.time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                response.status = "Declined";
                response.description = FindSubItem(PaygateStr, "Response Text:", 15);
                response.Trans.transactionName = request.Transaction.applicationName;
                response.Trans.invoiceNo = request.Transaction.invoiceNo;
                response.Trans.operatorID = request.Transaction.operatorID;
                response.Trans.deviceID = request.Transaction.deviceID;
                if (request.Transaction.transactionCode == "Sale")
                    Memo = "Declined Credit Sale: " + FindSubItem(PaygateStr, "Response Text:", 15);
                else if (request.Transaction.transactionCode == "Return")
                    Memo = "Declined Return: " + FindSubItem(PaygateStr, "Response Text:", 15);
                else if (request.Transaction.transactionCode == "VoidSale" || request.Transaction.transactionCode == "VoidReturn")
                    Memo = "Declined: " + request.Transaction.transactionCode + FindSubItem(PaygateStr, "Response Text:", 15);
                response.Trans.memo = Memo;
            }
            XmlSerializer Xmldata = new XmlSerializer(typeof(ExternalTransactionResponse));
            StringUTF8Writer sw = new StringUTF8Writer();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Xmldata.Serialize(sw, response, ns);
            Logger.defaultLogger.AddToFile("response: " + sw.ToString() + " ;index" + index.ToString());
            wBridge.ReplyData(index, sw.ToString());
        }
        public class StringUTF8Writer : System.IO.StringWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }
        public static string FindSubItem(string Source, string ItemName, int SubStrIndex)
        {
            int PositionStart, PositionEnd;
            PositionStart = Source.IndexOf(ItemName);
            if (PositionStart == -1) return "";

            PositionEnd = Source.Substring(PositionStart).IndexOf(";");
            try
            {
                if (PositionEnd == -1)
                    return Source.Substring(PositionStart).Substring(SubStrIndex);
                else
                    return Source.Substring(PositionStart, PositionEnd).Substring(SubStrIndex);
            }
            catch { return string.Empty; }
        }


        private void btnPrinter_Click(object sender, EventArgs e)
        {
            PrintDialog printer = new PrintDialog();
            if (printer.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            WriteAppSetting2Config("Printer", printer.PrinterSettings.PrinterName);
            btnPrinter.Text = printer.PrinterSettings.PrinterName;
        }

        public static void WriteAppSetting2Config(string strSettingName, string strFunctionName)
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetEntryAssembly().Location);
            config.AppSettings.Settings.Remove(strSettingName);
            config.AppSettings.Settings.Add(strSettingName, strFunctionName);
            config.Save();
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            wBridge.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Exit");
            menu.Items[0].Click += delegate
            {
                canClose = true;
                Logger.defaultLogger.AddToFile("Maunally exit...\r\n");
                this.Close();
            };

            notifyIcon1.ContextMenuStrip = menu;

            this.Text = "PKPayment " + Application.ProductVersion;

            wBridge.Start();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !this.canClose;
            this.Hide();
        }

        [XmlRootAttribute("ExternalTransactionRequest")]
        public class ExternalTransactionRequest
        {
            [XmlAttribute("time")]
            public string time { get; set; }

            [XmlElement("Transaction")]
            public Transaction Transaction { get; set; }
        }

        [XmlRootAttribute("Transaction")]
        public class Transaction
        {
            [XmlElement("merchantID")]
            public string merchantID { get; set; }

            [XmlElement("deviceID")]
            public string deviceID { get; set; }

            [XmlElement("transactionCode")]
            public string transactionCode { get; set; }

            [XmlElement("invoiceNo")]
            public string invoiceNo { get; set; }

            [XmlElement("operatorID")]
            public string operatorID { get; set; }

            [XmlElement("applicationName")]
            public string applicationName { get; set; }

            [XmlElement("memo")]
            public string memo { get; set; }

            [XmlElement("gratuity")]
            public string gratuity { get; set; }

            [XmlElement("totalAmount")]
            public string totalAmount { get; set; }
        }

        [XmlRootAttribute("ExternalTransactionResponse")]
        public class ExternalTransactionResponse
        {
            [XmlAttribute("time")]
            public string time { get; set; }

            [XmlAttribute("status")]
            public string status { get; set; }

            [XmlAttribute("description")]
            public string description { get; set; }


            [XmlElement("Transaction")]
            public TransactionRes Trans = new TransactionRes();
        }

        [XmlRootAttribute("Transaction")]
        public class TransactionRes
        {
            [XmlAttribute("merchantID")]
            public string merchantID { get; set; }

            [XmlAttribute("accountNumber")]
            public string accountNumber { get; set; }

            [XmlAttribute("accountDescription")]
            public string accountDescription { get; set; }

            [XmlAttribute("expirationDate")]
            public string expirationDate { get; set; }

            [XmlAttribute("transactionName")]
            public string transactionName { get; set; }

            [XmlAttribute("transactionCode")]
            public string transactionCode { get; set; }

            [XmlAttribute("invoiceNo")]
            public string invoiceNo { get; set; }

            [XmlAttribute("authorizationCode")]
            public string authorizationCode { get; set; }

            [XmlAttribute("operatorID")]
            public string operatorID { get; set; }

            [XmlAttribute("applicationName")]
            public string applicationName { get; set; }

            [XmlAttribute("interfaceName")]
            public string interfaceName { get; set; }

            [XmlAttribute("memo")]
            public string memo { get; set; }

            [XmlAttribute("deviceID")]
            public string deviceID { get; set; }

            [XmlAttribute("gratuity")]
            public string gratuity { get; set; }

            [XmlAttribute("authorizedAmount")]
            public string authorizedAmount { get; set; }
        }
    }
}
