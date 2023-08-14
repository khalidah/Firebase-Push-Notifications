using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_core_api_push_notification_demo.Models;
using net_core_api_push_notification_demo.Services;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace net_core_api_push_notification_demo.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

        [Route("firebasesendnotification")]
        [HttpPost]
        public IActionResult FirebaseSendNotification(NotificationModel notificationModel)
        {

            using (HttpClient client = new HttpClient())

            {

                string TransactionID = "6334360607";

                string url = $"https://fcm.googleapis.com/fcm/send";

                //string url = $"http://103.11.137.17:8081/BillPayGW/BillInquiryService?shortcode=555&userid=dbbl&password=dbbl&txnid={TransactionID}";


                Uri baseUri = new Uri(url);



                //This is the key section  UserName Password    

                //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("dbill:dBILL!23");
                  //string base64EncodedAuthenticationString = System.Convert.ToBase64String(plainTextBytes);



                var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri);

               // requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

                requestMessage.Headers.Add("Content", "application/json");
                //requestMessage.Headers.Add("Authorization", "key=AAAAURtTO1I:APA91bE46TadXT9YXOogb4CPIF_mito-aTxqRN1s0ZlOjIuECLj9YrS5gdj87plXKP1CjRzcB53orr4TRgur5AdEzGYQ8GGe6Mmbjv1DRCZbPUNRH4QgBM8Yu-emjMWnJLnQuC98m1In");
                requestMessage.Headers.Add("Authorization", "bearer AAAAURtTO1I:APA91bE46TadXT9YXOogb4CPIF_mito-aTxqRN1s0ZlOjIuECLj9YrS5gdj87plXKP1CjRzcB53orr4TRgur5AdEzGYQ8GGe6Mmbjv1DRCZbPUNRH4QgBM8Yu-emjMWnJLnQuC98m1In");


                requestMessage.Content =
                    JsonContent.Create(new
                    {
                        to = "/topics/all",
                        notification = new
                        {
                            body = "Body of Your Notification",
                            title = "Title of Your Notification"
                        }
                    });

                //make the request

                var task = client.SendAsync(requestMessage);

                var response = task.Result;

                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;

            }


            //var result = await _notificationService.SendNotification(notificationModel);
            return Ok(notificationModel);
        }
    }
}
