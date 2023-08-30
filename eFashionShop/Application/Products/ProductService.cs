using eFashionShop.Application.CloudinaryService;
using eFashionShop.Application.Images;
using eFashionShop.Data.EF;
using eFashionShop.Data.Entities;
using eFashionShop.Exceptions;
using eFashionShop.Extensions;
using eFashionShop.ViewModels.Catalog.Images;
using eFashionShop.ViewModels.Catalog.ProductImages;
using eFashionShop.ViewModels.Catalog.Products;
using eFashionShop.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eFashionShop.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly EShopDbContext _context;
        private readonly IImageService _imageService;

        public ProductService(EShopDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<int> Create(ProductCreateRequest req)
        {
            var product = new Product();

            req.CopyProperties(product);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            //Save image
            if (req.ThumbnailImage != null)
            {
                ImageCreateRedVm image = new ImageCreateRedVm();
                image.File = req.ThumbnailImage;
                await _imageService.AddImage(image, product.Id);
            }
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product: {productId}");

            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _imageService.DeleteImage(image.Id);
                _context.ProductImages.Remove(image);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductVm>> GetAllPaging(GetManageProductPagingRequest request)
        {
            var data = new List<ProductVm>();
            int totalRow = 0;
            if (request.CategoryId != null && request.CategoryId != 0)
            {
                //1. Select join
                var query = from p in _context.Products
                            join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                            from pic in ppic.DefaultIfEmpty()
                            join c in _context.Categories on pic.CategoryId equals c.Id into picc
                            from c in picc.DefaultIfEmpty()
                            select new { p, pic };
                //2. filter
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.p.Name.Contains(request.Keyword));

                //3. Paging
                totalRow = await query.CountAsync();

                data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ProductVm()
                    {
                        Id = x.p.Id,
                        DateCreated = x.p.DateCreated,
                        Name = x.p.Name,
                        Description = x.p.Description,
                        Details = x.p.Details,
                        IsFeatured = x.p.IsFeatured,
                        Trademark = x.p.Trademark,
                        ProductOrigin = x.p.ProductOrigin,
                        Ingredient = x.p.Ingredient,
                        Expiry = x.p.Expiry,
                        BlogDescription = x.p.BlogDescription,
                    }).ToListAsync();
            }
            else
            {
                //1. Select join
                var query = from p in _context.Products
                            select new { p };
                //2. filter
                if (!string.IsNullOrEmpty(request.Keyword))
                    query = query.Where(x => x.p.Name.Contains(request.Keyword));

                //3. Paging
                totalRow = await query.CountAsync();

                data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ProductVm()
                    {
                        Id = x.p.Id,
                        DateCreated = x.p.DateCreated,
                        Name = x.p.Name,
                        Description = x.p.Description,
                        Details = x.p.Details,
                        IsFeatured = x.p.IsFeatured,
                        Trademark = x.p.Trademark,
                        ProductOrigin = x.p.ProductOrigin,
                        Ingredient = x.p.Ingredient,
                        Expiry = x.p.Expiry,
                        BlogDescription = x.p.BlogDescription,
                    }).ToListAsync();
            }


            //4. Select and projection
            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }
        public async Task<List<ProductVm>> GetAll()
        {
            var data = new List<ProductVm>();
            //1. Select join
            var query = from p in _context.Products
                        from i in _context.ProductImages where (i.ProductId == p.Id && i.IsDefault == true)
                        select new { p, i };

            //3. Paging
            data = await query.Select(x => new ProductVm()
                {
                    Id = x.p.Id,
                    DateCreated = x.p.DateCreated,
                    Name = x.p.Name,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    IsFeatured = x.p.IsFeatured,
                    Trademark = x.p.Trademark,
                    ProductOrigin = x.p.ProductOrigin,
                    Ingredient = x.p.Ingredient,
                    Expiry = x.p.Expiry,
                    BlogDescription = x.p.BlogDescription,
                }).ToListAsync();
            return data;
        }
        public async Task<ProductVm> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            var categories = await _context.ProductInCategories.Where(x => x.ProductId == productId).Select(x => x.CategoryId).ToListAsync();

            var image = await _context.ProductImages.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();

            var productViewModel = new ProductVm()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = product != null ? product.Description : null,
                Details = product != null ? product.Details : null,
                Name = product != null ? product.Name : null,
                Trademark = product != null ? product.Trademark : null,
                ProductOrigin = product != null ? product.ProductOrigin : null,
                Ingredient = product != null ? product.Ingredient : null,
                Expiry = product != null ? product.Expiry : null,
                BlogDescription = product != null ? product.BlogDescription : null,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg",
                IsFeatured = product.IsFeatured,
                Categories = categories
            };
            return productViewModel;
        }
        public async Task<List<ProductVm>> GetAllByCategoriesId(int categoriesId)
        {
            var data = new List<ProductVm>();
            //list produc
            var producs = await _context.ProductInCategories.Where(x => x.CategoryId == categoriesId).Select(x => x.Product).ToListAsync();

            if(producs.Any())
            {
                foreach( Product product in producs)
                {
                    var image = await _context.ProductImages.Where(i => i.ProductId == product.Id && i.IsDefault == true).FirstOrDefaultAsync();
                    data.Add(new ProductVm()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        DateCreated = product.DateCreated,
                        Description = product.Description,
                        Details = product.Details,
                        Trademark = product.Trademark,
                        ProductOrigin = product.ProductOrigin,
                        Ingredient = product.Ingredient,
                        Expiry = product.Expiry,
                        BlogDescription = product.BlogDescription,
                        ThumbnailImage = image != null ? image.ImagePath : "",
                    });
                }
            }
            return data;
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new EShopException($"Cannot find an image with id {imageId}");

            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new EShopException($"Cannot find an image with id {imageId}");
            _context.ProductImages.Remove(productImage);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                await _imageService.DeleteImage(productImage.Id);
                return result;
            };
            throw new EShopException($"Cannot find an image with id {imageId}");
        }

        public async Task<int> Update(ProductUpdateRequest req)
        {
            var product = await _context.Products.FindAsync(req.Id);

            if (product == null) throw new EShopException($"Cannot find a product with id: {req.Id}");

            req.CopyProperties(product);

            _context.Update(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var user = await _context.Products.FindAsync(id);
            if (user == null)
            {
                return new ApiErrorResult<bool>($"Sản phẩm với id {id} không tồn tại");
            }
            foreach (var category in request.Categories)
            {
                var productInCategory = await _context.ProductInCategories
                    .FirstOrDefaultAsync(x => x.CategoryId == int.Parse(category.Id)
                    && x.ProductId == id);
                if (productInCategory != null && category.Selected == false)
                {
                    _context.ProductInCategories.Remove(productInCategory);
                }
                else if (productInCategory == null && category.Selected)
                {
                    await _context.ProductInCategories.AddAsync(new ProductInCategory()
                    {
                        CategoryId = int.Parse(category.Id),
                        ProductId = id
                    });
                }
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<ProductFeatureVm>> GetFeaturedProducts(int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        where p.IsFeatured == true
                        from i in _context.ProductImages
                        where (i.ProductId == p.Id && i.IsDefault == true)
                        select new { p, i };

            var data = await query.Take(take).
                Select(x => new ProductFeatureVm()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Details = x.p.Details,
                    Description = x.p.Description,
                    Trademark = x.p.Trademark,
                    ProductOrigin = x.p.ProductOrigin,
                    Ingredient = x.p.Ingredient,
                    Expiry = x.p.Expiry,
                    BlogDescription = x.p.BlogDescription,
                    ImagePath = x.i.ImagePath,
                    IsFeatured = x.p.IsFeatured
                }).ToListAsync();

            return data;
        }

        public async Task<int> SetMainImage(int imageId, int productId)
        {
            var images = await this.GetListImagesByProductId(productId);
            for (int i = 0; i < images.Count; i++)
            {
                if (images[i].Id != imageId)
                {
                    images[i].IsDefault = false;
                    _context.ProductImages.Update(images[i]);
                }
                else
                {
                    images[i].IsDefault = true;
                    _context.ProductImages.Update(images[i]);
                }
            }
            return await _context.SaveChangesAsync();
        }

        private async Task<List<ProductImage>> GetListImagesByProductId(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}