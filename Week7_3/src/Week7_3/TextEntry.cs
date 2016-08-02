using System;
using System.Collections.Generic;

namespace Week7_3
{
    public partial class TextEntry
    {
        public long ContentId { get; set; }
        public string ContentGuid { get; set; }
        public string Title { get; set; }
        public string ContentName { get; set; }
        public string Content { get; set; }
        public string IconPath { get; set; }
        public string DateExpires { get; set; }
        public string LastEditedBy { get; set; }
        public string ExternalLink { get; set; }
        public string Status { get; set; }
        public long ListOrder { get; set; }
        public string CallOut { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
