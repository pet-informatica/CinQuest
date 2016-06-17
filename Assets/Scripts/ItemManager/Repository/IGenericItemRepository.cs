using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb);
///	Generic definition of a Item Repository. 
/// </summary>
public interface IGenericItemRepository
{
    Dictionary<int, GenericItem> Items { get; }
    bool LoadItens();
    bool AddItem(GenericItem item);
    bool RemoveItem(int identifier);
    bool UpdateItem(int identifier, GenericItem item);
    GenericItem SearchItem(int identifier);
}