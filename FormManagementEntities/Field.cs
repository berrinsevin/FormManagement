namespace FormManagement.Entities
{
    public class Field
    {
        public int FieldId { get; set; }
        public int FormId { get; set; }
        public string? Name { get; set; }
        public string DataType { get; set; }
        public bool Required { get; set; }
        public string Value { get; set; }
        public virtual Form Form { get; set; }
    }
}