using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferencePlanner.GraphQL.Data
{
    public class Speaker
       {
           [Key]
           [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
           public int Id { get; set; }

           [Required]
           [StringLength(200)]
           public string? Name { get; set; }

           [StringLength(4000)]
           public string? Bio { get; set; }

           [StringLength(1000)]
           public string? WebSite { get; set; }

           public ICollection<SessionSpeaker> SessionSpeakers { get; set; } = 
               new List<SessionSpeaker>();
       }
   }