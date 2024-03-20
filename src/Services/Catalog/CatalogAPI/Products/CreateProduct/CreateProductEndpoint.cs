
namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, List<string> Category, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
                 //incoming request to product command object ,
                 //send it using the mediatr and mediatr will trigger handler class
                 //will get back the result and adapt the result to create product response

                 app.MapPost("/products",
                 async (CreateProductRequest request, ISender sender) =>
                 {
                     var command = request.Adapt<CreateProductCommand>();

                     //sends to the controller
                     var result = await sender.Send(command);

                     var response = result.Adapt<CreateProductResponse>();

                     return Results.Created($"/products/{response.Id}", response);

                 })
             .WithName("CreateProduct")
             .Produces<CreateProductResponse>(StatusCodes.Status201Created)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Create Product")
             .WithDescription("Create Product");
        }
    }
}
