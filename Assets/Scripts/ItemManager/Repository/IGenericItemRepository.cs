using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb);
///	Generic definition of a Item Repository. 
/// </summary>
public interface IGenericItemRepository
{
    Dictionary<int, GenericItem> items { get; }
    bool loadItens();
    bool addItem(GenericItem item);
    bool removeItem(int identifier);
    bool updateItem(int identifier, GenericItem item);
    GenericItem searchItem(int identifier);
}