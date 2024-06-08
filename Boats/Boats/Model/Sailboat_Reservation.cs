using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Boats.Model;

[PrimaryKey(nameof(Sailboat), nameof(Reservation))]
public class Sailboat_Reservation
{
    [ForeignKey("IdSailboat")]
    public virtual int Sailboat { get; set; }
    
    [ForeignKey("IdReservation")]
    public virtual int Reservation { get; set; }
}