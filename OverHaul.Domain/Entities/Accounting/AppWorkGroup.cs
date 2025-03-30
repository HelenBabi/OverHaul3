using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Domain.Common;

namespace Overhaul.Domain.Entities;

[Table(nameof(AppWorkGroup))]
public class AppWorkGroup : Entity,IRemovable
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [Required]
    [StringLength(300)]
    public string Url { get; set; }

    [Required]
    [StringLength(300)]
    public string DesktopPath { get; set; }
    public bool IsDeleted { get; set; }= false;
    public DateTime? DeletedAt { get; set; }
    public string? DeleterUserId { get; set; }
}
