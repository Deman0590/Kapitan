//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Documents.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cars
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cars()
        {
            this.documents = new HashSet<documents>();
        }
    
        public int CarId { get; set; }
        public string CarNumber { get; set; }
        public string StateNumber { get; set; }
        public string ChassisNumber { get; set; }
        public Nullable<int> BranchListId { get; set; }
        public int DateRelease { get; set; }
        public Nullable<int> BrandListId { get; set; }
        public string ModelId { get; set; }
        public Nullable<int> EcologicalClassListId { get; set; }
        public Nullable<int> TypeCarListId { get; set; }
        public Nullable<int> TypeFreightListId { get; set; }
        public Nullable<int> OwnershipListId { get; set; }
        public Nullable<bool> DepositId { get; set; }
        public string OwnershipName { get; set; }
        public Nullable<System.DateTime> OwnershipDateFrom { get; set; }
        public Nullable<System.DateTime> OwnershipDateTo { get; set; }
        public string OwnershipNote { get; set; }
        public string TrailerCarNumber { get; set; }
        public string TrailerStateNumber { get; set; }
        public Nullable<int> Massa { get; set; }
        public Nullable<int> MaxLoad { get; set; }
        public Nullable<int> GearboxListId { get; set; }
        public Nullable<int> AmountFuelTank { get; set; }
        public Nullable<bool> LeftFuelTank { get; set; }
        public Nullable<int> LeftFuelTankMax { get; set; }
        public Nullable<int> LeftFuelTankMin { get; set; }
        public Nullable<bool> RightFuelTank { get; set; }
        public Nullable<int> RightFuelTankMax { get; set; }
        public Nullable<int> RightFuelTankMin { get; set; }
        public Nullable<bool> OtherFuelTank { get; set; }
        public Nullable<int> OtherFuelTankMax { get; set; }
        public Nullable<int> OtherFuelTankMin { get; set; }
        public Nullable<bool> AdBlue { get; set; }
        public Nullable<int> AdBlueFuelTankMax { get; set; }
        public string NoteTank { get; set; }
        public Nullable<int> VolumeEngine { get; set; }
        public Nullable<int> PowerEngine { get; set; }
        public Nullable<int> TypeFuelListId { get; set; }
        public Nullable<int> HeightFifthWheelListId { get; set; }
        public Nullable<double> LoadFifthWheel { get; set; }
        public Nullable<int> AmountAxis { get; set; }
        public string NoteAxis { get; set; }
        public Nullable<int> AmountBattery { get; set; }
        public string NoteBattery { get; set; }
        public string Color { get; set; }
        public string Tachograph { get; set; }
        public Nullable<bool> ADR { get; set; }
        public Nullable<int> Generator { get; set; }
        public Nullable<int> Battery { get; set; }
        public Nullable<double> Ratio { get; set; }
        public Nullable<int> BrandGearboxListId { get; set; }
        public Nullable<int> Mileage { get; set; }
        public Nullable<System.DateTime> DateMileage { get; set; }
        public Nullable<int> GroupCarId { get; set; }
        public string Note { get; set; }
        public Nullable<int> StatusCarListId { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreateUserName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateUserName { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<bool> Check { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<documents> documents { get; set; }
    }
}
