using MyStore.DTOs;

namespace MyStore.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();

    Task<ProductResponseDto> GetProductByIdAsync(int id);

    Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto);

    Task<bool> UpdateProductAsync(int id, ProductCreateDto dto);

    Task<bool> DeleteProductAsync(int id);
}