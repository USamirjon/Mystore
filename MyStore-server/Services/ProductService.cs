using AutoMapper;
using MyStore.DTOs;
using MyStore.Repositories;
using MyStore.Models; 
namespace MyStore.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
            return null;
        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        product.CreatedAt = DateTime.UtcNow; 
        var created = await _productRepository.AddAsync(product);
        return _mapper.Map<ProductResponseDto>(created);
    }

    public async Task<bool> UpdateProductAsync(int id, ProductCreateDto dto)
    {
        
        if (!await _productRepository.ExistsAsync(id))
            return false;

        
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
            return false; 

        _mapper.Map(dto, existingProduct); 

        
        await _productRepository.UpdateAsync(existingProduct);

        return true;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        if (!await _productRepository.ExistsAsync(id))
            return false;
        
        await _productRepository.DeleteAsync(id);
        
        return true;
    }
}