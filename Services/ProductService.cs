using AutoMapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.ImageModels;
using InventoryManagement.Models.MerchandiseModels;
using InventoryManagement.Models.ProductModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;
using Slugify;

namespace InventoryManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly ISlugHelper _slugHelper;

        public ProductService(DataContext context, 
            IMapper mapper, 
            IImageService imageService,
            ISlugHelper slugHelper)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
            _slugHelper = slugHelper;
        }

        public async Task<ServiceResponseModel<List<ProductViewModel>>> All()
        {
            var response = new ServiceResponseModel<List<ProductViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from m in _context.Merchandises
                            join w in _context.Warehouses on m.WarehouseId equals w.Id
                            join c in _context.Categories on m.CategoryId equals c.Id
                            select new { m, WarehouseName = w.Name, CategoryName = c.Name };

                var data = await query
                    .Select(x => new ProductViewModel
                    {
                        Id = x.m.Id.ToString(),
                        CategoryId = x.m.CategoryId.ToString(),
                        CategoryName = x.CategoryName,
                        WarehouseId = x.m.WarehouseId.ToString(),
                        WarehouseName = x.WarehouseName,
                        Name = x.m.Name,
                        Price = x.m.Price,
                        Quantity = x.m.Quantity,
                        Unit = x.m.Unit,
                        Description = x.m.Description,
                        Image = x.m.Image,
                        IsActive = x.m.IsActive,
                    }).ToListAsync();

                if(data != null)
                {
                    response.isSuccess = true;
                    response.data = data;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Create(CreateProductRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Lỗi hệ thống",
                isSuccess = false,
            };

            try
            {
                var product = new Merchandise()
                {
                    CategoryId = new Guid(request.CategoryId),
                    WarehouseId = new Guid(request.WarehouseId),
                    Name = request.Name,
                    Price = request.Price,
                    Quantity = request.Quantity,
                    Unit = request.Unit,
                    Description = request.Description,
                    IsActive = request.IsActive,
                };

                if(request.ImageUrl != null) 
                {
                    product.Image = request.ImageUrl;
                }

                if(request.Image != null)
                {
                    var image = new UploadImageModel()
                    {
                        File = request.Image,
                        FileName = _slugHelper.GenerateSlug(request.Name),
                        With = 1920,
                        Height = 1080,
                        Folder = "products"
                    };

                    var uploadResult = await _imageService.AddImageAsync(image);

                    if (!uploadResult.isSuccess)
                        return response;

                    product.Image = uploadResult.data.SecureUrl.AbsoluteUri;
                }

                _context.Merchandises.Add(product);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    await _imageService.DeleteImageAsync(_slugHelper.GenerateSlug(request.Name), "products");
                    return response;
                }

                response.Message = "Thêm mới thành công";
                response.isSuccess = true;

                return response;

            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Delete(string id)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Lỗi hệ thống",
                isSuccess = false,
            };

            try
            {
                var product = await _context.Merchandises.FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == ActiveEnum.Active);

                if (product == null)
                {
                    response.Message = "Không tìm thấy thông tin sản phẩm!";

                    return response;
                }

                product.IsActive = ActiveEnum.InActive;

                _context.Merchandises.Update(product);
                var saveData = await _context.SaveChangesAsync(true);

                if (saveData != 1)
                {
                    response.Message = "Có lỗi trong quá trình thực hiện, vui lòng thử lại sau!";
                    return response;
                }

                await _imageService.DeleteImageAsync(_slugHelper.GenerateSlug(product.Name), "products");

                response.isSuccess = true;
                response.Message = "Xóa sản phẩm thành công!";
                
                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<ProductViewModel>> Get(string id)
        {
            var response = new ServiceResponseModel<ProductViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from m in _context.Merchandises
                            where m.Id.ToString() == id && m.IsActive == ActiveEnum.Active
                            join w in _context.Warehouses on m.WarehouseId equals w.Id
                            join c in _context.Categories on m.CategoryId equals c.Id
                            select new { m, WarehouseName = w.Name, CategoryName = c.Name };

                var data = await query
                    .Select(x => new ProductViewModel
                    {
                        Id = x.m.Id.ToString(),
                        CategoryId = x.m.CategoryId.ToString(),
                        CategoryName = x.CategoryName,
                        WarehouseId = x.m.WarehouseId.ToString(),
                        WarehouseName = x.WarehouseName,
                        Name = x.m.Name,
                        Price = x.m.Price,
                        Quantity = x.m.Quantity,
                        Unit = x.m.Unit,
                        Description = x.m.Description,
                        Image = x.m.Image,
                        IsActive = x.m.IsActive,
                    }).FirstOrDefaultAsync();

                if (data != null)
                {
                    response.isSuccess = true;
                    response.data = data;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> Update(UpdateProductRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Lỗi hệ thống",
                isSuccess = false,
            };

            try
            {

                var product = await _context.Merchandises.FirstOrDefaultAsync(x => x.Id.ToString() == request.Id);

                if(product == null)
                {
                    response.Message = "Không tìm thấy thông tin sản phẩm!";
                    return response;
                }

                if(request.ImageFile != null)
                {
                    var image = new UploadImageModel()
                    {
                        File = request.ImageFile,
                        FileName = _slugHelper.GenerateSlug(request.Name),
                        With = 1920,
                        Height = 1080,
                        Folder = "products"
                    };

                    var uploadResult = await _imageService.AddImageAsync(image);

                    if (!uploadResult.isSuccess)
                        return response;

                    request.Image = uploadResult.data.SecureUrl.AbsoluteUri;

                    await _imageService.DeleteImageAsync(_slugHelper.GenerateSlug(product.Name), "products");
                }

                _mapper.Map(request, product);

                _context.Merchandises.Update(product);

                var result = await _context.SaveChangesAsync();

                if(result != 0)
                {
                    response.Message = "Cập nhật thành công";
                    response.isSuccess = true;
                }    

                return response;

            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> UpdateQuantityAsync(List<UpdateProductQuantityRequest> requests)
        {
            var response = new ServiceResponseModel<bool>()
            {
                isSuccess = false,
            };

            try
            {
                // Extracting the list of product IDs from requests
                var listProductId = requests.Select(x => x.Id).ToList();

                // Fetching products from the database
                var products = await _context.Merchandises
                    .Where(x => listProductId.Contains(x.Id.ToString()))
                    .ToListAsync();

                // Updating quantities based on requests
                foreach (var request in requests)
                {
                    var product = products.FirstOrDefault(p => p.Id.ToString() == request.Id);
                    if (product != null)
                    {
                        product.Quantity += request.Quantity;
                    }
                }

                // Updating products in the database
                _context.Merchandises.UpdateRange(products);
                var result = await _context.SaveChangesAsync();
                if (result != 0)
                    response.isSuccess = true;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }
    }
}
