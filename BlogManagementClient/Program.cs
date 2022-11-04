var postsService = Server.Host.Services.GetRequiredService<PostsService>();

// var categories = await productsService.GetProductsByCategory();
//
// foreach (var category in categories)
// {
//     Console.WriteLine($"Category: {category.Name}");
//     foreach (var product in category.Products)
//     {
//         Console.WriteLine($"  {product.Name} ({product.Quality}, {product.Color}) at {product.Price:C}");
//     }
// }
//
// var ordersService = Unicorns.Host.Services.GetRequiredService<PostsService>();
//
// foreach (var order in await ordersService.GetOrders())
// {
//     PrintOrder(order);
// }
//
// void PrintOrder(Order order)
// {
//     Console.WriteLine($"Order {order.Id} made by {order.Customer.Name} on {order.OrderedOn:D}");
//     foreach (var orderLine in order.OrderLines)
//     {
//         Console.WriteLine($"  {orderLine.Quantity} of {orderLine.Product.Name} at {orderLine.Product.Price:C}");
//     }
// }
