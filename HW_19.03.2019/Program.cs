using HW_19._03._2019.Entities;
using HW_19._03._2019.Repository;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_19._03._2019
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("1. QR Code инфо покупателя/n2. QR Code геолокация");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        QRCodeGeneratorService qr = new QRCodeGeneratorService();
                        qr.GetQrCodePurchaseInfo(1, 1);

                        QrCodeRepository q = new QrCodeRepository();
                        QRCodeEntity e = q.Read(3);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            ms.Write(e.Content, 0, e.Content.Length);
                            Bitmap qrCodeImage = new Bitmap(ms);
                            qrCodeImage.Save(@"G:\\QR\Info.png");
                        }
                    }
                    break;
                case 2:
                    {
                        QRCodeGeneratorService qr = new QRCodeGeneratorService();

                        qr.GetQrCodeGeolocation("43.2565", "76.9285");

                        QrCodeRepository q = new QrCodeRepository();
                        QRCodeEntity e = q.ReadGeo(2);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            ms.Write(e.Content, 0, e.Content.Length);
                            Bitmap qrCodeImage = new Bitmap(ms);
                            qrCodeImage.Save(@"G:\\QR\Geolocation.png");
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
