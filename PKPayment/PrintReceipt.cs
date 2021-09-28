using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data;
//using RomeStation.ReceiptAndReportDataSet;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PKPayment
{
    class PrintItem
    {
        private string text;
        private Image img;
        public int Type { get; set; } //0 text; 1 image; 2 line
        public Font TextFont { get; set; }
        public int Align { get; set; } //0 left; 1 right; 2 center
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color TextColor { get; set; }
        public bool ForTextSender { get; set; }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                img = null;
            }
        }

        public Image imgPic
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
                text = "";
            }
        }
    }

    public class ReceiptPrintUtl
    {
        PrintDocument PrintContent = new PrintDocument();
        List<PrintItem> PrList = new List<PrintItem>();
        public static int PaperWidth;
        public ReceiptPrintUtl()
        {
            PrintContent.PrintPage += new PrintPageEventHandler(PrintContent_PrintPage);
            PrintContent.PrinterSettings.PrintToFile = false;
            PaperWidth = PrintContent.PrinterSettings.DefaultPageSettings.Margins.Right - PrintContent.PrinterSettings.DefaultPageSettings.Margins.Left;
            try
            {
                string PrinterName;
                PrinterName = System.Configuration.ConfigurationManager.AppSettings["Printer"];
                PrinterSettings.StringCollection PrinterList = PrinterSettings.InstalledPrinters;
                for (int i = 0; i < PrinterList.Count; i++)
                {
                    if (PrinterList[i] == PrinterName)
                    {
                        PrintContent.PrinterSettings.PrinterName = PrinterName;
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        public void ClearPrinterData()
        {
            PrList.Clear();
        }
        public void LastRecorderForTextSender()
        {
            if (PrList.Count > 0)
            {
                PrList[PrList.Count - 1].ForTextSender = true;
            }
        }
        public void AddTextToPrint(string Text, int X, int Y, int Width, Font font, Color color, int Align = 0)
        {
            try
            {
                PrintItem PrintItms = new PrintItem();
                PrintItms.Text = Text;
                PrintItms.TextFont = font;
                PrintItms.TextColor = color;
                PrintItms.PositionX = X;
                PrintItms.PositionY = Y;
                PrintItms.Width = Width;
                PrintItms.Align = Align;
                PrintItms.Type = 0;
                PrintItms.ForTextSender = false;
                PrList.Add(PrintItms);
            }
            catch { }
        }

        public void AddMultiLineTextToPrint(string Text, int X, int Y, int Width, Font font, Color color, int Align = 0)
        {
            try
            {
                PrintItem PrintItms = new PrintItem();
                PrintItms.Text = Text;
                PrintItms.TextFont = font;
                PrintItms.TextColor = color;
                PrintItms.PositionX = X;
                PrintItms.PositionY = Y;
                PrintItms.Width = Width;
                PrintItms.Align = Align;
                PrintItms.Type = 10;
                PrintItms.ForTextSender = false;
                PrList.Add(PrintItms);
            }
            catch { }
        }
        public void AddBarcodeToPrint(string Barcode, int X, int Y, int Width, int Height, int Align = 0)
        {
            try
            {
                PrintItem PrintItms = new PrintItem();

                PrintItms.imgPic = Code128Rendering.MakeBarcodeImage(Barcode, 1, false);
                PrintItms.PositionX = X;
                PrintItms.PositionY = Y;
                PrintItms.Width = Width;
                PrintItms.Height = Height;
                PrintItms.Align = Align;
                PrintItms.Type = 1;
                PrintItms.ForTextSender = false;
                PrList.Add(PrintItms);
            }
            catch { }

        }
        public void AddImgToPrint(Image Img, int X, int Y, int Width, int Height, int Align = 0)
        {
            try
            {
                PrintItem PrintItms = new PrintItem();
                PrintItms.imgPic = Img;
                PrintItms.PositionX = X;
                PrintItms.PositionY = Y;
                PrintItms.Align = Align;
                PrintItms.Type = 1;
                PrintItms.ForTextSender = false;
                PrList.Add(PrintItms);
            }
            catch { }
        }

        public void AddImgBytesToPrint(Byte[] BytesImg, int X, int Y, int Width, int Height, int Align = 0)
        {
            try
            {
                PrintItem PrintItms = new PrintItem();
                MemoryStream ms = new MemoryStream(BytesImg);
                PrintItms.imgPic = Image.FromStream(ms);
                PrintItms.PositionX = X;
                PrintItms.PositionY = Y;
                PrintItms.Width = Width;
                PrintItms.Height = Height;
                PrintItms.Align = Align;
                PrintItms.Type = 1;
                PrintItms.ForTextSender = false;
                PrList.Add(PrintItms);
            }
            catch { }
        }

        public void AddLine(int Y, int LineType = 2)
        {
            try
            {
                PrintItem PrintItms = new PrintItem();
                PrintItms.PositionY = Y;
                PrintItms.Type = LineType;
                PrintItms.ForTextSender = false;
                PrList.Add(PrintItms);
            }
            catch { }
        }

        public void PrintPreview(Graphics g, int XPostion)
        {
            SizeF sizef;
            int PageBoundWidth = 300;


            foreach (PrintItem Pri in PrList)
            {
                if (Pri.Type == 1) //image print
                {
                    int X = Pri.PositionX + XPostion;
                    if (Pri.Width <= 0) Pri.Width = PageBoundWidth + Pri.Width;
                    if (Pri.Align == 2) //need to be print at center position
                    {
                        X = (int)((PageBoundWidth - Pri.Width) * 0.5) + XPostion;
                        if (X < 0) X = 0;
                    }
                    g.DrawImage(Pri.imgPic, X, Pri.PositionY, Pri.Width, Pri.Height);
                }
                else if (Pri.Type == 0) //text print
                {
                    int X = Pri.PositionX + XPostion;
                    sizef = g.MeasureString(Pri.Text, Pri.TextFont);
                    if (Pri.Align == 2) //need to be print at center position
                    {
                        X = (int)((PageBoundWidth - sizef.Width) * 0.5) + XPostion;
                        if (X < 0) X = 0;
                    }
                    else if (Pri.Align == 1) //right align
                    {
                        X = (int)(PageBoundWidth - sizef.Width - 5 - X) + XPostion;
                        if (X < 0) X = 0;
                    }
                    if (Pri.Width == 0) Pri.Width = (int)sizef.Width + 5;
                    if (Pri.Width > PageBoundWidth) Pri.Width = PageBoundWidth;
                    RectangleF layoutRectangle = new RectangleF(X, Pri.PositionY, Pri.Width, sizef.Height);
                    g.DrawString(Pri.Text, Pri.TextFont, new SolidBrush(Pri.TextColor), layoutRectangle);
                }
                else if (Pri.Type == 10) //text print
                {
                    sizef = g.MeasureString(Pri.Text, Pri.TextFont, new SizeF(PageBoundWidth, 5000));

                    if (Pri.Width == 0) Pri.Width = (int)sizef.Width + 5;
                    if (Pri.Width > PageBoundWidth) Pri.Width = PageBoundWidth;
                    RectangleF layoutRectangle = new RectangleF(Pri.PositionX + XPostion, Pri.PositionY, Pri.Width, sizef.Height);
                    g.DrawString(Pri.Text, Pri.TextFont, new SolidBrush(Pri.TextColor), layoutRectangle);
                }
                else if (Pri.Type == 2 || Pri.Type == 3 || Pri.Type == 4) //line print
                {
                    Pen pen = new Pen(Color.Black);
                    if (Pri.Type == 2)
                    {
                        pen.Width = 2;
                    }
                    else if (Pri.Type == 3)
                    {
                        pen.Width = 1;
                    }
                    else
                    {
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                        pen.Width = 1;
                    }
                    g.DrawLine(pen, 0 + XPostion, Pri.PositionY, PageBoundWidth, Pri.PositionY);
                }
            }
        }

        public void Print()
        {
            PrintContent.PrintController = new System.Drawing.Printing.StandardPrintController();
            PrintContent.Print();
        }


        void PrintContent_PrintPage(object sender, PrintPageEventArgs e)
        {
            SizeF sizef;
            int PageBoundWidth = 283;

            foreach (PrintItem Pri in PrList)
            {
                if (Pri.Type == 1) //image print
                {
                    int X = Pri.PositionX;
                    if (Pri.Width <= 0) Pri.Width = PageBoundWidth + Pri.Width;
                    if (Pri.Align == 2) //need to be print at center position
                    {
                        X = (int)((PageBoundWidth - Pri.Width) * 0.5);
                        if (X < 0) X = 0;
                    }
                    e.Graphics.DrawImage(Pri.imgPic, X, Pri.PositionY, Pri.Width, Pri.Height);
                }
                else if (Pri.Type == 0) //text print
                {
                    int X = Pri.PositionX;
                    sizef = e.Graphics.MeasureString(Pri.Text, Pri.TextFont);
                    if (Pri.Align == 2) //need to be print at center position
                    {
                        X = (int)((PageBoundWidth - sizef.Width) * 0.5);
                        if (X < 0) X = 0;
                    }
                    else if (Pri.Align == 1) //right align
                    {
                        X = (int)(PageBoundWidth - sizef.Width - 5 - X);
                        if (X < 0) X = 0;
                    }
                    if (Pri.Width == 0) Pri.Width = (int)sizef.Width + 5;
                    if (Pri.Width > PageBoundWidth) Pri.Width = PageBoundWidth;
                    RectangleF layoutRectangle = new RectangleF(X, Pri.PositionY, Pri.Width, sizef.Height);
                    e.Graphics.DrawString(Pri.Text, Pri.TextFont, new SolidBrush(Pri.TextColor), layoutRectangle);
                }
                else if (Pri.Type == 10) //text print
                {
                    sizef = e.Graphics.MeasureString(Pri.Text, Pri.TextFont, new SizeF(PageBoundWidth, 5000));

                    if (Pri.Width == 0) Pri.Width = (int)sizef.Width + 5;
                    if (Pri.Width > PageBoundWidth) Pri.Width = PageBoundWidth;
                    RectangleF layoutRectangle = new RectangleF(Pri.PositionX, Pri.PositionY, Pri.Width, sizef.Height);
                    e.Graphics.DrawString(Pri.Text, Pri.TextFont, new SolidBrush(Pri.TextColor), layoutRectangle);
                }
                else if (Pri.Type == 2) //line print
                {
                    Pen pen = new Pen(Color.Black);
                    pen.Width = 2;
                    e.Graphics.DrawLine(pen, 0, Pri.PositionY, PageBoundWidth, Pri.PositionY);
                }
            }

            e.HasMorePages = false;
        }

    }

    class PrintReceipt
    {
        string ReceiptLanguage = "";
        const int AmountEdge = 15;
        public PrintReceipt(string Language)
        {
            ReceiptLanguage = Language;
        }


        public void PrintPayGatewaySettlement(string PayGateway, ReceiptPrintUtl ReceiptPrinter, ref int PositionY)
        {
            if (!string.IsNullOrEmpty(PayGateway))
            {
                ReceiptPrinter.AddTextToPrint("Settlement", 0, PositionY, 0, new Font("Arial", 12), Color.Black, 2);
                PositionY += 25;

                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "TransDate:", 11) + " " + FindSubItem(PayGateway, "TransTime:", 11),
                    140, PositionY, 0, new Font("Arial", 8), Color.Black, 1);
                PositionY += 16;
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "MID:", 0), 0, PositionY, 0, new Font("Arial", 10), Color.Black);
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "TID:", 0), 0, PositionY, 0, new Font("Arial", 10), Color.Black, 1);
                PositionY += 16;
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Batch #:", 0), 0, PositionY, 0, new Font("Arial", 10), Color.Black);
                PositionY += 20;

                ReceiptPrinter.AddTextToPrint("Sale:   ", 0, PositionY, 0, new Font("Arial", 10), Color.Black);
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Sale Count:", 12), 100, PositionY, 0, new Font("Arial", 10), Color.Black);
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Sale Amount:", 13), 150, PositionY, 0, new Font("Arial", 10), Color.Black);
                PositionY += 18;
                ReceiptPrinter.AddTextToPrint("Refund: ", 0, PositionY, 0, new Font("Arial", 10), Color.Black);
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Refund Count:", 14), 100, PositionY, 0, new Font("Arial", 10), Color.Black);
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Refund Amount:", 15), 150, PositionY, 0, new Font("Arial", 10), Color.Black);
                PositionY += 18;
                ReceiptPrinter.AddTextToPrint("Void:   ", 0, PositionY, 0, new Font("Arial", 10), Color.Black);
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Void Count:", 12), 100, PositionY, 0, new Font("Arial", 10), Color.Black);
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Void Amount:", 13), 150, PositionY, 0, new Font("Arial", 10), Color.Black);
                PositionY += 18;
                ReceiptPrinter.AddTextToPrint("Total:  ", 0, PositionY, 0, new Font("Arial", 10), Color.Black);
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Total Count:", 13), 100, PositionY, 0, new Font("Arial", 10), Color.Black);
                ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Total Amount:", 14), 150, PositionY, 0, new Font("Arial", 10), Color.Black);
                PositionY += 18;
            }
        }
        public void PrintPaymentGatewayDetail(string PayGateway, ReceiptPrintUtl ReceiptPrinter, int PositionY, bool MerReceipt, bool approved = true)
        {
            bool English = true;
            try
            {
                if (!string.IsNullOrEmpty(PayGateway))
                {
                    if (!MerReceipt && FindSubItem(PayGateway, "Lang:", 6) == "1") English = false;

                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Header1:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Header1:", 9), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        PositionY += 16;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Header2:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Header2:", 9), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        PositionY += 16;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Header3:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Header3:", 9), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        PositionY += 16;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Header4:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Header4:", 9), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        PositionY += 16;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Header5:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Header5:", 9), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        PositionY += 16;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Header6:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Header6:", 9), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        PositionY += 16;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Header7:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Header7:", 9), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        PositionY += 16;
                    }
                    PositionY += 5;

                    ReceiptPrinter.AddLine(PositionY);
                    PositionY += 12;


                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Demo:", 0)))
                    {
                        if (English)
                            ReceiptPrinter.AddTextToPrint("DEMO MODE", 0, PositionY, 0, new Font("Arial", 10, FontStyle.Bold), Color.Black, 2);
                        else
                            ReceiptPrinter.AddTextToPrint("MODE DEMO", 0, PositionY, 0, new Font("Arial", 10, FontStyle.Bold), Color.Black, 2);
                        PositionY += 20;
                    }
                    ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Transaction Type:", 18), 0, PositionY, 0, new Font("Arial", 10, FontStyle.Bold), Color.Black);
                    ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "OrgTransaction Type:", 21), 40, PositionY, 0, new Font("Arial", 10, FontStyle.Bold), Color.Black);
                    ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "TransDate:", 11) + " " + FindSubItem(PayGateway, "TransTime:", 11),
                        0, PositionY, 0, new Font("Arial", 8), Color.Black, 1);
                    //ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "TransTime:", 11), 200, PositionY, 0, new Font("Arial", 8), Color.Black);
                    PositionY += 18;

                    if (English)
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "MID:", 0), 0, PositionY, 0, new Font("Arial", 8), Color.Black);
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "TID:", 0), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 1);
                        PositionY += 16;
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Batch #:", 0), 0, PositionY, 0, new Font("Arial", 8), Color.Black);
                        PositionY += 16;

                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Auth#:", 0), 0, PositionY, 0, new Font("Arial", 8), Color.Black);
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "REF#:", 0), 130, PositionY, 0, new Font("Arial", 8), Color.Black, 1);
                        PositionY += 16;
                    }
                    else
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "IDM:", 0), 0, PositionY, 0, new Font("Arial", 8), Color.Black);
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "IDT:", 0), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 1);
                        PositionY += 16;
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "No Lot:", 0), 0, PositionY, 0, new Font("Arial", 8), Color.Black);
                        PositionY += 16;

                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "CODE APPR:", 0), 0, PositionY, 0, new Font("Arial", 8), Color.Black);
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "No. REF:", 0), 130, PositionY, 0, new Font("Arial", 8), Color.Black, 1);
                        PositionY += 16;
                    }
                    ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "CardDescription:", 17), 0, PositionY, 0, new Font("Arial", 8), Color.Black);
                    ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "AccountNo:", 11) + FindSubItem(PayGateway, "Entry Mode:", 12), 95, PositionY, 0, new Font("Arial", 8), Color.Black);
                    ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Account Type:", 14), 5, PositionY, 0, new Font("Arial", 8), Color.Black, 1);
                    //if (FindSubItem(PayGateway, "TipLine:", 9) == "1" || FindSubItem(PayGateway, "CVM:", 5) == "2" || FindSubItem(PayGateway, "CVM:", 5) == "3")
                    //{
                    //    ReceiptPrinter.AddTextToPrint("**/**", 0, PositionY, 0, new Font("Arial", 8), Color.Black, 1);
                    //}
                    PositionY += 16;
                    //ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Entry Mode:", 12), 0, PositionY, 0, new Font("Arial", 8), Color.Black);
                    //ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Account Type:", 14), 100, PositionY, 0, new Font("Arial", 8), Color.Black);
                    //PositionY += 16;

                    string tipAmount = FindSubItem(PayGateway, "TipAmount:", 11);
                    if (decimal.Parse(tipAmount, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat) != 0)
                    {
                        if (English)
                            ReceiptPrinter.AddTextToPrint("Amount:", 0, PositionY, 0, new Font("Arial", 10.5f, FontStyle.Bold), Color.Black);
                        else
                            ReceiptPrinter.AddTextToPrint("Montant:", 0, PositionY, 0, new Font("Arial", 10.5f, FontStyle.Bold), Color.Black);
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Amount:", 8), 0, PositionY, 0, new Font("Arial", 10.5f, FontStyle.Bold), Color.Black, 1);
                        PositionY += 22;

                        if (English)
                            ReceiptPrinter.AddTextToPrint("Tips:", 0, PositionY, 0, new Font("Arial", 10.5f, FontStyle.Bold), Color.Black);
                        else
                            ReceiptPrinter.AddTextToPrint("conseils:", 0, PositionY, 0, new Font("Arial", 10.5f, FontStyle.Bold), Color.Black);
                        ReceiptPrinter.AddTextToPrint(tipAmount, 0, PositionY, 0, new Font("Arial", 10.5f, FontStyle.Bold), Color.Black, 1);
                        PositionY += 22;
                    }
                    ReceiptPrinter.AddTextToPrint("Total:", 0, PositionY, 0, new Font("Arial", 10.5f, FontStyle.Bold), Color.Black);
                    ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Total Amount:", 14), 0, PositionY, 0, new Font("Arial", 10.5f, FontStyle.Bold), Color.Black, 1);
                    PositionY += 22;

                    string Response = FindSubItem(PayGateway, "Response ISO Code:", 19) + " - ";
                    Response += FindSubItem(PayGateway, "Response Text:", 15) + " - ";
                    Response += FindSubItem(PayGateway, "Response Code:", 15);
                    ReceiptPrinter.AddTextToPrint(Response, 50, PositionY, 0, new Font("Arial", 9), Color.Black, 2);
                    PositionY += 16;

                    //                    if (MerReceipt)
                    //                    {
                    if (FindSubItem(PayGateway, "TipLine:", 9) == "1" || FindSubItem(PayGateway, "CVM:", 5) == "2" || FindSubItem(PayGateway, "CVM:", 5) == "3")
                    {
                        PositionY += 25;
                        if (string.IsNullOrEmpty(FindSubItem(PayGateway, "Demo:", 0)))
                        {
                            ReceiptPrinter.AddTextToPrint(" x________________________________________", 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        }
                        else
                        {
                            ReceiptPrinter.AddTextToPrint(" x________________________________________", 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        }
                        PositionY += 25;
                    }
                    else
                    {
                        if (English)
                            ReceiptPrinter.AddTextToPrint("SIGNATURE NOT REQUIRED", 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        else
                            ReceiptPrinter.AddTextToPrint("SIGNATURE NON REQUISE", 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        PositionY += 20;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "EMVName:", 9)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "EMVName:", 9), 0, PositionY, 0, new Font("Arial", 9), Color.Black);
                        PositionY += 16;
                    }

                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "AID:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "AID:", 0), 0, PositionY, 0, new Font("Arial", 9), Color.Black);
                        PositionY += 16;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "TVR:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "TVR:", 0), 0, PositionY, 0, new Font("Arial", 9), Color.Black);
                        PositionY += 16;
                    }

                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "TSI:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "TSI:", 0), 0, PositionY, 0, new Font("Arial", 9), Color.Black);
                        PositionY += 25;
                    }

                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "EnLine1:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "EnLine1:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "EnLine2:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "EnLine2:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "EnLine3:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "EnLine3:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "EnLine4:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "EnLine4:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "EnLine5:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "EnLine5:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "EnLine6:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "EnLine6:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }

                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "CustEnLine1:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "CustEnLine1:", 13), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "CustEnLine2:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "CustEnLine2:", 13), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "CustEnLine3:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "CustEnLine3:", 13), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "CustEnLine4:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "CustEnLine4:", 13), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "CustEnLine5:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "CustEnLine5:", 13), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "CustEnLine6:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "CustEnLine6:", 13), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }

                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Footer1:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Footer1:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Footer2:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Footer2:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Footer3:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Footer3:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Footer4:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Footer4:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Footer5:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Footer5:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Footer6:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Footer6:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!string.IsNullOrEmpty(FindSubItem(PayGateway, "Footer7:", 0)))
                    {
                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Footer7:", 9), 0, PositionY, 0, new Font("Arial", 7.5f), Color.Black, 2);
                        PositionY += 15;
                    }
                    if (!approved)
                    {
                        if (English)
                            ReceiptPrinter.AddTextToPrint("TRANSACTION CANCELLED", 0, PositionY, 0, new Font("Arial", 10, FontStyle.Bold), Color.Black, 2);
                        else
                            ReceiptPrinter.AddTextToPrint("OPERATION ANNULEE", 0, PositionY, 0, new Font("Arial", 10, FontStyle.Bold), Color.Black, 2);
                        PositionY += 18;
                    }
                    if (MerReceipt)
                    {
                        if (English)
                            ReceiptPrinter.AddTextToPrint("MERCHANT COPY", 0, PositionY, 0, new Font("Arial", 8, FontStyle.Bold), Color.Black, 2);
                        else
                            ReceiptPrinter.AddTextToPrint("COPIE MARCHAND", 0, PositionY, 0, new Font("Arial", 8, FontStyle.Bold), Color.Black, 2);
                    }
                    else
                    {
                        if (English)
                            ReceiptPrinter.AddTextToPrint("CUSTOMER COPY", 0, PositionY, 0, new Font("Arial", 8, FontStyle.Bold), Color.Black, 2);
                        else
                            ReceiptPrinter.AddTextToPrint("COPIE CLIENT", 0, PositionY, 0, new Font("Arial", 8, FontStyle.Bold), Color.Black, 2);
                    }
                    PositionY += 18;

                    if (approved)
                    {
                        ReceiptPrinter.AddBarcodeToPrint(FindSubItem(PayGateway, "Auth#:", 7), 0, PositionY, 200, 25, 2);
                        PositionY += 30;

                        ReceiptPrinter.AddTextToPrint(FindSubItem(PayGateway, "Auth#:", 7), 0, PositionY, 0, new Font("Arial", 8), Color.Black, 2);
                        PositionY += 18;
                    }

                }
                ReceiptPrinter.Print();
            }
            catch { }
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
            catch { return ""; }
        }
    }


}
