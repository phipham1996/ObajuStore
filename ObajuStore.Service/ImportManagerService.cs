using ObajuStore.Common.Exceptions;
using ObajuStore.Common.Extensions;
using ObajuStore.Model.Models;
using ObajuStore.Service.ExportImport.Help;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;

namespace ObajuStore.Service.ExportImport
{
    public interface IImportManagerService
    {
        void ImportProductsFromXlsx(Stream stream);
    }

    public class ImportManagerService : IImportManagerService
    {
        private IProductService _productService;

        public ImportManagerService(IProductService productService)
        {
            _productService = productService;
        }

        public void ImportProductsFromXlsx(Stream stream)
        {
            #region properties

            var properties = new[]
            {
                new PropertyByName<Product>("ID"),
               new PropertyByName<Product>("Name"),
               new PropertyByName<Product>("Alias"),
               new PropertyByName<Product>("CategoryID"),
               new PropertyByName<Product>("Image"),
               new PropertyByName<Product>("MoreImages"),
               new PropertyByName<Product>("Price"),
               new PropertyByName<Product>("OriginalPrice"),
               new PropertyByName<Product>("PromotionPrice"),
               new PropertyByName<Product>("Warranty"),
               new PropertyByName<Product>("Description"),
               new PropertyByName<Product>("Content"),
                new PropertyByName<Product>("HotFlag"),
               new PropertyByName<Product>("HomeFlag"),
               new PropertyByName<Product>("ViewCount"),
               new PropertyByName<Product>("Quantity"),
               new PropertyByName<Product>("BrandID"),
               new PropertyByName<Product>("Tags"),
               new PropertyByName<Product>("MetaDescription"),
               new PropertyByName<Product>("MetaKeyword"),
               new PropertyByName<Product>("CreatedDate"),
               new PropertyByName<Product>("CreatedBy"),
               new PropertyByName<Product>("Status")
            };

            #endregion properties

            var manager = new PropertyManager<Product>(properties);
            using (var xlPackage = new ExcelPackage(stream))
            {
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new WorkSheetNotFoundException("No worksheet found");

                var iRow = 2;

                while (true)
                {
                    var allColumnsAreEmpty = manager.GetProperties
                        .Select(property => worksheet.Cells[iRow, property.PropertyOrderPosition])
                        .All(cell => cell == null || cell.Value == null || String.IsNullOrEmpty(cell.Value.ToString()));

                    if (allColumnsAreEmpty)
                        break;

                    manager.ReadFromXlsx(worksheet, iRow);

                    var product = _productService.GetByID(manager.GetProperty("ID").IntValue);
                    var isNew = product == null;
                    product = product ?? new Product();

                    product.Name = manager.GetProperty("Name").StringValue;
                    product.Alias = manager.GetProperty("Alias").StringValue;
                    product.CategoryID = manager.GetProperty("CategoryID").IntValue;
                    product.Image = manager.GetProperty("Image").StringValue;
                    product.MoreImages = manager.GetProperty("MoreImages").StringValue;
                    product.Price = manager.GetProperty("Price").DecimalValue;
                    product.OriginalPrice = manager.GetProperty("OriginalPrice").DecimalValue;
                    product.PromotionPrice = manager.GetProperty("PromotionPrice").DecimalValue;
                    product.Warranty = manager.GetProperty("Warranty").IntValue;
                    product.Description = manager.GetProperty("Description").StringValue;
                    product.Content = manager.GetProperty("Content").StringValue;
                    product.HotFlag = manager.GetProperty("HotFlag").BooleanValue;
                    product.HomeFlag = manager.GetProperty("HomeFlag").BooleanValue;
                    product.ViewCount = manager.GetProperty("ViewCount").IntValue;
                    product.Quantity = manager.GetProperty("Quantity").IntValue;
                    product.BrandID = manager.GetProperty("BrandID").IntValue;
                    product.Tags = manager.GetProperty("Tags").StringValue;
                    product.MetaDescription = manager.GetProperty("MetaDescription").StringValue;
                    product.MetaKeyword = manager.GetProperty("MetaKeyword").StringValue;
                    product.CreatedDate = DateTime.Now;
                    product.CreatedBy = manager.GetProperty("CreatedBy").StringValue;
                    product.Status = manager.GetProperty("Status").BooleanValue;

                    if (isNew)
                    {
                        try
                        {
                            _productService.Add(product);
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            product.UpdatedBy = manager.GetProperty("CreatedBy").StringValue;
                            product.UpdatedDate = DateTime.Now;
                            _productService.Update(product);
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                    }
                    iRow++;
                    _productService.SaveChanges();
                }
            }
        }
    }
}