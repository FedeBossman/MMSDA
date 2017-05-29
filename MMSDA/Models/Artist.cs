using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMSDA.Models
{
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }
        public string Name { get; set; }

        [Display(Name = "# of songs")]
        public int SongsCount
        {
            get {
                int songs = 0;
                
                foreach (Album album in Albums)
                {
                    songs += album.Songs.Count;
                }
                return songs;
            }
        }

        public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
        
    }
}