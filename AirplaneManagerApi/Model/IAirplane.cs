namespace projekt.Model;

public interface IAirplane
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stars { get; set; }
    public Byte[] Img { get; set; }
}