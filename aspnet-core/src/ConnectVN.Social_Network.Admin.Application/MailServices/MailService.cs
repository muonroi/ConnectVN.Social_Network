using ConnectVN.Social_Network.Admin.Email;
using ConnectVN.Social_Network.Users;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConnectVN.Social_Network.Admin.DTO;
using Microsoft.Extensions.Options;
using Azure.Storage.Blobs;
using ConnectVN.Social_Network.Common.Settings;
using Microsoft.Extensions.Configuration;

namespace ConnectVN.Social_Network.Admin.MailServices
{
    public class MailService : IEmailService
    {
        private static IConfiguration _configuration;
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;
        public async Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Xin chào! {{UserName}}, Vui lòng xác nhận email của bạn", userEmailOptions.PlaceHolders);

            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirm"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }
        public MailService(IOptions<SMTPConfigModel> smtpConfig, IConfiguration configuration)
        {
            _smtpConfig = smtpConfig.Value;
            _configuration = configuration;
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new()
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new()
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }
        private static string GetEmailBody(string templateName)
        {
            string containerStr = "connectvnimages";//_configuration.GetSection($"Application:{Social_NetworkSettings.ENV_CONTAINERNAME}").Value;
            string connecttionStr = @"DefaultEndpointsProtocol=https;AccountName=connectvnimages;AccountKey=twrISSTrmAYmEaK7RQ+ZNZ9ZWSnDvPdu3IDHAgBawk0hLiBo45n0AHim/DDgitX6nXvpIUHiIu3w+AStoT7/jA==;EndpointSuffix=core.windows.net";//_configuration.GetSection($"Application:{Social_NetworkSettings.ENV_CONNECTIONSTRING}").Value;
            BlobServiceClient blobServiceClient = new(connecttionStr);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerStr);
            BlobClient blobClient = blobContainerClient.GetBlobClient(string.Format(templatePath, templateName));
            using MemoryStream memoryStream = new();
            blobClient.DownloadTo(memoryStream);
            memoryStream.Position = 0;
            string body = new StreamReader(memoryStream).ReadToEnd();
            return body;
        }
        private static string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (KeyValuePair<string, string> placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return text;
        }
        public Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions)
        {
            throw new NotImplementedException();
        }

        public Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            throw new NotImplementedException();
        }
    }
}
