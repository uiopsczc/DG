
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using cfg.item;
using SimpleJSON;

namespace cfg
{
public class Tables
{
    public TbItem TbItem {get; }

    public Tables(Func<string, JSONNode> loader)
    {
        TbItem = new TbItem(loader("item_tbitem"));
        ResolveRef();
    }
    
    private void ResolveRef()
    {
        TbItem.ResolveRef(this);
    }
}

}
