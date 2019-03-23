using HW_19._03._2019.Repository;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HW_19._03._2019.Entities;
using static QRCoder.PayloadGenerator;

namespace HW_19._03._2019
{
    public class QRCodeGeneratorService
    {
        private readonly ProductRepository _productRepository;
        private readonly QrCodeRepository _qrCodeRepository;

        public void GetQrCodePurchaseInfo(int userId, int productId)
        {
            var productInfo = _productRepository.Read(productId);
            var userInfo = "Ruslan Z.";
            string purchaseInfoString =
                $"At {DateTime.Now.ToShortTimeString()} {userInfo} " +
                $"purchased {productInfo.ProductName}" + $" for {productInfo.Cost}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(purchaseInfoString, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            string pathToSave = ConfigurationManager.AppSettings["qrCodesOutputDirectory"];
            string fileName = $"{Guid.NewGuid().ToString()}.png";

            //qrCodeImage.Save(Path.Combine(pathToSave, fileName));
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                _qrCodeRepository.Add(new QRCodeEntity()
                {
                    UserId = userId,
                    QrCodeType = QrCodeType.TextEncodedQrCode,
                    Content = ms.ToArray()
                });
            }
        }

        public void GetQrCodeGeolocation(string latitude, string longtitude)
        {
            Geolocation generator = new Geolocation(latitude, longtitude);
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var qrCodeAsBitmap = qrCode.GetGraphic(20);

            
            //string pathToSave = ConfigurationManager.AppSettings["qrCodesOutputDirectory"];
            //string fileName = $"{Guid.NewGuid().ToString()}.png";           

            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeAsBitmap.Save(ms, ImageFormat.Png);
                _qrCodeRepository.AddGeo(new QRCodeEntity()
                {
                    UserId = 1,
                    QrCodeType = QrCodeType.TextEncodedQrCode,
                    Content = ms.ToArray()
                });
            }
        }

        public QRCodeGeneratorService()
        {
            _productRepository = new ProductRepository();
            _qrCodeRepository = new QrCodeRepository();
        }
    }
}
