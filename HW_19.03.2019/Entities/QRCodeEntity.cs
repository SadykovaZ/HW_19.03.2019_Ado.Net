﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_19._03._2019.Entities
{
    public enum QrCodeType
    {
        TextEncodedQrCode,
        LocationEncodedQrCode
    }
    public class QRCodeEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] Content { get; set; }
        public QrCodeType QrCodeType { get; set; }
    }
}
