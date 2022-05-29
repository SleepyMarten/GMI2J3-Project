namespace projekt.Model
{
    public record Airplane : IAirplane
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public Byte[] Img { get; set; }

        public Airplane(Guid id, string name, string description, int stars, Byte[] img) =>
            (Id, Name, Description, Stars, Img) = (id, name, description, stars, img);
        
    }
}

