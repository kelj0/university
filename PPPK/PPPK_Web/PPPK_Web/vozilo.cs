//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPPK_Web
{
    using PPPK_Web.CustomValidators;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class vozilo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vozilo()
        {
            this.putni_nalog = new HashSet<putni_nalog>();
            this.servis = new HashSet<servi>();
            this.zauzece_vozilo = new HashSet<zauzece_vozilo>();
        }

        public int id { get; set; }
        public Nullable<int> tip_vozila_id { get; set; }
        [Required(ErrorMessage = "Marka vozila je obavezno polje!")]
        public string marka { get; set; }
        [Required(ErrorMessage = "Godina proizvodnje je obavezno polje!")]
        [GodinaProizvodnjeValidator(ErrorMessage = "Neispravna godinja proizvodnje!")]
        public int godina_proizvodnje { get; set; }
        [Required(ErrorMessage = "Broj kilometara je obavezan")]
        [KilometriValidator(ErrorMessage = "Neispravan broj kilometara")]
        public decimal pocetni_km { get; set; }
        public decimal trenutni_km { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<putni_nalog> putni_nalog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servi> servis { get; set; }
        public virtual tip_vozila tip_vozila { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zauzece_vozilo> zauzece_vozilo { get; set; }
    }
}