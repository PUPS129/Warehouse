namespace Warehouse.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;

    [Table("Product")]
    public class Product : INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductList = new HashSet<ProductList>();
        }

        public int ProductID { get; set; }

        public string title;

        public string description;

        public string unitOfMeasurement;

        public decimal? priceForUnit;

        //[Column("Category")]
        public int category;

        [Required(ErrorMessage = "���� �� ����� ���� ������.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "��������� ��� ������� 5 ��������")]
        public string Title
        {
            get { return title; }
            set
            {
                //ValidateProperty(value, "Title");
                title = value;
                OnPropertyChanged("Title");
            }
        }

        [Required(ErrorMessage = "���� �� ����� ���� ������.")]
        public string Description
        {
            get { return description; }
            set
            {
                //ValidateProperty(value, "Description");
                description = value;
                OnPropertyChanged("Description");
            }
        }

        [Required(ErrorMessage = "���� �� ����� ���� ������.")]
        public string UnitOfMeasurement
        {
            get { return unitOfMeasurement; }
            set
            {
                //ValidateProperty(value, "UnitOfMeasurement");
                unitOfMeasurement = value;
                OnPropertyChanged("UnitOfMeasurement");
            }
        }

        [Required(ErrorMessage = "���� �� ����� ���� ������.")]
        [Range(0, int.MaxValue, ErrorMessage ="������� ���������")]
        public decimal? PriceForUnit
        {
            get { return priceForUnit; }
            set
            {
                ValidateProperty(value, "PriceForUnit");
                priceForUnit = value;
                OnPropertyChanged("PriceForUnit");
            }
        }

        [Column("Category")]
        public int CategoryId
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        [Column("Category")]
        public virtual Category Categories { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductList> ProductList { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name,
            });
        }
    }
}

