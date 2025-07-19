using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using StoreApp3.Data.Abstract;
using StoreApp3.Entity;
using StoreApp3.Helpers;
using StoreApp3.Models;

namespace StoreApp3.Controllers
{
    public class OrderController : Controller
    {
        private Cart _cart;
        private IOrderRepository _orderRepository;

        public OrderController(Cart cartService, IOrderRepository orderRepository)
        {
            _cart = cartService;
            _orderRepository = orderRepository;
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            return View(new OrderModel() { Cart = cart });
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(OrderModel model)
        {
            var cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            if (!cart.Items.Any())
            {
                ModelState.AddModelError("", "Sepetinizde ürün yok.");
            }

            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Name = model.Name,
                    Email = model.Email,
                    City = model.City,
                    Phone = model.Phone,
                    AddressLine = model.AddressLine,
                    OrderDate = DateTime.Now,
                    OrderItems = cart.Items.Select(i => new StoreApp3.Entity.OrderItem
                    {
                        PostId = i.Product.ProductId,
                        Price = (double)i.Product.Price,
                        Quantity = i.Quantity
                    }).ToList()
                };

                model.Cart = cart;
                var payment = await ProcessPayment(model);

                if (payment.Status == "success")
                {
                    _orderRepository.AddOrder(order);
                    cart.Clear();
                    HttpContext.Session.SetJson("cart", cart);
                    return RedirectToAction("/Completed", new { orderId = order.Id });
                }
                model.Cart = cart;
                return View(model);

            }

            model.Cart = cart;
            return View(model);
        }

        public IActionResult Completed(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
        private async Task<Payment> ProcessPayment(OrderModel model)
        {
            return await Task.Run(async () =>
            {
                Options options = new Options
                {
                    ApiKey = "<api_key>",
                    SecretKey = "<api_secret>",
                    BaseUrl = "https://sandbox-api.iyzipay.com"
                };

                CreatePaymentRequest request = new CreatePaymentRequest
                {
                    Locale = Locale.TR.ToString(),
                    ConversationId = new Random().Next(111111111,999999999).ToString(),
                    Price = model?.Cart?.CalculateTotal().ToString(),
                    PaidPrice =  model?.Cart?.CalculateTotal().ToString(),
                    Currency = Currency.TRY.ToString(),
                    Installment = 1,
                    BasketId = "B67832",
                    PaymentChannel = PaymentChannel.WEB.ToString(),
                    PaymentGroup = PaymentGroup.PRODUCT.ToString()
                };

                PaymentCard paymentCard = new PaymentCard
                {
                    CardHolderName = model?.CartName,
                    CardNumber = model?.CartNumber,
                    ExpireMonth = model?.ExpirationMonth,
                    ExpireYear = model?.ExpirationYear,
                    Cvc = model?.Cvc,
                    RegisterCard = 0
                };
                request.PaymentCard = paymentCard;

                Buyer buyer = new Buyer
                {
                    Id = "BY789",
                    Name = "Ömer",
                    Surname = "Apaydın",
                    GsmNumber = "+905350000000",
                    Email = "email@email.com",
                    IdentityNumber = "74300864791",
                    LastLoginDate = "2015-10-05 12:43:35",
                    RegistrationDate = "2013-04-21 15:12:09",
                    RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                    Ip = "85.34.78.112",
                    City = "Istanbul",
                    Country = "Turkey",
                    ZipCode = "34732"
                };
                request.Buyer = buyer;

                Address shippingAddress = new Address
                {
                    ContactName = "Jane Doe",
                    City = "Istanbul",
                    Country = "Turkey",
                    Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                    ZipCode = "34742"
                };
                request.ShippingAddress = shippingAddress;

                Address billingAddress = new Address
                {
                    ContactName = "Jane Doe",
                    City = "Istanbul",
                    Country = "Turkey",
                    Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                    ZipCode = "34742"
                };
                request.BillingAddress = billingAddress;

                List<BasketItem> basketItems = new List<BasketItem>();
                

                    foreach (var item in model?.Cart?.Items ?? Enumerable.Empty<CartItem>())
                    {
                        BasketItem firstbasketItem = new BasketItem();
                        
                            firstbasketItem.Id = item.Product.ProductId.ToString();
                            firstbasketItem.Name = item.Product.Title;
                            firstbasketItem.Category1 = "Elektronik";
                            firstbasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                            firstbasketItem.Price = item.Product.Price.ToString();
                        
                            basketItems.Add(firstbasketItem);
                    }


                
                
                
                request.BasketItems = basketItems;

                Payment payment =await Payment.Create(request, options);
                return payment;
            });
        }

    }
}