namespace LibraryCatalog.Models
{
  public class CopyTitle
    {       
        public int CopyTitleId { get; set; }
        public int CopyId { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }
        public Copy Copy { get; set; }
    }
}