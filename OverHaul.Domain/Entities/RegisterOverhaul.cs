using Overhaul.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Overhaul.Domain.Entities;

public class RegisterOverhaul : Entity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new int Id { get; set; }
    public string FirstName { get; set; }
   
    public string LastName { get; set; }
   
    public string FatherName { get; set; }
   
    public string Nationality { get; set; }
   
    public string shSh { get; set; }
   
    public DateOnly BirthDay { get; set; }
   
    public string? BankCardNo { get; set; }
   
    public string? IBAN { get; set; }

    public string? SSN { get; set; }

   
    public string Mobile { get; set; }

    public string Tell { get; set; }
      
    public string PostalCode { get; set; }
     
    public string Address { get; set; }
    public string Degree { get; set; }
    public string Major { get; set; }
    public bool State { get; set; }
    public int MilitaryCardTypeId { get; set; }
    public int IsargariTypeId { get; set; }
    public int MaritalStatusTypeId { get; set; }
    public virtual MilitaryCardType MilitaryCardType { get; set; }
    public virtual IsargariType IsargariType { get; set; }
    public virtual MaritalStatusType MaritalStatusType { get; set; }
}


