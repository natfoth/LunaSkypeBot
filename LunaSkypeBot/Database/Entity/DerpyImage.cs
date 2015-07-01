using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LunaSkypeBot.Database.Entity
{
    public class DerpyImage : IEntity
    {
        [Key]
        public long Id { get; set; } //id_number
        [Index(IsUnique = true)]
        public long DerpyImageId { get; set; } //id_number
        public string IdHash { get; set; } //id
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DbUpdatedAt { get; set; } //LOCAL_ONLY
        //public List<object> duplicate_reports { get; set; }
        public string FileName { get; set; } //file_name
        public string Description { get; set; } //description
        public string Uploader { get; set; } //uploader
        public string Image { get; set; } //image
        public long Score { get; set; } //score
        public long Upvotes { get; set; } //upvotes
        public long Downvotes { get; set; } //downvotes
        public long Favourites { get; set; } //faves
        public long CommentCount { get; set; } //comment_count
        public string Tags { get; set; } //tags
        //public List<string> tag_ids { get; set; }
        public long Width { get; set; } //width
        public long Height { get; set; } //height
        public double AspectRatio { get; set; } //aspect_ratio
        public string OriginalFormat { get; set; } //original_format
        public string MimeType { get; set; } //mime_type
        public string Sha512Hash { get; set; } //sha512_hash
        public string OriginalSha512Hash { get; set; } //orig_sha512_hash
        public string SourceUrl { get; set; } //source_url
        public string License { get; set; } //license
        
        public long RepresentationsId { get; set; } //representations
        public virtual DerpyRepresentation Representations { get; set; } //representations
        public bool IsRendered { get; set; } //is_rendered
        public bool IsOptimized { get; set; } //is_optimized
        //public byte[] downloadedImage { get; set; }

        public Uri GetImageUri()
        {
            if (Image.StartsWith("//"))
                return new Uri("https:" + Image);

            return new Uri(Image);
        }

        public string[] GetTagArray()
        {
            return Tags.Split(','); 
        } 
    }

    public class DerpyRepresentation : IEntity
    {
        [Key]
        public long Id { get; set; } //id_number
        [Index(IsUnique = true)]
        public long DerpyRepresentationId { get; set; } //id_number
        public string ThumbTiny { get; set; } //thumb_tiny
        public string ThumbSmall { get; set; } //thumb_small
        public string Thumb { get; set; } //thumb
        public string Small { get; set; } //small
        public string Medium { get; set; } //medium
        public string Large { get; set; } //large
        public string Tall { get; set; } //tall
        public string Full { get; set; } //full
    }
}
