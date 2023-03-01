using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAspNetCoreApp2.Web.Models
{
    public class Product
    {
       
        public int Id { get; set; }

        [Remote(action:"HasProductName", controller:"Products")]
        [StringLength(50, ErrorMessage ="isim alanına en fazla 50 karakter girilebilinir")]

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ürün isim alanını doldurmak zorunludur")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ürün fiyat alanını doldurmak zorunludur")]
        public decimal? Price { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ürün Stok alanını doldurmak zorunludur")]
        [System.ComponentModel.DataAnnotations.Range(1,2000, ErrorMessage = "Ürün Stok alanı 1 ile 2000 arasında bir değer olmalıdır")]
        public int? Stock { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ürün renk alanını doldurmak zorunludur")]
        public string Color { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ürün genişlik alanını doldurmak zorunludur")]
        public int? Width { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ürün yükseklik alanını doldurmak zorunludur")]
        public int? Height { get; set; }

        [ValidateNever]
        [NotMapped]
        public IFormFile Image { get; set; }
        // [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ürün resim alanını doldurmak zorunludur")]
        [ValidateNever]
        public string ImagePath { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Kategori seçiniz")]
        public int CategoryId { get; set; } //foreignkey
        public Category Category { get; set; }
        //public string CategoryName { get; set; } //?
        public bool IsPublish { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Expire { get; set; }
        [StringLength(80, MinimumLength =10, ErrorMessage = "Açıklama alanına 10 ile 80 karakter girilebilinir")]
        public string Description { get; set; }


    }
}
