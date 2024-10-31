using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace eStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("CreateVNPayUrl")]
        public IActionResult CreateVNPayUrl(decimal amount)
        {
            string vnp_TmnCode = _configuration["VNPay:vnp_TmnCode"];
            string vnp_HashSecret = _configuration["VNPay:vnp_HashSecret"];
            string vnp_Url = _configuration["VNPay:vnp_Url"];
            string vnp_ReturnUrl = _configuration["VNPay:vnp_ReturnUrl"];

            var payUrl = CreatePaymentUrl(vnp_Url, vnp_TmnCode, vnp_HashSecret, vnp_ReturnUrl, amount);
            return Ok(new { payUrl });
        }

        private string CreatePaymentUrl(string vnp_Url, string vnp_TmnCode, string vnp_HashSecret, string vnp_ReturnUrl, decimal amount)
        {
            var vnp_Params = new Dictionary<string, string>
        {
            { "vnp_Version", "2.1.0" },
            { "vnp_Command", "pay" },
            { "vnp_TmnCode", vnp_TmnCode },
            { "vnp_Amount", ((int)(amount * 100)).ToString() },
            { "vnp_CurrCode", "VND" },
            { "vnp_TxnRef", DateTime.Now.Ticks.ToString() }, // Mã giao dịch
            { "vnp_OrderInfo", "Payment for order" },
            { "vnp_Locale", "vn" },
            { "vnp_ReturnUrl", vnp_ReturnUrl },
            { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
        };

            // Sắp xếp và ký tạo URL
            vnp_Params = vnp_Params.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            string queryString = string.Join("&", vnp_Params.Select(x => $"{x.Key}={x.Value}"));
            string vnp_SecureHash = HmacSHA512(vnp_HashSecret, queryString);
            return $"{vnp_Url}?{queryString}&vnp_SecureHash={vnp_SecureHash}";
        }

        private static string HmacSHA512(string key, string data)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
            }
        }

        [HttpGet("PaymentReturn")]
        public IActionResult PaymentReturn([FromQuery] Dictionary<string, string> vnpayData)
        {
            // Bước 1: Xác thực vnp_SecureHash
            string vnp_HashSecret = _configuration["VNPay:vnp_HashSecret"];
            string vnp_SecureHash = vnpayData["vnp_SecureHash"];
            vnpayData.Remove("vnp_SecureHash"); // Loại bỏ vnp_SecureHash trước khi ký lại

            // Sắp xếp vnpayData theo key và tạo lại hash
            vnpayData = vnpayData.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            string hashData = string.Join("&", vnpayData.Select(kv => kv.Key + "=" + kv.Value));
            string checkHash = HmacSHA512(vnp_HashSecret, hashData);

            if (checkHash != vnp_SecureHash)
            {
                // Xác thực thất bại
                return BadRequest("Invalid checksum");
            }

            // Bước 2: Kiểm tra kết quả thanh toán
            string vnp_ResponseCode = vnpayData["vnp_ResponseCode"];
            if (vnp_ResponseCode == "00")
            {
                // Thanh toán thành công
                // Cập nhật trạng thái đơn hàng và xử lý logic
                return Ok("Payment successful");
            }
            else
            {
                // Thanh toán thất bại
                return BadRequest("Payment failed");
            }
        }
    }
}
