namespace Notiflow.Backoffice.Application.Models.Common;

/// <summary>
/// DataTable result
/// </summary>
/// <typeparam name="T">type of result model</typeparam>
public class DtResult<T>
{
    [JsonPropertyName("draw")]
    public int Draw { get; set; }
    
    [JsonPropertyName("recordsTotal")]
    public int RecordsTotal { get; set; }
    
    [JsonPropertyName("recordsFiltered")]
    public int RecordsFiltered { get; set; }
   
    [JsonPropertyName("data")]
    public IEnumerable<T> Data { get; set; }
    
    [JsonPropertyName("error")]
    public string Error { get; set; }

    public string PartialView { get; set; }
}

/// <summary>
/// Datatable parameters
/// </summary>
public record DtParameters
{
    public int Draw { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }
    public virtual string SortKey => Order is null ? string.Empty : string.Concat(Columns[Order[0].Column].Name, " ", Order[0].Dir);
    public string SearchKey => Search?.Value;


    #region Other Properties
    public DtColumn[] Columns { get; set; }
    public DtOrder[] Order { get; set; }
    public DtSearch Search { get; set; }
    public string SortOrder
    {
        get
        {
            if (Columns is not null && Order is not null && Order.Length > 0)
                return $"{Columns[Order[0].Column].Data}{(Order[0].Dir == DtOrderDir.Desc ? " " + Order[0].Dir : string.Empty)}";

            return default;
        }
    }

    public IEnumerable<string> AdditionalValues { get; set; }
    #endregion

    #region Special Properties
    public int PageIndex => Start;
    public int PageSize => Length;
    #endregion
}

/// <summary>
/// DataTable column
/// </summary>
public class DtColumn
{
    public string Data { get; set; }
    public string Name { get; set; }
    public bool Searchable { get; set; }
    public bool Orderable { get; set; }
    public DtSearch Search { get; set; }
}

/// <summary>
/// DataTable Order
/// </summary>
public class DtOrder
{
    public int Column { get; set; }
    public DtOrderDir Dir { get; set; }
}

/// <summary>
/// DataTable order enum
/// </summary>
public enum DtOrderDir
{
    Asc,
    Desc
}

/// <summary>
/// DataTable search
/// </summary>
public class DtSearch
{
    public string Value { get; set; }
    public bool Regex { get; set; }
}
