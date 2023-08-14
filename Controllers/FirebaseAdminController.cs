using Microsoft.AspNetCore.Mvc;
using net_core_api_push_notification_demo.Models;
using System.Net.Http.Json;
using System.Net.Http;
using System;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;
using System.Threading.Tasks;

namespace net_core_api_push_notification_demo.Controllers
{

    [Route("api/firebaseadmin")]
    [ApiController]
    public class FirebaseAdminController : ControllerBase
    {


        [Route("adminfirebasesendnotification")]
        [HttpPost]
        public async Task<IActionResult> adminFirebaseSendNotificationAsync(NotificationModel notificationModel)
        {

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("~/Firebase_key/google-services.json"),
            });

            // Construct the message payload
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Test Notification",
                    Body = "This is a test notification"
                },
                Token = "your_device_token",
                //Topic = ""
            };

            // Send the message
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            return Ok(notificationModel);
        }


    }
}
