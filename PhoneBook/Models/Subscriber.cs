using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Subscriber
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона", AllowEmptyStrings = false)]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Укажите 'ФИО / Наименование' абонента", AllowEmptyStrings = false)]
        [Display(Name = "ФИО / Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите адрес абонента", AllowEmptyStrings = false)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите тип абонента")]
        [Display(Name = "Тип")]
        public SubscriberType? SubscriberType { get; set; }

        [Display(Name = "Картинка")]
        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }
    }

    public enum SubscriberType: byte
    {
        [Display(Name = "физ.лицо")]
        NaturalPerson = 1,
        [Display(Name = "юр.лицо")]
        ArtificialPerson = 2
    }
}