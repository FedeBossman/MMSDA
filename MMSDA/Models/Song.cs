using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMSDA.Models
{
    public class Song
    {

        [Key]  
        public int SongID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Lyrics { get; set; }
        public int Duration { get; set; }
        public string DurationString {
            get {
                TimeSpan Time = TimeSpan.FromSeconds(Duration);
                return Time.ToString(@"mm\:ss");
            }
        }

        public int? AlbumRef { get; set; }
        [ForeignKey("AlbumRef")]
        public virtual Album Album { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    }
}