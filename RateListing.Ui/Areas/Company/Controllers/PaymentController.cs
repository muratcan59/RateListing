using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNet.Identity;
using RateListing.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateListing.Ui.Areas.Company.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        public ActionResult Step1()
        {
            var user = bUser.GetById(User.Identity.GetUserId());

            if (!user.isInformationConfirmed || user.isPaymentConfirmed)
            {
                return Redirect("/Company/Dashboard");
            }

            return View();
        }

        public ActionResult Step2(Buyer buyer, Address billingAddress)
        {
            var userId = User.Identity.GetUserId();
            var user = bUser.GetById(userId);

            if (!user.isInformationConfirmed || user.isPaymentConfirmed)
            {
                return Redirect("/Company/Dashboard");
            }

            var packageInfo = bUser.GetPacgageInfo(userId);
            var host = "https://www.ratelisting.com/";

            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
            request.Locale = Locale.TR.ToString();
            //request.ConversationId = userId;
            request.Price = packageInfo.GrandTotal.ToString().Replace(",", ".");
            request.PaidPrice = packageInfo.GrandTotal.ToString().Replace(",", ".");
            request.Currency = Currency.TRY.ToString();
            //request.BasketId = "B67832";
            //request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = host + "/Company/Payment/Step3";

            List<int> enabledInstallments = new List<int>();
            enabledInstallments.Add(2);
            enabledInstallments.Add(3);
            enabledInstallments.Add(6);
            enabledInstallments.Add(9);
            request.EnabledInstallments = enabledInstallments;


            buyer.Id = userId;
            buyer.Ip = GetIp();
            buyer.Country = "Turkey";
            request.Buyer = buyer;

            billingAddress.Country = "Turkey";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = packageInfo.package.Id.ToString();
            firstBasketItem.Name = packageInfo.package.Name;
            firstBasketItem.Category1 = "Paket";
            //firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            firstBasketItem.Price = packageInfo.GrandTotal.ToString().Replace(",", ".");
            basketItems.Add(firstBasketItem);

            request.BasketItems = basketItems;

            var options = new Iyzipay.Options()
            {
                ApiKey = "645Q5YWPvGhv1bu3RMsOr17LPgpyeObN",
                SecretKey = "Jy1Epy5pPFDcEfge3Vfvquq4A8HJkHzO",
                BaseUrl = "https://api.iyzipay.com"
            };

            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
            ViewBag.IyzipayScript = checkoutFormInitialize.CheckoutFormContent;

            var payment = new RateListing.Model.Payment
            {
                CreateDate = DateTime.Now,
                Date = DateTime.Now,
                iyzipay_token = checkoutFormInitialize.Token,
                paymentDesc = packageInfo.package.Name,
                userId = user.Id,
                price = packageInfo.GrandTotal,
            };
            bPayment.Add(payment);
            return View();
        }

        public ActionResult Step3(string token)
        {
            RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
            request.ConversationId = "123456789";
            request.Token = token;

            var options = new Iyzipay.Options()
            {
                ApiKey = "645Q5YWPvGhv1bu3RMsOr17LPgpyeObN",
                SecretKey = "Jy1Epy5pPFDcEfge3Vfvquq4A8HJkHzO",
                BaseUrl = "https://api.iyzipay.com"
            };

            CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, options);
            var user = bUser.GetById(User.Identity.GetUserId());
            var payment = bPayment.GetByToken(token);

            if (checkoutForm.PaymentStatus == "SUCCESS")
            {
                payment.isSuccess = true;
                payment.iyzipay_paymentId = checkoutForm.PaymentId;
                bPayment.Update(payment);

                user.isPaymentConfirmed = true;
                bUser.Update(user);
            }
            else
            {
                payment.errorCode = checkoutForm.ErrorCode;
                payment.errorMessage = checkoutForm.ErrorMessage;
                bPayment.Update(payment);
            }
            return View(checkoutForm);
        }

        public static string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
    }
}