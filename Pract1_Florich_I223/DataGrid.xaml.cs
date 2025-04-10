﻿using System;
using System.Linq;
using System.Windows;
using Pract1_Florich_I223.Logic;
using Pract1_Florich_I223.dbContext;

namespace Pract1_Florich_I223
{
    public partial class ProductDataGrid : Window
    {
        private ShopDBEntities1 _dbContext = new ShopDBEntities1();

        public ProductDataGrid()
        {
            InitializeComponent();
            LoadData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsDataGrid.SelectedItem as Products;

            if (selectedProduct != null)
            {
                var editWindow = new AddEditProductWindow(selectedProduct, _dbContext);

                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsDataGrid.SelectedItem as Products;

            if (selectedProduct != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _dbContext.Products.Remove(selectedProduct);
                        _dbContext.SaveChanges();

                        LoadData();
                        MessageBox.Show("Товар удален");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new AddEditProductWindow(null, _dbContext);

            if (editWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                var products = _dbContext.Products.ToList();
                ProductsDataGrid.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
