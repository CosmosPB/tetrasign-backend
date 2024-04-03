namespace TetraSign.Core.Helpers.Sunat;

public class HttpBody {
    public class Archivo {
        public string nomArchivo { get; set; }
        public string arcGreZip { get; set; }
        public string hashZip { get; set; }
    }

    public Archivo archivo { get; set; }

    public HttpBody() => archivo = new Archivo();
}