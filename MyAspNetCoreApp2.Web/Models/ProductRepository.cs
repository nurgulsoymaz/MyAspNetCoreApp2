using MyAspNetCoreApp2.Web.Models;

namespace MyAspNetCoreApp2.Web.Models
{
    //repositoriy inde CREATE DELETE UPDATE işlemleri yapıldı.
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>()   //ilk bir list sınıfı oluşturuldu

        {
         new () { Id = 1, Name = "Kalem 1", Price = 100, Stock = 200 },
         new () { Id = 2, Name = "Kalem 2", Price = 200, Stock = 300 },
         new()  { Id = 3, Name = "Kalem 3", Price = 300, Stock = 400 },
        };
        public List<Product> GetAll() => _products; //tüm ürünleri döndürelim
        public void Add(Product newproduct) => _products.Add(newproduct); //product ekleme

        public void Remove(int id)  //delete 
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == id); //data kontrolü yap "hasProduct

            if (hasProduct == null) //data yoksa exception fırlat
            {
                throw new Exception($"Bu id({id})'ye sahip ürün bulunmamaktadır.");
            }
            _products.Remove(hasProduct);

        }

        public void Update(Product updateProduct) //update işlemi
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == updateProduct.Id); //güncelencek nesneyi bul

            if (hasProduct == null)
            {
                throw new Exception($"Bu id({updateProduct.Id})'ye sahip ürün bulunmamaktadır.");
            }

            hasProduct.Name= updateProduct.Name;   // var olan datayı güncelledik
            hasProduct.Price= updateProduct.Price;
            hasProduct.Stock= updateProduct.Stock;

            //güncellenen datayı tekrar verebilmem için bu datanın indexini bulmak lazım

            var index = _products.FindIndex( x => x.Id == updateProduct.Id ); 
            _products[index] = hasProduct;


        }







    }
}
