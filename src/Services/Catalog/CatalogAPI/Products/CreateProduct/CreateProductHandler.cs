
namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name , string Description , List<string> Category , string ImageFile , decimal Price )
                                        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    //constructor injection is used here.
    internal class CreateProductCommandHandler(IDocumentSession session) 
                   : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create product entity from command object
            //save the database
            //return result

            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Category = command.Category,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateProductResult(product.Id);
        }
    }
}
