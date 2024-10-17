namespace Ecommerce.Data
{
    public class ShoppingCart
    {
        public List<Cart> CartItems = new List<Cart>();
        public void AddToCart(Product product,int qty)
        {
            //var ifxist= CartItems.Any(p=>p.Product.Id==product.Id);
            var ifexist = CartItems.FirstOrDefault(p => p.Product.Id == product.Id);
            if (ifexist !=null ) {
                ifexist.Qty += qty;
            }
            else
            {
                CartItems.Add(new Cart { Product = product, Qty = qty });
            }
          
        }
        public void RemoveItem(int producId)
        {
            CartItems.RemoveAll(p => p.Product.Id == producId);
        }
        public double TotalPrice() { 
        return  CartItems.Sum(p=>p.Product.Price*p.Qty);
        }

    }
}
