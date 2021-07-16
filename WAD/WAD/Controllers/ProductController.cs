﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WAD.Context;
using WAD.Models;

namespace WAD.Controllers
{
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Product
        public ActionResult Index(string search, string sortOrder, string categoryId)
        {
            ViewBag.CategoryId = 0;
            //var products = db.Products.Include(p => p.Category);
            string sort = !String.IsNullOrEmpty(sortOrder) ? sortOrder : "asc";
            // Cach 1 dung thang dbSet-- su dung duoc relationship
            /*if (!String.IsNullOrEmpty(search) && sort.Equals("asc"))
            {
                var products = db.Products.Where(p => p.Name.Contains(search)).OrderBy(p => p.Name).ToList();
                return View(products.OrderBy(p => p.Name).ToList());
            }
            else if (!String.IsNullOrEmpty(search) && sort.Equals("desc"))
            {
                var products = db.Products.Where(p => p.Name.Contains(search)).OrderByDescending(p => p.Name).ToList();
                return View(products.OrderByDescending(p => p.Name).ToList());
            } else
            {
                return View(db.Products.ToList());
            }*/

            // Cach 2 dung db Raw - tim trong 1 bang
            var products = from p in db.Products select p;
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }
            switch (sort)
            {
                case "asc": products = products.OrderBy(p => p.Name); break;
                case "desc": products = products.OrderByDescending(p => p.Name); break;
            }

            // Loc theo Category
            // products = products.Where(p => p.CategoryID == 1);
            if (!String.IsNullOrEmpty(categoryId))
            {
                var catId = Convert.ToInt32(categoryId);
                products = products.Where(p => p.CategoryID == catId);
                ViewBag.CategoryID = catId;
            }

            var categories = db.Categories.ToList();
            var brands = db.Brands.ToList();
            dynamic data = new ExpandoObject();
            data.Products = products;
            data.Categories = categories;
            data.Brands = brands;
            return View(data);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult AddToCart(int? id, int? qty)
        {
            try
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                // them vao gio hang
                CartItem item = new CartItem(product, (int)qty);
                // lay gio hang tu Session
                Cart cart = (Cart)Session["Cart"];
                if (cart == null)
                {
                    Customer customer = new Customer("Nguyen Van An", "0975712398", "So 8 Ton That Thuyet");
                    cart = new Cart();
                    cart.Customer = customer;
                }
                cart.AddToCart(item);
                Session["cart"] = cart; // them session
            } catch (Exception e)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult RemoveItem(int? id)
        {
            try
            {
                Cart cart = (Cart)Session["Cart"];
                if (cart == null)
                {
                    return HttpNotFound();
                }
                cart.RemoveItem((int) id);
                Session["cart"] = cart; // them session
                cart.CalculateGrandTotal();
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Cart");
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name");
            ViewBag.BrandID = new SelectList(db.Brands, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image,Price,Description,CategoryID,BrandID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product); // them 1 object Product vao danh sach
                db.SaveChanges(); // luu vao db cac thay doi du lieu
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", product.CategoryID);
            ViewBag.BrandID = new SelectList(db.Brands, "Id", "Name", product.BrandID);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", product.CategoryID);
            ViewBag.BrandID = new SelectList(db.Brands, "Id", "Name", product.BrandID);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,Price,Description,CategoryID,BrandID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified; // trang thai cua db la change (modified) chu khong phai new
                db.SaveChanges(); // luu thay doi vao db
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", product.CategoryID);
            ViewBag.BrandID = new SelectList(db.Brands, "Id", "Name", product.BrandID);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product); // xoa product khoi danh sach
            db.SaveChanges(); // luu vao db
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            if(ModelState.IsValid)
            {
                var cart = (Cart)Session["cart"];
                order.GrandTotal = cart.GrandTotal;
                order.CreatedAt = DateTime.Now;
                order.Status = 1;
                db.Orders.Add(order);
                db.SaveChanges();
                foreach (var item in cart.CartItems)
                {
                    OrderItem orderItem = new OrderItem() { OrderID = order.Id, ProductID = item.Product.Id, Qty = item.Quantity, Price = item.Product.Price };
                    db.OrderItems.Add(orderItem);
                }
                db.SaveChanges();
                Session["cart"] = null;
            }
            return RedirectToAction("CheckOutSuccess");
        }

        public string CheckoutSuccess()
        {
            return "Tạo đơn thành công ...";
        }
    }
}
