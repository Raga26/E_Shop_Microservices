using Marten.Linq.QueryHandlers;

namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category): IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler(IDocumentSession session , ILogger<GetProductByCategoryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler handle called with {@Query}", query);

            var products = await session.Query<Product>()
                           .Where(p => p.Category.Contains(query.Category))
                           .ToListAsync();

            return new GetProductByCategoryResult(products);

        }
    }
}
