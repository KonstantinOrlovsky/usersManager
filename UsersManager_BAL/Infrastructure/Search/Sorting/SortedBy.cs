using System.ComponentModel;

namespace UsersManager_BAL.Infrastructure.Search.Sorting;

public class SortedBy
{
    public SortedBy()
    {
    }

    public SortedBy(string fieldName, bool byDescending = default)
    {
        FieldName = fieldName;
        ByDescending = byDescending;
    }

    [DefaultValue("Name")]
    public string FieldName { get; set; }
    [DefaultValue(false)]
    public bool ByDescending { get; set; }
}