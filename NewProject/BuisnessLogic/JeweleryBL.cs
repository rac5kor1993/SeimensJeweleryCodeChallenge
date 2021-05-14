using NewProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NewProject.BuisnessLogic
{
    public class JeweleryBL : IJeweleryBL
    {
        private readonly IJeweleryBr _JewelryBr;

        public JeweleryBL(IJeweleryBr JewelryBr)
        {

            _JewelryBr = JewelryBr;
        }

        public bool GetUser(string username)
        {
            var res = _JewelryBr.GetUser(username);
            if (res.Name.Equals(username))
            {
                return true;
            }
            else
                return false;
        }

        public GoldPriceCalculation GetTotalGoldPrice(GoldPriceCalculation goldprice, string userrole)
        {
           // var userinfo = _JewelryBr.GetUserInfo(id);
            //GoldPriceCalculation goldPriceCalculation = new GoldPriceCalculation(goldprice);
            if (userrole== "Priveleged")
            {
                goldprice.Discount = 2;

            }
            goldprice.GetTotalValue(goldprice.Discount);

            return goldprice;
        }

        public MemoryStream CreatePdf(GoldPriceCalculation goldprice)
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("SamplePdf" + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document doc = new Document();
            doc.SetMargins(0, 0, 0, 0);
            //Create PDF Table with 5 columns  
            PdfPTable tableLayout = new PdfPTable(5);
            doc.SetMargins(0, 0, 0, 0);
            //Create PDF Table  

            //file will created in this path  
            string strAttachment = Path.Combine("~/Downloads/" + strPDFFileName);


            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(Add_Content_To_PDF(tableLayout,goldprice));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            return workStream;
        }
        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, GoldPriceCalculation goldprice)
        {

            float[] headers = { 50, 24, 45, 35, 50 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            //Add Title to the PDF file at the top  

           



            tableLayout.AddCell(new PdfPCell(new Phrase("Creating Pdf using ItextSharp", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });


            ////Add header  
           
            AddCellToHeader(tableLayout, "Gold Price(per gram)");
            AddCellToHeader(tableLayout, "Weight");
            AddCellToHeader(tableLayout, "Discount");
            AddCellToHeader(tableLayout, "Total Price");

            ////Add body  

            
                AddCellToBody(tableLayout, goldprice.GetGoldPrice.ToString());
                AddCellToBody(tableLayout, goldprice.GetWeight.ToString());
                AddCellToBody(tableLayout, goldprice.GetDiscount.ToString());
                AddCellToBody(tableLayout, goldprice.GetTotalPrice.ToString());
               

            

            return tableLayout;
        }
        // Method to add single cell to the Header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {

            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.YELLOW)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new iTextSharp.text.BaseColor(128, 0, 0)
            });
        }

        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }
    }
}
