using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMSDA.Models
{
    public class Album
    {
        [Key]
        public int AlbumID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Duration
        {
            get
            {
                int TotalDuration = 0;
                foreach(Song s in Songs)
                {
                    TotalDuration += s.Duration;
                }
                TimeSpan Time = TimeSpan.FromSeconds(TotalDuration);
                return Time.ToString(@"mm\:ss");
            }
        }

        public int? ArtistRef { get; set; }
        [ForeignKey("ArtistRef")]
        public virtual Artist Artist { get; set; }

        public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}