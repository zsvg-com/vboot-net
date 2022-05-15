namespace Vboot.Core.Module.Pub;

public class Zmeta
{
    public string title{ get; set; }

    public string affix{ get; set; }

    public string icon{ get; set; }

    public int orderNo{ get; set; }

    public bool ignoreKeepAlive{ get; set; }

    public Zmeta()
    {
    }

    public Zmeta(string title, string affix, string icon, int orderNo, bool ignoreKeepAlive)
    {
        this.title = title;
        this.affix = affix;
        this.icon = icon;
        this.orderNo = orderNo;
        this.ignoreKeepAlive = ignoreKeepAlive;
    }
}