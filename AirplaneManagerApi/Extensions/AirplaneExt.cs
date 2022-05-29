using System;
using projekt.Model;

namespace projekt.Extensions
{
    public static class AirplaneExt
    {
        public static string Verify(this IAirplane airplane)
            => airplane switch
            {
                IAirplane a when a.Id == null => "Id cannot be null",
                IAirplane a when a.Img == null  || a.Img.Length == 0 => "Img cannot be null",
                IAirplane a when a.Stars == null => "Rating cannot be 0!",
                IAirplane a when a.Name.IsNullOrEmpty() || a.Description.IsNullOrEmpty() => "Please enter all values",
                IAirplane a when !a.Name.IsSpecialCharFree() || !a.Description.IsSpecialCharFree() => "Special characters aren't allowed!",
                //Airplane a when a.Img == null => "You need to upload a photo!",
                _ => null
            };
    }
}

