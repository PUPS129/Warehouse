﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Warehouse.View.Order.MakeOrder;

namespace Warehouse.ViewModel.Order
{
    using System.Windows.Interactivity;
    using Microsoft.VisualBasic;
    using Model;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Runtime.CompilerServices;
    using System.Windows.Documents;
    using System.Windows.Input;

    public class OrderViewModel : INotifyPropertyChanged
    {
        Warehouse.ApplicationContext db;
        RelayCommand makeOrderCommand;
        RelayCommand isItemChecked;
        RelayCommand isItemUnChecked;


        IEnumerable<OrderList>  orderLists;
        IEnumerable<UserLoginPass> userLoginPasses;
        IEnumerable<Client> clients;
        IEnumerable<ProductList> productLists;

        public List<object> CheckedItemsList = new List<object>();


        //class IsChecked
        //{

        //}
        //ProductList IsChecked = new ProductList();

        //public List<IsChecked> isCheckeds = new List<IsChecked>();

        //private bool isChecked;
        //public bool IsChecked
        //{
        //    get { return isChecked; }
        //    set
        //    {
        //        if (isChecked == value) return;
        //        {
        //            isChecked = value;
        //            OnPropertyChanged("IsChecked");
        //        }
        //    }
        //}


        public IEnumerable<ProductList> ProductLists
        {
            get { return productLists; }
            set
            {
                productLists = value;
                OnPropertyChanged("OrderLists");
            }
        }

        public IEnumerable<OrderList> OrderLists
        {
            get { return orderLists; }
            set
            {
                orderLists = value;
                OnPropertyChanged("OrderLists");
            }
        }

        public IEnumerable<UserLoginPass> UserLoginPasses
        {
            get { return userLoginPasses; }
            set
            {
                userLoginPasses = value;
                OnPropertyChanged("UserLoginPasses");
            }
        }

        public IEnumerable<Client> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public OrderViewModel()
        {
            db = new Warehouse.ApplicationContext();
            OrderLists = db.OrderLists.Local.ToBindingList();
            db.UserLoginPasses.ToList();
            db.OrderLists.ToList();
            
        }

        private bool _IsCheckedI = false;
        public bool IsCheckedI
        {
            get => _IsCheckedI;
            set
            {
                _IsCheckedI = value;
                OnPropertyChanged(nameof(IsCheckedI));
            }

        }


        public RelayCommand IsItemChecked
        {
            get
            {
                return isItemChecked ??
                    (isItemChecked = new RelayCommand((selectedItem) =>
                    {
                        //LoginWindow makeOrderWindow = new LoginWindow();
                        //makeOrderWindow.Show();
                        if (selectedItem == null) return;
                        if (selectedItem != null)
                        {
                            ProductList product = (ProductList)selectedItem;
                            CheckedItemsList.Add(selectedItem);
                        }
                        IsCheckedI = true;
                    }));
            }
        }

        public RelayCommand IsItemUnChecked
        {
            get
            {
                return isItemUnChecked ??
                    (isItemUnChecked = new RelayCommand((selectedItem) =>
                    {
                        //LoginWindow makeOrderWindow = new LoginWindow();
                        //makeOrderWindow.Show();
                        if (selectedItem == null) return;
                        if (selectedItem != null)
                        {
                            ProductList product = (ProductList)selectedItem;
                            CheckedItemsList.Remove(selectedItem);
                        }
                        IsCheckedI = false;
                    }));
            }
        }


        public RelayCommand MakeOrderCommand
        {
            get
            {
                return makeOrderCommand ??
                    (makeOrderCommand = new RelayCommand((selectedItem) =>
                    {
                        MakeOrderWindow makeOrderWindow = new MakeOrderWindow(new OrderList());

                        if (makeOrderWindow.ShowDialog() == true)
                        {
                            if (selectedItem != null)
                            {
                                OrderList orderList = new OrderList()
                                {
                                    ClientID = db.Clients.First(cl => cl.Name == makeOrderWindow.ClientCmbBx.SelectedValue.ToString()).ClientID,
                                    ManagerID = 2,
                                    OrderDate = DateTime.Today,
                                };
                                db.OrderLists.Add(orderList);

                                foreach (ProductList content in db.ProductLists)
                                {
                                    //content.IsChecked.

                                    if (content.IsChecked == true && content.Amount > 0)
                                    {
                                        OrderContent contentToAdd = new OrderContent()
                                        {
                                            ProductListID = content.ProductListID,
                                            OrderListID = orderList.OrderListID,
                                            ProductAmount = 2,
                                        };
                                        db.OrderContents.Add(contentToAdd);
                                    }
                                }
                                db.SaveChanges();
                            }
                            //else
                            //{
                            //    LoginWindow maeOrderWindow = new LoginWindow();
                            //    maeOrderWindow.Show();
                            //}

                            //OrderList orderList = makeOrderWindow.OrderList;
                            //orderList.TotalPrice = 0;
                        }
                    }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

//ProductList productList = selectedItem as ProductList;

//ProductList productList1 = new ProductList()
//{
//    ProductListID = productList.ProductListID,
//    Amount = productList.Amount,
//};
//ChangeProductAmountWindow updateProductList = new ChangeProductAmountWindow(productList);

//if (updateProductList.ShowDialog() == true)
//{
//    productList = db.ProductLists.Find(updateProductList.ProductList.ProductListID);
//    if (productList != null)
//    {
//        productList.Amount = updateProductList.ProductList.Amount;

//        db.Entry(productList).State = EntityState.Modified;
//        db.SaveChanges();
//    }
//}