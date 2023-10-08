using System.ComponentModel;

namespace TheaterSchool.Models;

public partial class PhysicalPersons
{
    [DisplayName("Идентификатор физического лица")]
    public int ID { get; set; }

    [DisplayName("Фамилия")]
    public string LastName { get; set; }

    [DisplayName("Имя")]
    public string FirstName { get; set; }

    [DisplayName("Отчество")]
    public string SecondName { get; set; }

    [DisplayName("Дата рождения")]
    public DateOnly DateOfBirth { get; set; }

    [DisplayName("Телефон")]
    public string Phone { get; set; }

    [DisplayName("Пол")]
    public bool Sex { get; set; }

    [DisplayName("Адрес")]
    public string Address { get; set; }

    public virtual Student Student { get; set; }

    public virtual Teacher Teacher { get; set; }
}