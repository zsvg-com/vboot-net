using System.Collections.Generic;

namespace Vboot.Core.Module.Pub;

public class Zmenu
{
    
    public string id{ get; set; }

    public string pid{ get; set; }

    public string perm{ get; set; }

    public string path{ get; set; }

    public string name{ get; set; }

    public string component{ get; set; }

    public Zmeta meta{ get; set; }

    public string redirect{ get; set; }

    public List<Zmenu> children{ get; set; }
}