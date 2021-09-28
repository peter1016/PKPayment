using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

namespace PKPayment
{
    public partial class frmPayGate : Form
    {
        IPAddress Address;
        int Port;
        string Amount;
        string TransType;
        //int intLanguageDisplay;
        public PayGateSocket PayGate = new PayGateSocket();
        public frmPayGate(IPAddress TerAddress, int TerPort, string TransAmount, string TerTransType, int intLanguageDisplay)
        {
            InitializeComponent();
            //CommonUtils.SetLanguage(intLanguageDisplay, this)

            Address = TerAddress;
            Port = TerPort;
            Amount = TransAmount;
            TransType = TerTransType;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Exec()
        {
            try
            {                
                if (PayGate.SendGlobalPaymentCommand(Address, Port, Decimal.Parse(Amount), TransType) == false)
                {
                    lbMsg.Text = "Network failure";
                    Logger.defaultLogger.AddToFile("Network failure...");
                }
                else
                {
                    lbMsg.Text = PayGate.MsgText;
                }
            }
            catch (Exception ex)
            {
                Logger.defaultLogger.AddToFile("Exception in PayGate.SendGlobalPaymentCommand: " + ex.Message);
            }
            finally
            {
                Logger.defaultLogger.AddToFile("Closing...");
                this.Close();
            }
        }
        private void frmPayGate_Load(object sender, EventArgs e)
        {
        }

        private void frmPayGate_Shown(object sender, EventArgs e)
        {
        }


        public class PayGateSocket
        {
            public const string TransType_Sale = "00";
            public const string TransType_PreAuto = "01";
            public const string TransType_PreAutoCompletion = "02";
            public const string TransType_Refund = "03";
            public const string TransType_Force = "04";
            public const string TransType_Void = "05";
            public const string TransType_Settlement = "20";
            public const string TransType_AutoSettlement = "21";

            public string paymentSplit = Convert.ToChar(28).ToString();
            long lTimeTick;
            Socket c = null;
            public Encoding Encoding = Encoding.Default;
            byte[] Buffer = new byte[1024];
            public string ReceiptString = string.Empty;
            public string CommandString = string.Empty;
            public MessagePacket ReceiptPacket, CommandPacket;
            public string MPrintString, CPrintString;
            public string PaymentType = string.Empty;
            public string MsgText = string.Empty;
            public bool Approved = false;
            public PayGateSocket()
            {

            }

            public int ConnectToPayGate(IPAddress Address, int Port)
            {
                IPEndPoint ipe = new IPEndPoint(Address, Port);

                c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                c.ReceiveTimeout = 2000;
                c.Blocking = true;
                c.Connect(ipe);

                return 0;
            }

            public int Send(string strSend)
            {
                byte[] bytes = Encoding.GetBytes(strSend);
                c.Send(bytes);
                return 0;
            }

            public int Receive()
            {
                int i = 0;
                byte[] buf = new byte[1];
                while (c.Available > 0)
                {
                    c.Receive(buf, 1, SocketFlags.None);
                    if (buf[0] == 0x11)
                    {
                        lTimeTick = DateTime.Now.Ticks;
                        continue; //heart beat
                    }
                    if (buf[0] == 0x06) continue; //Ack
                    Buffer[i] = buf[0];
                    i++;
                    if (c.Available < 1) Thread.Sleep(30);
                }
                if (i > 0)
                {
                    try
                    {
                        string DataReceived = this.Encoding.GetString(Buffer, 0, i);
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        StreamWriter Sw = new StreamWriter(path + "\\PayGatelog.txt", true);
                        Sw.WriteLine(DateTime.Now.ToString("yyMMdd HH:mm:ss") + ": " + DataReceived, i);
                        Sw.Close();
                    }
                    catch { }
                }
                return i;
            }

            public bool SendGlobalPaymentCommand(IPAddress Address, int Port, decimal Amount, string TransType)
            {
                bool Wait = true;
                long lTmpTick = 0;
                ReceiptString = string.Empty;
                CommandString = string.Empty;
                string sentData = string.Empty;

                Approved = false;
                try
                {
                    ConnectToPayGate(Address, Port);
                }
                catch(Exception ex)
                {
                    Logger.defaultLogger.AddToFile("Exception in ConnectToPayGate: " + ex.Message);
                    return false;
                }
                System.Threading.Thread.Sleep(300);
                int intAmount = Convert.ToInt32(Amount * 100);
                if (TransType == Global.PAYMENT_TYPE_Payment)
                    sentData = TransType_Sale + paymentSplit + "001" + intAmount.ToString();
                else if (TransType == Global.PAYMENT_TYPE_Refund)
                    sentData = TransType_Refund + paymentSplit + "001" + intAmount.ToString();
                else if (TransType == "Void")
                    sentData = TransType_Void;
                else if (TransType == "Settlement")
                    sentData = TransType_Settlement;


                try
                {
                    Logger.defaultLogger.AddToFile("PayGate.SendGlobalPaymentCommand... " + sentData);
                    Send(sentData);
                }
                catch(Exception ex)
                {
                    Logger.defaultLogger.AddToFile("Exception in send command: " + ex.Message);
                    return false;
                }
                c.ReceiveTimeout = 1000;
                c.Blocking = false;
                lTimeTick = DateTime.Now.Ticks;
                while (Wait && TransType != "Settlement")
                {
                    if ((DateTime.Now.Ticks - lTmpTick >= 10000000 * 2 && lTmpTick != 0) ||(DateTime.Now.Ticks - lTimeTick > 10000000 * 30))
                    {
                        if (MsgText.Trim().Length == 0) MsgText = "Communication timeout";
                        break;
                    }

                    int intGetBytes = 0;
                    //Application.DoEvents();
                    try
                    {
                        intGetBytes = Receive();
                        if (intGetBytes == 0) continue;
                    }
                    catch (Exception ex)
                    {
                        Logger.defaultLogger.AddToFile("Exception in communication: " + ex.Message);
                        return false;
                    }

                    string DataReceived = this.Encoding.GetString(Buffer, 0, intGetBytes);
                    string[] responsePacketArray = DataReceived.Split(new Char[] { Convert.ToChar(28) });
                    if (responsePacketArray.Length < 1) continue;
                    if (responsePacketArray[0].Length < 3) continue;
                    if (responsePacketArray[0].Substring(0,2) != "99") CommandPacket = SaveandResponse(responsePacketArray);
                    try
                    {
                        switch (responsePacketArray[0].Substring(0,2))
                        {
                            case "95":
                                MsgText = "Payment Gateway is StandAlone";
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            case "99":
                                {
                                    this.ReceiptString = DataReceived;

                                    ReceiptPacket = SaveandResponse(responsePacketArray);
                                    GetPayType(ReceiptPacket.M300_CardType.Substring(3));

                                    MPrintString = Package2String(ReceiptPacket, true);
                                    CPrintString = Package2String(ReceiptPacket, false);

                                    System.Threading.Thread.Sleep(200);
                                    Send("990"); //Print OK

                                    if (!string.IsNullOrEmpty(CommandString))
                                    {
                                        Wait = false;
                                        Approved = true;
                                    }
                                    if (Approved && !string.IsNullOrEmpty(ReceiptPacket.M101_TransStatus.Substring(3)))
                                    {
                                        if (ReceiptPacket.M101_TransStatus.Substring(3) != "00") Approved = false;
                                    }
                                    break;
                                }
                            case "00":
                                {
                                    this.CommandString = DataReceived;
                                    MsgText = (CommandPacket.M402_ResponseText.Length > 3) ? CommandPacket.M402_ResponseText.Substring(3) : "";
                                    GetPayType(CommandPacket.M300_CardType.Substring(3));

                                    if (!string.IsNullOrEmpty(ReceiptString))
                                    {
                                        Wait = false;
                                        Approved = true;
                                    }
                                    break;
                                }
                            case "01":
                                try
                                {
                                    MsgText = (CommandPacket.M402_ResponseText.Length > 3) ? CommandPacket.M402_ResponseText.Substring(3) : "";
                                }
                                catch
                                {
                                }
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            case "10":
                                try
                                {
                                    MsgText = (CommandPacket.M402_ResponseText.Length > 3) ? CommandPacket.M402_ResponseText.Substring(3) : "";
                                }
                                catch
                                {
                                }
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            case "11":
                                MsgText = "Communication Error";                                
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            case "12":
                                MsgText = "Cancelled by User";
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            case "13":
                                MsgText = "Timed out on User Input";
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            case "14":
                                MsgText = "Transaction Not Completed";
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            case "15":
                                MsgText = "Batch Empty";
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            case "16":
                                MsgText = "Declined by Merchant";
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            case "30":
                                MsgText = "Invalid ECR Parameter";
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                            default:
                                lTmpTick = DateTime.Now.Ticks;
                                break;
                        }                        
                    }
                    catch (Exception ex)
                    {
                        Logger.defaultLogger.AddToFile("Exception in frmPayGate.cs: " + ex.Message);
                    }
                }
                try
                {
                    if (c.Connected) c.Disconnect(false);
                    c.Close();
                }
                catch
                {
                }

                if (Approved == false && !string.IsNullOrEmpty(this.ReceiptString))
                {
                    PrintReceipt Pr = new PrintReceipt("1");
                    ReceiptPrintUtl ReceiptPrinter = new ReceiptPrintUtl();

                    //for (int i = 0; i < 90; i++)
                    //{
                    //    Thread.Sleep(10);
                    //    Application.DoEvents();
                    //}
                    if (!string.IsNullOrEmpty(MPrintString))
                    {
                        Pr.PrintPaymentGatewayDetail(MPrintString, ReceiptPrinter, 10, true, false);
                        ReceiptPrinter.Print();

                        ReceiptPrinter.ClearPrinterData();
                        Pr.PrintPaymentGatewayDetail(CPrintString, ReceiptPrinter, 10, false, false);
                        ReceiptPrinter.Print();
                    }
                }
                return true;
            }

            public static MessagePacket SaveandResponse(string[] responsePacketArray)
            {

                MessagePacket Approved = new MessagePacket();
                for (int i = 0; i < responsePacketArray.Length; i++)
                {
                    if (responsePacketArray[i].ToString().Substring(0, 3) == "001") Approved.M001_TransAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "002") Approved.M002_ECRTenderType = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "003") Approved.M003_ECRClerkID = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "100") Approved.M100_TransType = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "101") Approved.M101_TransStatus = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "102") Approved.M102_TransDate = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "103") Approved.M103_TransTime = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "104") Approved.M104_TransAmount = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "105") Approved.M105_TipAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "106") Approved.M106_CashbackAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "107") Approved.M107_SurchargeAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "108") Approved.M108_TaxAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "109") Approved.M109_TotalAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "110") Approved.M110_InvoiceNo = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "111") Approved.M111_OrderNo = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "112") Approved.M112_RefNo = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "113") Approved.M113_TransSeq = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "114") Approved.M114_TableNo = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "115") Approved.M115_TicketNo = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "116") Approved.M116_VoucherNo = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "117") Approved.M117_ShiptoPostal = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "118") Approved.M118_ClerkID = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "119") Approved.M119_ClerkName = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "120") Approved.M120_NumberofGiftCards = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "121") Approved.M121_DCCOptInFlag = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "122") Approved.M122_DCCConversionRate = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "123") Approved.M123_DCCCurrencyAlpha = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "124") Approved.M124_DCCAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "125") Approved.M125_DCCTipAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "126") Approved.M126_DCCTotalAmount = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "127") Approved.M127_OriginalTransAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "128") Approved.M128_OriginalTipAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "129") Approved.M129_OriginalCashbackAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "130") Approved.M130_OriginalSurchargeAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "131") Approved.M131_OriginalTaxAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "132") Approved.M132_OriginalTotalAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "133") Approved.M133_GiftCardRef = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "134") Approved.M134_OrignalTransType = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "135") Approved.M135_NumberofCust = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "300") Approved.M300_CardType = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "301") Approved.M301_CardDescription = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "302") Approved.M302_AccountNo = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "303") Approved.M303_CardLanguage = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "304") Approved.M304_CardName = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "305") Approved.M305_AccountType = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "306") Approved.M306_CardEntryMode = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "307") Approved.M307_CustomerNumber = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "308") Approved.M308_EMVAID = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "309") Approved.M309_EMVTVR = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "310") Approved.M310_EMVTSI = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "311") Approved.M311_EMVName = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "312") Approved.M312_EMVdata = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "313") Approved.M313_CardNotPresent = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "314") Approved.M314_AmountDueType = responsePacketArray[i];



                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "400") Approved.M400_AuthNo = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "401") Approved.M401_ResponseCode = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "402") Approved.M402_ResponseText = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "403") Approved.M403_ResponseISOCode = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "404") Approved.M404_RetrievalReference = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "405") Approved.M405_AmountDue = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "406") Approved.M406_Trace = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "407") Approved.M407_AVSResult = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "408") Approved.M408_CVVResult = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "409") Approved.M409_CardBalance = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "500") Approved.M500_BatchNumber = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "501") Approved.M501_BatchFlag = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "502") Approved.M502_BatchTotalAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "503") Approved.M503_BatchTotalCount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "504") Approved.M504_BatchSaleAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "505") Approved.M505_BatchSaleCount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "506") Approved.M506_BatchRefundAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "507") Approved.M507_BatchRefundCount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "508") Approved.M508_BatchVoidAmount = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "509") Approved.M509_BatchVoidCount = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "600") Approved.M600_DemoMode = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "601") Approved.M601_TerminalID = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "602") Approved.M602_MCID = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "603") Approved.M603_CurrencyCode = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "700") Approved.M700_ReceiptHeader1 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "701") Approved.M701_ReceiptHeader2 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "702") Approved.M702_ReceiptHeader3 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "703") Approved.M703_ReceiptHeader4 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "704") Approved.M704_ReceiptHeader5 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "705") Approved.M705_ReceiptHeader6 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "706") Approved.M706_ReceiptHeader7 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "707") Approved.M707_ReceiptFooter1 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "708") Approved.M708_ReceiptFooter2 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "709") Approved.M709_ReceiptFooter3 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "710") Approved.M710_ReceiptFooter4 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "711") Approved.M711_ReceiptFooter5 = responsePacketArray[i];


                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "712") Approved.M712_ReceiptFooter6 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "713") Approved.M713_ReceiptFooter7 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "714") Approved.M714_EndorsementLine1 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "715") Approved.M715_EndorsementLine2 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "716") Approved.M716_EndorsementLine3 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "717") Approved.M717_EndorsementLine4 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "718") Approved.M718_EndorsementLine5 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "719") Approved.M719_EndorsementLine6 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "720") Approved.M720_CustomerEndorsementLine1 = responsePacketArray[i];

                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "721") Approved.M721_CustomerEndorsementLine2 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "722") Approved.M722_CustomerEndorsementLine3 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "723") Approved.M723_CustomerEndorsementLine4 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "724") Approved.M724_CustomerEndorsementLine5 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "725") Approved.M725_CustomerEndorsementLine6 = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "726") Approved.M726_BlankTipLineIndicator = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "727") Approved.M727_TipAssistLine = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "728") Approved.M728_TaxRegistration = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "729") Approved.M729_TaxLabel = responsePacketArray[i];
                    else if (responsePacketArray[i].ToString().Substring(0, 3) == "900") Approved.M900_Settlement = responsePacketArray[i];
                }
                return Approved;
            }
            public static string Package2String(MessagePacket packet, bool FixEnglish = true)
            {
                bool English = true;
                StringBuilder strPackage = new StringBuilder();
                if (!string.IsNullOrEmpty(packet.M303_CardLanguage))
                {
                    strPackage.Append("Lang: " + packet.M303_CardLanguage.Substring(3) + ";");
                    if (packet.M303_CardLanguage != "3030" && FixEnglish == false) English = false;
                }

                if (!string.IsNullOrEmpty(packet.M104_TransAmount))
                {
                    decimal Amount;
                    if (decimal.TryParse(packet.M104_TransAmount.Substring(3), out Amount))
                    {
                        Amount *= 0.01m;
                        strPackage.Append("Amount: $" + Amount.ToString() + ";");
                    }
                    //strPackage.Append("TransAmount: " + packet.M104_TransAmount.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M105_TipAmount))
                {
                    decimal Amount;
                    if (decimal.TryParse(packet.M105_TipAmount.Substring(3), out Amount))
                    {
                        Amount *= 0.01m;
                        strPackage.Append("TipAmount: $" + Amount.ToString() + ";");
                    }
                    //strPackage.Append("TransAmount: " + packet.M104_TransAmount.Substring(3) + ";");
                }
                else
                {
                    strPackage.Append("TipAmount: $" + "0" + ";");
                }
                if (!string.IsNullOrEmpty(packet.M101_TransStatus))
                {
                    strPackage.Append("TransStatus: " + packet.M101_TransStatus.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M100_TransType))
                {
                    string TransType = "";
                    if (English)
                    {
                        if (packet.M100_TransType == "10000") TransType = "Sale";
                        if (packet.M100_TransType == "10001") TransType = "Pre Auth";
                        if (packet.M100_TransType == "10002") TransType = "Pre Auth Completion";
                        if (packet.M100_TransType == "10003") TransType = "Refund";
                        if (packet.M100_TransType == "10004") TransType = "Force";
                        if (packet.M100_TransType == "10005") TransType = "Void";
                        if (packet.M100_TransType == "10006") TransType = "Card Balance Inquiry";
                        if (packet.M100_TransType == "10007") TransType = "Cash Back Only";
                    }
                    else
                    {
                        if (packet.M100_TransType == "10000") TransType = "Vente";
                        if (packet.M100_TransType == "10001") TransType = "Pre Auth";
                        if (packet.M100_TransType == "10002") TransType = "Pre Auth Completion";
                        if (packet.M100_TransType == "10003") TransType = "retour";
                        if (packet.M100_TransType == "10004") TransType = "Force";
                        if (packet.M100_TransType == "10005") TransType = "Vide";
                        if (packet.M100_TransType == "10006") TransType = "Card Balance Inquiry";
                        if (packet.M100_TransType == "10007") TransType = "Cash Back Only";
                    }
                    strPackage.Append("Transaction Type: " + TransType + ";");

                }

                if (!string.IsNullOrEmpty(packet.M134_OrignalTransType))
                {
                    string TransType = "";
                    if (English)
                    {
                        if (packet.M134_OrignalTransType == "13400") TransType = "Sale";
                        if (packet.M134_OrignalTransType == "13401") TransType = "Pre Auth";
                        if (packet.M134_OrignalTransType == "13402") TransType = "Pre Auth Completion";
                        if (packet.M134_OrignalTransType == "13403") TransType = "Refund";
                        if (packet.M134_OrignalTransType == "13404") TransType = "Force";
                        if (packet.M134_OrignalTransType == "13405") TransType = "Void";
                        if (packet.M134_OrignalTransType == "13406") TransType = "Card Balance Inquiry";
                        if (packet.M134_OrignalTransType == "13407") TransType = "Cash Back Only";
                    }
                    else
                    {
                        if (packet.M134_OrignalTransType == "13400") TransType = "Vente";
                        if (packet.M134_OrignalTransType == "13401") TransType = "Pre Auth";
                        if (packet.M134_OrignalTransType == "13402") TransType = "Pre Auth Completion";
                        if (packet.M134_OrignalTransType == "13403") TransType = "retour";
                        if (packet.M134_OrignalTransType == "13404") TransType = "Force";
                        if (packet.M134_OrignalTransType == "13405") TransType = "Vide";
                        if (packet.M134_OrignalTransType == "13406") TransType = "Card Balance Inquiry";
                        if (packet.M134_OrignalTransType == "13407") TransType = "Cash Back Only";
                    }
                    strPackage.Append("OrgTransaction Type: " + TransType + ";");
                }
                if (!string.IsNullOrEmpty(packet.M300_CardType))
                {
                    string CardType = "";
                    if (packet.M300_CardType == "30000") CardType = "Debit";
                    if (packet.M300_CardType == "30001") CardType = "VISA";
                    if (packet.M300_CardType == "30002") CardType = "MC";
                    if (packet.M300_CardType == "30003") CardType = "AMEX";
                    if (packet.M300_CardType == "30004") CardType = "Diners Club";
                    if (packet.M300_CardType == "30005") CardType = "Discover Card";
                    if (packet.M300_CardType == "30006") CardType = "JCB";
                    if (packet.M300_CardType == "30007") CardType = "Union Pay Card";
                    if (packet.M300_CardType == "30008") CardType = "Other Credit Card";
                    if (packet.M300_CardType == "30009") CardType = "Gift Card";

                    strPackage.Append("CardType: " + CardType + ";");
                }

                if (!string.IsNullOrEmpty(packet.M301_CardDescription))
                {
                    strPackage.Append("CardDescription: " + packet.M301_CardDescription.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M302_AccountNo))
                {
                    strPackage.Append("AccountNo: " + packet.M302_AccountNo.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M304_CardName))
                {
                    strPackage.Append("CardName: " + packet.M304_CardName.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M306_CardEntryMode))
                {
                    if (English)
                    {
                        //if (packet.M306_CardEntryMode == "3060") strPackage.Append("Entry Mode: " + "Magnetic Stripe;");
                        //if (packet.M306_CardEntryMode == "3061") strPackage.Append("Entry Mode: " + "Chip;");
                        //if (packet.M306_CardEntryMode == "3062") strPackage.Append("Entry Mode: " + "Tap;");
                        //if (packet.M306_CardEntryMode == "3063") strPackage.Append("Entry Mode: " + "Manual;");
                        //if (packet.M306_CardEntryMode == "3064") strPackage.Append("Entry Mode: " + "Chip Fallback to Swipe;");
                        //if (packet.M306_CardEntryMode == "3065") strPackage.Append("Entry Mode: " + "Chip Fallback to Manual;");
                        //if (packet.M306_CardEntryMode == "3066") strPackage.Append("Entry Mode: " + "Card Not Present Manual;");
                        if (packet.M306_CardEntryMode == "3060") strPackage.Append("Entry Mode: " + "S;");
                        if (packet.M306_CardEntryMode == "3061") strPackage.Append("Entry Mode: " + "C;");
                        if (packet.M306_CardEntryMode == "3062") strPackage.Append("Entry Mode: " + "T;");
                        if (packet.M306_CardEntryMode == "3063") strPackage.Append("Entry Mode: " + "M;");
                        if (packet.M306_CardEntryMode == "3064") strPackage.Append("Entry Mode: " + "Chip Fallback to Swipe;");
                        if (packet.M306_CardEntryMode == "3065") strPackage.Append("Entry Mode: " + "Chip Fallback to Manual;");
                        if (packet.M306_CardEntryMode == "3066") strPackage.Append("Entry Mode: " + "Card Not Present Manual;");
                    }
                    else
                    {
                        if (packet.M306_CardEntryMode == "3060") strPackage.Append("Entry Mode: " + "G;");
                        if (packet.M306_CardEntryMode == "3061") strPackage.Append("Entry Mode: " + "Chip;");
                        if (packet.M306_CardEntryMode == "3062") strPackage.Append("Entry Mode: " + "Tap;");
                        if (packet.M306_CardEntryMode == "3063") strPackage.Append("Entry Mode: " + "Manual;");
                        if (packet.M306_CardEntryMode == "3064") strPackage.Append("Entry Mode: " + "Chip Fallback to Swipe;");
                        if (packet.M306_CardEntryMode == "3065") strPackage.Append("Entry Mode: " + "Chip Fallback to Manual;");
                        if (packet.M306_CardEntryMode == "3066") strPackage.Append("Entry Mode: " + "Card Not Present Manual;");
                    }
                }

                if (!string.IsNullOrEmpty(packet.M305_AccountType))
                {
                    if (English)
                    {
                        if (packet.M305_AccountType == "3050") strPackage.Append("Account Type: " + "DEFAULT;");
                        if (packet.M305_AccountType == "3051") strPackage.Append("Account Type: " + "SAVING;");
                        if (packet.M305_AccountType == "3052") strPackage.Append("Account Type: " + "CHEQUING;");
                    }
                    else
                    {
                        if (packet.M305_AccountType == "3050") strPackage.Append("Account Type: " + "DEFAULT;");
                        if (packet.M305_AccountType == "3051") strPackage.Append("Account Type: " + "SAVING;");
                        if (packet.M305_AccountType == "3052") strPackage.Append("Account Type: " + "CHEQUES;");
                    }
                }

                if (!string.IsNullOrEmpty(packet.M308_EMVAID))
                {
                    if (English)
                        strPackage.Append("AID: " + packet.M308_EMVAID.Substring(3) + ";");
                    else
                        strPackage.Append("AID: " + packet.M308_EMVAID.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M309_EMVTVR))
                {
                    strPackage.Append("TVR: " + packet.M309_EMVTVR.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M310_EMVTSI))
                {
                    strPackage.Append("TSI: " + packet.M310_EMVTSI.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M311_EMVName))
                {
                    strPackage.Append("EMVName: " + packet.M311_EMVName.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M312_EMVdata))
                {
                    strPackage.Append("CVM: " + packet.M312_EMVdata.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M102_TransDate))
                {
                    string Date = string.Empty;
                    Date += "20" + packet.M102_TransDate.Substring(3).Substring(0, 2) + "/";
                    Date += packet.M102_TransDate.Substring(3).Substring(2, 2) + "/";
                    Date += packet.M102_TransDate.Substring(3).Substring(4, 2);
                    strPackage.Append("TransDate: " + Date + ";");
                }

                if (!string.IsNullOrEmpty(packet.M103_TransTime))
                {
                    string time = string.Empty;
                    time += packet.M103_TransTime.Substring(3).Substring(0, 2) + ":";
                    time += packet.M103_TransTime.Substring(3).Substring(2, 2) + ":";
                    time += packet.M103_TransTime.Substring(3).Substring(4, 2);
                    strPackage.Append("TransTime: " + time + ";");
                }

                if (!string.IsNullOrEmpty(packet.M106_CashbackAmount))
                {
                    strPackage.Append("CashbackAmount: " + packet.M106_CashbackAmount.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M109_TotalAmount))
                {
                    decimal Amount;
                    if (decimal.TryParse(packet.M109_TotalAmount.Substring(3), out Amount))
                    {
                        Amount *= 0.01m;
                        strPackage.Append("Total Amount: $" + Amount.ToString() + ";");
                    }
                }

                if (!string.IsNullOrEmpty(packet.M110_InvoiceNo))
                {
                    strPackage.Append("InvoiceNo: " + packet.M110_InvoiceNo.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M400_AuthNo))
                {
                    if (English)
                        strPackage.Append("Auth#: " + packet.M400_AuthNo.Substring(3) + ";");
                    else
                        strPackage.Append("CODE APPR: " + packet.M400_AuthNo.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M401_ResponseCode))
                {
                    strPackage.Append("Response Code: " + packet.M401_ResponseCode.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M402_ResponseText))
                {
                    if (English)
                    {
                        strPackage.Append("Response Text: " + packet.M402_ResponseText.Substring(3) + ";");
                    }
                    else
                    {
                        if (packet.M402_ResponseText.Substring(3) == "APPROVED") strPackage.Append("Response Text: " + "APPROUVE;");
                        else if (packet.M402_ResponseText.Substring(3) == "DECLINED") strPackage.Append("Response Text: " + "REFUSE;");

                    }
                }

                if (!string.IsNullOrEmpty(packet.M403_ResponseISOCode))
                {
                    strPackage.Append("Response ISO Code: " + packet.M403_ResponseISOCode.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M500_BatchNumber))
                {
                    if (English)
                        strPackage.Append("Batch #: " + packet.M500_BatchNumber.Substring(3) + ";");
                    else
                        strPackage.Append("No Lot: " + packet.M500_BatchNumber.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M600_DemoMode))
                {
                    strPackage.Append("Demo: Demo;");
                }

                if (!string.IsNullOrEmpty(packet.M601_TerminalID))
                {
                    if (English)
                        strPackage.Append("TID: " + packet.M601_TerminalID.Substring(3) + ";");
                    else
                        strPackage.Append("IDT: " + packet.M601_TerminalID.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M602_MCID))
                {
                    if (English)
                        strPackage.Append("MID: " + packet.M602_MCID.Substring(3) + ";");
                    else
                        strPackage.Append("IDM: " + packet.M602_MCID.Substring(3) + ";");

                }

                if (!string.IsNullOrEmpty(packet.M700_ReceiptHeader1))
                {
                    strPackage.Append("Header1: " + packet.M700_ReceiptHeader1.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M701_ReceiptHeader2))
                {
                    strPackage.Append("Header2: " + packet.M701_ReceiptHeader2.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M702_ReceiptHeader3))
                {
                    strPackage.Append("Header3: " + packet.M702_ReceiptHeader3.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M703_ReceiptHeader4))
                {
                    strPackage.Append("Header4: " + packet.M703_ReceiptHeader4.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M704_ReceiptHeader5))
                {
                    strPackage.Append("Header5: " + packet.M704_ReceiptHeader5.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M705_ReceiptHeader6))
                {
                    strPackage.Append("Header6: " + packet.M705_ReceiptHeader6.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M706_ReceiptHeader7))
                {
                    strPackage.Append("Header7: " + packet.M706_ReceiptHeader7.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M707_ReceiptFooter1))
                {
                    strPackage.Append("Footer1: " + packet.M707_ReceiptFooter1.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M708_ReceiptFooter2))
                {
                    strPackage.Append("Footer2: " + packet.M708_ReceiptFooter2.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M709_ReceiptFooter3))
                {
                    strPackage.Append("Footer3: " + packet.M709_ReceiptFooter3.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M710_ReceiptFooter4))
                {
                    strPackage.Append("Footer4: " + packet.M710_ReceiptFooter4.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M711_ReceiptFooter5))
                {
                    strPackage.Append("Footer5: " + packet.M711_ReceiptFooter5.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M712_ReceiptFooter6))
                {
                    strPackage.Append("Footer6: " + packet.M712_ReceiptFooter6.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M713_ReceiptFooter7))
                {
                    strPackage.Append("Footer7: " + packet.M713_ReceiptFooter7.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M714_EndorsementLine1))
                {
                    strPackage.Append("EnLine1: " + packet.M714_EndorsementLine1.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M715_EndorsementLine2))
                {
                    strPackage.Append("EnLine2: " + packet.M715_EndorsementLine2.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M716_EndorsementLine3))
                {
                    strPackage.Append("EnLine3: " + packet.M716_EndorsementLine3.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M717_EndorsementLine4))
                {
                    strPackage.Append("EnLine4: " + packet.M717_EndorsementLine4.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M718_EndorsementLine5))
                {
                    strPackage.Append("EnLine5: " + packet.M718_EndorsementLine5.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M719_EndorsementLine6))
                {
                    strPackage.Append("EnLine6: " + packet.M719_EndorsementLine6.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M720_CustomerEndorsementLine1))
                {
                    strPackage.Append("CustEnLine1: " + packet.M720_CustomerEndorsementLine1.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M721_CustomerEndorsementLine2))
                {
                    strPackage.Append("CustEnLine2: " + packet.M721_CustomerEndorsementLine2.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M722_CustomerEndorsementLine3))
                {
                    strPackage.Append("CustEnLine3: " + packet.M722_CustomerEndorsementLine3.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M723_CustomerEndorsementLine4))
                {
                    strPackage.Append("CustEnLine4: " + packet.M723_CustomerEndorsementLine4.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M724_CustomerEndorsementLine5))
                {
                    strPackage.Append("CustEnLine5: " + packet.M724_CustomerEndorsementLine5.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M725_CustomerEndorsementLine6))
                {
                    strPackage.Append("CustEnLine6: " + packet.M725_CustomerEndorsementLine6.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M726_BlankTipLineIndicator))
                {
                    strPackage.Append("TipLine: " + packet.M726_BlankTipLineIndicator.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M112_RefNo))
                {
                    if (English)
                        strPackage.Append("REF#: " + packet.M112_RefNo.Substring(3) + ";");
                    else
                        strPackage.Append("No. REF: " + packet.M112_RefNo.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M502_BatchTotalAmount))
                {
                    strPackage.Append("Total Amount: " + packet.M502_BatchTotalAmount.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M503_BatchTotalCount))
                {
                    strPackage.Append("Total Count: " + packet.M503_BatchTotalCount.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M504_BatchSaleAmount))
                {
                    strPackage.Append("Sale Amount: " + packet.M504_BatchSaleAmount.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M505_BatchSaleCount))
                {
                    strPackage.Append("Sale Count: " + packet.M505_BatchSaleCount.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M506_BatchRefundAmount))
                {
                    strPackage.Append("Refund Amount: " + packet.M506_BatchRefundAmount.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M507_BatchRefundCount))
                {
                    strPackage.Append("Refund Count: " + packet.M507_BatchRefundCount.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M508_BatchVoidAmount))
                {
                    strPackage.Append("Void Amount: " + packet.M508_BatchVoidAmount.Substring(3) + ";");
                }
                if (!string.IsNullOrEmpty(packet.M509_BatchVoidCount))
                {
                    strPackage.Append("Void Count: " + packet.M509_BatchVoidCount.Substring(3) + ";");
                }

                if (!string.IsNullOrEmpty(packet.M900_Settlement))
                {
                    string[] Settlement = packet.M900_Settlement.Split(Convert.ToChar(29));
                    for (int i = 0; i < Settlement.Length; i++)
                    {
                        decimal Amount;
                        if (Settlement[i].Substring(0, 3) == "502")
                        {
                            if (decimal.TryParse(Settlement[i].Substring(3), out Amount))
                            {
                                Amount *= 0.01m;
                                strPackage.Append("Total Amount: $" + Amount.ToString() + ";");
                            }
                        }
                        if (Settlement[i].Substring(0, 3) == "503")
                        {
                            strPackage.Append("Total Count: " + Settlement[i].Substring(3) + ";");
                        }
                        if (Settlement[i].Substring(0, 3) == "504")
                        {
                            if (decimal.TryParse(Settlement[i].Substring(3), out Amount))
                            {
                                Amount *= 0.01m;
                                strPackage.Append("Sale Amount: $" + Amount.ToString() + ";");
                            }
                        }
                        if (Settlement[i].Substring(0, 3) == "505")
                        {
                            strPackage.Append("Sale Count: " + Settlement[i].Substring(3) + ";");
                        }
                        if (Settlement[i].Substring(0, 3) == "506")
                        {
                            if (decimal.TryParse(Settlement[i].Substring(3), out Amount))
                            {
                                Amount *= 0.01m;
                                strPackage.Append("Refund Amount: $" + Amount.ToString() + ";");
                            }
                        }
                        if (Settlement[i].Substring(0, 3) == "507")
                        {
                            strPackage.Append("Refund Count: " + Settlement[i].Substring(3) + ";");
                        }
                        if (Settlement[i].Substring(0, 3) == "508")
                        {
                            if (decimal.TryParse(Settlement[i].Substring(3), out Amount))
                            {
                                Amount *= 0.01m;
                                strPackage.Append("Void Amount: $" + Amount.ToString() + ";");
                            }
                        }
                        if (Settlement[i].Substring(0, 3) == "509")
                        {
                            strPackage.Append("Void Count: " + Settlement[i].Substring(3) + ";");
                        }

                    }
                }
                return strPackage.ToString();
            }

            private void GetPayType(string PacketCode)
            {
                if (PacketCode == "00") this.PaymentType = Global.PAYMENT_METHOD_Debit;
                else if (PacketCode == "01") this.PaymentType = Global.PAYMENT_METHOD_Visa;
                else if (PacketCode == "02") this.PaymentType = Global.PAYMENT_METHOD_MasterCard;
                else if (PacketCode == "03") this.PaymentType = Global.PAYMENT_METHOD_AE;
                else if (PacketCode == "04") this.PaymentType = Global.PAYMENT_METHOD_Diners_Club;
                else if (PacketCode == "05") this.PaymentType = Global.PAYMENT_METHOD_Discover_Card;
                else if (PacketCode == "06") this.PaymentType = Global.PAYMENT_METHOD_JCB;
                else if (PacketCode == "07") this.PaymentType = Global.PAYMENT_METHOD_UnionPay;
                else if (PacketCode == "08") this.PaymentType = Global.PAYMENT_METHOD_OtherCreditCard;
                else if (PacketCode == "09") this.PaymentType = Global.PAYMENT_METHOD_GiftCard;

            }

        }

    }
    public class MessagePacket
    {
        public string AllData;
        public string M000_MsgType;
        public string M001_TransAmount;
        public string M002_ECRTenderType;
        public string M003_ECRClerkID;
        public string M100_TransType;
        public string M101_TransStatus;
        public string M102_TransDate;
        public string M103_TransTime;
        public string M104_TransAmount;
        public string M105_TipAmount;
        public string M106_CashbackAmount;
        public string M107_SurchargeAmount;
        public string M108_TaxAmount;
        public string M109_TotalAmount;
        public string M110_InvoiceNo;
        public string M111_OrderNo;
        public string M112_RefNo;
        public string M113_TransSeq;
        public string M114_TableNo;
        public string M115_TicketNo;
        public string M116_VoucherNo;
        public string M117_ShiptoPostal;
        public string M118_ClerkID;
        public string M119_ClerkName;
        public string M120_NumberofGiftCards;
        public string M121_DCCOptInFlag;
        public string M122_DCCConversionRate;
        public string M123_DCCCurrencyAlpha;
        public string M124_DCCAmount;
        public string M125_DCCTipAmount;
        public string M126_DCCTotalAmount;
        public string M127_OriginalTransAmount;
        public string M128_OriginalTipAmount;
        public string M129_OriginalCashbackAmount;
        public string M130_OriginalSurchargeAmount;
        public string M131_OriginalTaxAmount;
        public string M132_OriginalTotalAmount;
        public string M133_GiftCardRef;
        public string M134_OrignalTransType;
        public string M135_NumberofCust;
        public string M300_CardType;
        public string M301_CardDescription;
        public string M302_AccountNo;
        public string M303_CardLanguage;
        public string M304_CardName;
        public string M305_AccountType;
        public string M306_CardEntryMode;
        public string M307_CustomerNumber;
        public string M308_EMVAID;
        public string M309_EMVTVR;
        public string M310_EMVTSI;
        public string M311_EMVName;
        public string M312_EMVdata;
        public string M313_CardNotPresent;
        public string M314_AmountDueType;
        public string M400_AuthNo;
        public string M401_ResponseCode;
        public string M402_ResponseText;
        public string M403_ResponseISOCode;
        public string M404_RetrievalReference;
        public string M405_AmountDue;
        public string M406_Trace;
        public string M407_AVSResult;
        public string M408_CVVResult;
        public string M409_CardBalance;
        public string M500_BatchNumber;
        public string M501_BatchFlag;
        public string M502_BatchTotalAmount;
        public string M503_BatchTotalCount;
        public string M504_BatchSaleAmount;
        public string M505_BatchSaleCount;
        public string M506_BatchRefundAmount;
        public string M507_BatchRefundCount;
        public string M508_BatchVoidAmount;
        public string M509_BatchVoidCount;
        public string M600_DemoMode;
        public string M601_TerminalID;
        public string M602_MCID;
        public string M603_CurrencyCode;
        public string M700_ReceiptHeader1;
        public string M701_ReceiptHeader2;
        public string M702_ReceiptHeader3;
        public string M703_ReceiptHeader4;
        public string M704_ReceiptHeader5;
        public string M705_ReceiptHeader6;
        public string M706_ReceiptHeader7;
        public string M707_ReceiptFooter1;
        public string M708_ReceiptFooter2;
        public string M709_ReceiptFooter3;
        public string M710_ReceiptFooter4;
        public string M711_ReceiptFooter5;
        public string M712_ReceiptFooter6;
        public string M713_ReceiptFooter7;
        public string M714_EndorsementLine1;
        public string M715_EndorsementLine2;
        public string M716_EndorsementLine3;
        public string M717_EndorsementLine4;
        public string M718_EndorsementLine5;
        public string M719_EndorsementLine6;
        public string M720_CustomerEndorsementLine1;
        public string M721_CustomerEndorsementLine2;
        public string M722_CustomerEndorsementLine3;
        public string M723_CustomerEndorsementLine4;
        public string M724_CustomerEndorsementLine5;
        public string M725_CustomerEndorsementLine6;
        public string M726_BlankTipLineIndicator;
        public string M727_TipAssistLine;
        public string M728_TaxRegistration;
        public string M729_TaxLabel;
        public string M900_Settlement;

    }
}
