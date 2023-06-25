using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticeWebApp1.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен быть минимум длиной в {2} символа", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

    [MetadataType(typeof(EventAttr))]
    public partial class Event
    {
    }

    public class EventAttr
    {
        /*public int idEvents { get; set; }
        [Required(ErrorMessage = "Please enter the news title.")]
        public string EventName { get; set; }
        public int Typeid { get; set; }
        public int Ageid { get; set; }
        public int Formid { get; set; }
        public string EventLink { get; set; }
        public string EventDesc { get; set; }*/
        public int idEvents { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название")]
        [Display(Name = "Название мероприятия")]
        public string EventName { get; set; }
        [Display(Name = "Тип мероприятия")]
        public int Typeid { get; set; }
        [Display(Name = "Возрастной рейтинг")]
        public int Ageid { get; set; }
        [Display(Name = "Форма проведения")]
        public int Formid { get; set; }
        [Display(Name = "Ссылка на источник")]
        public string EventLink { get; set; }
        [Display(Name = "Описание")]
        public string EventDesc { get; set; }
    }

    [MetadataType(typeof(EventFormAttr))]
    public partial class EventForm
    {

    }

    public class EventFormAttr
    {
        [Display(Name = "Форма проведения")]
        public int EventForm1 { get; set; }
    }

    [MetadataType(typeof(UsersDataAttr))]
    public partial class UsersData
    {
    }

    public class UsersDataAttr
    {
        [Display(Name = "Логин")]
        public int Login { get; set; }
        [Display(Name = "Пароль")]
        public int Password { get; set; }
    }

    [MetadataType(typeof(StageAttr))]
    public partial class Stage
    {
    }

    public class StageAttr
    {
        public int idStage { get; set; }
        [Required]
        [Display(Name = "Номер этапа")]

        public int StageNumber { get; set; }
        [Required]
        [Display(Name = "Мероприятие")]
        public int EventId { get; set; }
        [Required]
        [Display(Name = "Название этапа")]
        public string StageName { get; set; }
        [Required]
        [Display(Name = "Улица")]
        public int AdressId { get; set; }
        [Display(Name = "Дом")]
        public string House { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата и время начала")]
        [DisplayFormat(DataFormatString = "{0:d-MM-yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime DateStart { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата и время конца")]
        [DisplayFormat(DataFormatString = "{0:d-MM-yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        //DataFormatString = "{0:d-MM-yyyyTHH:mm}",
        public System.DateTime DateFinish { get; set; }
        [Display(Name = "Стоимость")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public Nullable<double> StageCost { get; set; }
        [Display(Name = "Описание этапа")]
        public string StageDesc { get; set; }
    }

    [MetadataType(typeof(MemberAttr))]
    public partial class Member
    {
    }

    public class MemberAttr
    {
        public int idMember { get; set; }
        [Required]
        [Display(Name = "Фамилия участника")]
        public string MemberSurname { get; set; }
        [Display(Name = "Имя участника")]
        public string MemberName { get; set; }
        [Display(Name = "Отчество участника")]
        public string MemberOtch { get; set; }
        [Required]
        [Display(Name = "Тип участника")]
        public int MemberTypeId { get; set; }
        [Display(Name = "Описание участника")]
        public string MemberDesc { get; set; }
        [Display(Name = "Ссылка")]
        public string MemberLink { get; set; }
    }

    [MetadataType(typeof(ManagerAttr))]
    public partial class Manager
    {
    }

    public class ManagerAttr
    {
        public int idManager { get; set; }
        [Required]
        [Display(Name = "Фамилия организатора")]
        public string ManagerSurname { get; set; }
        [Display(Name = "Имя организатора")]
        public string ManagerName { get; set; }
        [Display(Name = "Отчество организатора")]
        public string ManagerOtch { get; set; }
        [Required]
        [Display(Name = "Тип организатора")]
        public int ManagerTypeid { get; set; }
        [Display(Name = "Ссылка")]
        public string ManagerLink { get; set; }
        [Display(Name = "Описание организатора")]
        public string ManagerDesc { get; set; }
    }

    [MetadataType(typeof(AdressAttr))]
    public partial class Adress
    {
    }

    public class AdressAttr
    {
        public int idAdress { get; set; }
        [Required]
        [Display(Name = "Название улицы")]
        public string Adress1 { get; set; }
    }
}

